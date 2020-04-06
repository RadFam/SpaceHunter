using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionSubPanelScript : GeneralMenu
{
    [SerializeField]
    private Text myText;

    void Start()
    {
        //myText.text = "";
    }

    public void SetText(string txt)
    {
        Debug.Log(txt);
        myText.text = txt;
    }
}
