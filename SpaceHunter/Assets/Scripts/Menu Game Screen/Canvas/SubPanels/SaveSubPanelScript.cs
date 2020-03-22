using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSubPanelScript : MonoBehaviour
{
    public Text editText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnOkButtonPressed()
    {
        if (editText.text != "")
        {
            string saveFilename = editText.text;
            ConstGameCtrl.instance.Save(saveFilename);
            gameObject.SetActive(false);
        }
    }

    public void OnExitButtonPressed()
    {
        gameObject.SetActive(false);
    }
}
