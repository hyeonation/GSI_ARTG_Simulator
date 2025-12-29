using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System;
using System.Linq;
//<summary>
// PLC Struct Generator Editor Window
// PLC 데이터 구조체를 자동으로 생성하는 데 사용됩니다.
// CSV 파일을 읽어 C# Struct 코드를 생성합니다. path는 Assets/Script/DataBase/에 저장됩니다.
// unity Editor 메뉴에서 접근 가능하며, Tools > PLC Struct Generator Window 에서 실행할 수 있습니다.
//</summary>
public class PlcStructGenerator : EditorWindow
{
    private UnityEngine.Object csvFileObject;
    private string outputClassName = "CraneWritePlcData";

    [MenuItem("Tools/PLC Struct Generator Window")]
    public static void ShowWindow()
    {
        GetWindow<PlcStructGenerator>("PLC Gen");
    }

    private void OnGUI()
    {
        GUILayout.Label("PLC Data Struct Generator (v2.5 - Array & Struct Fix)", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        csvFileObject = EditorGUILayout.ObjectField("CSV File (.csv)", csvFileObject, typeof(UnityEngine.Object), false);
        outputClassName = EditorGUILayout.TextField("Output Class Name", outputClassName);

        EditorGUILayout.Space();

        if (GUILayout.Button("Generate C# Struct (Fixed)"))
        {
            GenerateStruct();
        }
    }

    private void GenerateStruct()
    {
        if (csvFileObject == null || string.IsNullOrWhiteSpace(outputClassName))
        {
            EditorUtility.DisplayDialog("Error", "CSV 파일과 클래스 이름을 확인해주세요.", "OK");
            return;
        }

        string csvPath = AssetDatabase.GetAssetPath(csvFileObject);
        string outputPath = $"Assets/Script/DataBase/{outputClassName}.cs";

        ParseAndGenerate(csvPath, outputPath, outputClassName);
    }

    private class FieldInfoData
    {
        public int Offset;
        public string CodeLine;
    }

    private void ParseAndGenerate(string csvPath, string outputPath, string className)
    {
        if (!File.Exists(csvPath)) return;

        string[] lines = File.ReadAllLines(csvPath);
        List<FieldInfoData> generatedFields = new List<FieldInfoData>();
        Dictionary<int, List<string>> bitProperties = new Dictionary<int, List<string>>();
        Dictionary<int, string> bitBackingNames = new Dictionary<int, string>();
        // Struct Depth 관리를 위한 해시
        HashSet<string> structStack = new HashSet<string>();

        // 중복확인을 위한 해시
        HashSet<string> fieldNames = new HashSet<string>();

        string currentArrayPrefix = "";
        string currentStructContext = "";

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var parts = line.Split(',');
            if (parts.Length < 3) continue;

            string originalName = parts[0].Trim();
            string type = parts[1].Trim();
            string offsetStr = parts[2].Trim();

            if (originalName.Equals("Name", StringComparison.OrdinalIgnoreCase)) continue;

            // 1. 배열 인덱스 추출 (예: Block_Map[0] -> "0") 추후 중복되는 변수에 인덱스 부여
            if (originalName.Contains("["))
            {
                currentArrayPrefix = originalName.Replace("[", "_").Replace("]", "");
            }

            // 2. 구조체 및 중간 선언부(GetCSharpType의 default 값들) 필터링
            // 'Struct' 타입이거나 특정 명칭을 가진 행은 필드 생성을 건너뛰고 컨텍스트만 갱신함
            string csharpType = GetCSharpType(type);
            if (csharpType.Equals("Struct", StringComparison.OrdinalIgnoreCase))
            {
                if (csharpType.Contains("Struct"))
                {
                    currentStructContext = SanitizeName(originalName.Split('[')[0]);
                    structStack.Add(currentStructContext);
                }
                continue;
            }

            if (csharpType.StartsWith("Array", StringComparison.OrdinalIgnoreCase)) continue;

            if (!double.TryParse(offsetStr, out double offsetRaw)) continue;




            int byteOffset = (int)Math.Floor(offsetRaw);
            string baseFieldName = SanitizeName(originalName.Split('[')[0]);

            // 3. 필드 이름 중복 방지 로직 (인덱스 결합)
            string finalFieldName = string.IsNullOrEmpty(currentStructContext)
                ? baseFieldName
                : $"{currentStructContext}_{baseFieldName}";

            if (!string.IsNullOrEmpty(currentArrayPrefix) && !finalFieldName.StartsWith(currentArrayPrefix))
            {
                finalFieldName = $"{currentArrayPrefix}_{baseFieldName}";
            }
            // 중복확인
            int duplicateCount = 1;
            while (fieldNames.Contains(finalFieldName))
            {
                // 중복이라면 이전의 struct 가져오기 반복
                // 한번더 전이여야하는데?
                finalFieldName = $"{structStack.ToList()[structStack.Count - duplicateCount]}_{finalFieldName}";
                duplicateCount++;
            }
            fieldNames.Add(finalFieldName);

            // 4. 데이터 타입별 처리
            if (csharpType.Equals("Bool", StringComparison.OrdinalIgnoreCase))
            {
                int bitIndex = (int)Math.Round((offsetRaw - byteOffset) * 10);

                if (!bitProperties.ContainsKey(byteOffset))
                {
                    bitProperties[byteOffset] = new List<string>();
                    bitBackingNames[byteOffset] = $"{currentStructContext}_Raw_{byteOffset}";
                }

                bitProperties[byteOffset].Add($"    // Bit {bitIndex}: {originalName}");
                bitProperties[byteOffset].Add($"    public bool {finalFieldName} => ({bitBackingNames[byteOffset]} & (1 << {bitIndex})) != 0;");
            }
            else
            {

                string code = $"    [FieldOffset({byteOffset})]\n    public {csharpType} {finalFieldName};";
                generatedFields.Add(new FieldInfoData { Offset = byteOffset, CodeLine = code });
            }
        }

        // 비트 속성 추가
        foreach (var kvp in bitProperties)
        {
            StringBuilder sbBit = new StringBuilder();
            sbBit.AppendLine($"    [FieldOffset({kvp.Key})]");
            sbBit.AppendLine($"    public byte {bitBackingNames[kvp.Key]};");
            foreach (var prop in kvp.Value) sbBit.AppendLine(prop);
            generatedFields.Add(new FieldInfoData { Offset = kvp.Key, CodeLine = sbBit.ToString().TrimEnd() });
        }

        // 5. 코드 생성 및 저장
        var sortedFields = generatedFields.OrderBy(f => f.Offset).ToList();
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("using System;");
        sb.AppendLine("using System.Runtime.InteropServices;");
        sb.AppendLine();
        sb.AppendLine("[Serializable]");
        sb.AppendLine("[StructLayout(LayoutKind.Explicit)]");
        sb.AppendLine($"public struct {className}");
        sb.AppendLine("{");
        foreach (var field in sortedFields) sb.AppendLine(field.CodeLine);
        sb.AppendLine("}");

        try
        {
            File.WriteAllText(outputPath, sb.ToString());
            AssetDatabase.Refresh();
            Debug.Log($"<color=lime>[PlcGen] Refined Struct Generated: {className}</color>");
        }
        catch (Exception e) { Debug.LogError($"Error: {e.Message}"); }
    }

