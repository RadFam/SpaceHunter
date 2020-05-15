using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemPanelScript : GeneralMenu
{
    public SaveSubPanelScript ssps;
    public LoadSubPanelScript lsps;

    public void OpenSaveDialog()
    {
        ssps.gameObject.SetActive(true);
    }

    public void OpenLoadDialog()
    {
        lsps.gameObject.SetActive(true);
        //lsps.OnEnable();
    }

    public void OpenNewGame()
    {
        ConstGameCtrl.instance.ZeroNewGame();
        gameObject.SetActive(false);
    }
}
