using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class LoadTextElement : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    public TextMeshProUGUI loadText;
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
        Color color;
        if (colorType == 1)
        {
            color = new Color(255, 246, 0);
        }
        if (colorType == 2)
        {
            color = new Color(255, 255, 255);
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        LoadSubPanelScript lsps = GameObject.Find("LoadSubPanelScript").GetComponent<LoadSubPanelScript>();
        lsps.OnLoadFilePressed(myNum);
    }
}