    private string SanitizeName(string name)
    {
        if (string.IsNullOrEmpty(name)) return "Unnamed";
        string cleanName = name.Replace(" ", "_").Replace("-", "_").Replace(".", "_");
        if (char.IsDigit(cleanName[0])) cleanName = "_" + cleanName;
        return cleanName;
    }

    private string GetCSharpType(string plcType)
    {
        if (string.IsNullOrEmpty(plcType)) return "Struct";
        string t = plcType.Trim();

        return t switch
        {
            _ when t.Contains("Array", StringComparison.OrdinalIgnoreCase) => "Array",
            _ when t.Contains("Struct", StringComparison.OrdinalIgnoreCase) => "Struct",
            _ when t.Equals("real", StringComparison.OrdinalIgnoreCase) => "float",
            _ when t.Equals("dint", StringComparison.OrdinalIgnoreCase) => "int",
            _ when t.Equals("int", StringComparison.OrdinalIgnoreCase) => "short",
            _ when t.Equals("word", StringComparison.OrdinalIgnoreCase) => "ushort",
            _ when t.Equals("byte", StringComparison.OrdinalIgnoreCase) || t.Equals("char", StringComparison.OrdinalIgnoreCase) => "byte",
            _ when t.Equals("bool", StringComparison.OrdinalIgnoreCase) => "bool",
            _ => "Struct" // 기본값. 위에서 처리되지 않은 타입은 Struct로 간주
        };
    }
}