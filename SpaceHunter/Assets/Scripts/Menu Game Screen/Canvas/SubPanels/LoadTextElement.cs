using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class LoadTextElement : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    //public TextMeshProUGUI loadText;
    public Text loadText;
    private int myNum;

    public int MyNum
    {
        get { return myNum; }
        set { myNum = value; }
    }

   public void SetParams(string txt, int num)
   {
        loadText.text = txt;
        myNum = num;
   }

    public void SetColor(int colorType)
    {
        Color color = new Color(0, 0, 0);
        if (colorType == 1)
        {
            color = new Color(255, 246, 0);
        }
        if (colorType == 2)
        {
            color = new Color(255, 255, 255);
        }
        loadText.color = color;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        LoadSubPanelScript lsps = GameObject.Find("LoadSubPanel").GetComponent<LoadSubPanelScript>();
        lsps.OnLoadFilePressed(myNum);
    }
}
