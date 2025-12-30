using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CraneControlPopup : UI_Popup
{
    #region Enums
    public enum Buttons
    {
        Background,
        Btn_LIDAR
    }

    #endregion

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));

        GetButton((int)Buttons.Btn_LIDAR).onClick.AddListener(OnClickLIDARButton);
        GetButton((int)Buttons.Background).onClick.AddListener(ClosePopup);

        return true;
    }


    public void OnClickLIDARButton()
    {

    }
}
