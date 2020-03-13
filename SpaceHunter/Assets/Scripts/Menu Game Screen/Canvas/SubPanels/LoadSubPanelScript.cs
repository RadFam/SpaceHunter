using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadSubPanelScript : MonoBehaviour
{
    public LoadTextElement loadElementPrefab;

    private string fileExt = "*.shs";
    private int fileCounts = 0;
    private int fileChosen = 0;
    private List<LoadTextElement> loads = new List<LoadTextElement>();

    public void OnEnable()
    {
        string[] fileEntries = Directory.GetFiles("/Saves", fileExt);
        fileCounts = fileEntries.Length;

        if (fileCounts > 0)
        {
            for (int i = 0; i < fileCounts; ++i)
            {
                LoadTextElement obj = Instantiate(loadElementPrefab);
                obj.gameObject.SetActive(true);
                obj.transform.parent = loadElementPrefab.transform.parent;
                string tmp = fileEntries[i];
                tmp = tmp.Replace(".shs", string.Empty);
                obj.SetParams(tmp, i);
                loads.Add(obj);
            }
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
        
    }

    public void OnExitButtonPressed()
    {
        loads.Clear();
        gameObject.SetActive(false);
    }
}
