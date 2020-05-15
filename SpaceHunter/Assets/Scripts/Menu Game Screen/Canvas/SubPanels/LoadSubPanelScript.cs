using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadSubPanelScript : MonoBehaviour
{
    public LoadTextElement loadElementPrefab;
    public Transform parent;

    private string fileExt = "*.shs";
    private int fileCounts = 0;
    private int fileChosen = 0;
    private List<LoadTextElement> loads = new List<LoadTextElement>();
    private List<string> filenames = new List<string>();

    public void OnEnable()
    {
        string[] fileEntries = Directory.GetFiles("C:/Users/maxle/AppData/LocalLow/DefaultCompany/SpaceHunter", fileExt);
        fileCounts = fileEntries.Length;
        fileChosen = 0;

        //Debug.Log("fileCounts: " + fileCounts);

        if (fileCounts > 0)
        {
            for (int i = 0; i < fileCounts; ++i)
            {
                LoadTextElement obj = Instantiate(loadElementPrefab, parent);
                obj.gameObject.SetActive(true);
                //obj.transform.parent = loadElementPrefab.transform.parent;
                obj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y, 0.0f);
                string tmp = fileEntries[i];
                tmp = tmp.Replace(".shs", string.Empty);
                tmp = tmp.Replace("C:/Users/maxle/AppData/LocalLow/DefaultCompany/SpaceHunter\\", string.Empty);
                //Debug.Log("tmp name: " + tmp);
                obj.SetParams(tmp, i);
                loads.Add(obj);
                filenames.Add(tmp);
            }
            loads[fileChosen].SetColor(2);
        }
    }

    public void OnLoadFilePressed(int num)
    {
        fileChosen = num;
        for (int i = 0; i < fileCounts; ++i)
        {
            if (i != fileChosen)
            {
                loads[i].SetColor(1);
            }
            else
            {
                loads[i].SetColor(2);
            }
        }
    }

    public void OnOkButtonPressed()
    {
        string tmp;
        tmp = Path.Combine(Application.persistentDataPath, filenames[fileChosen] + ".shs");
        ConstGameCtrl.instance.Load(tmp);
        OnExitButtonPressed();
    }

    public void OnExitButtonPressed()
    {
        loads.Clear();
        filenames.Clear();
        gameObject.SetActive(false);
    }
}
