using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSubPanelScript : MonoBehaviour
{
    public List<GameObject> myButtons = new List<GameObject>();

    public void UnlockButtons(int level)
    {
        foreach (GameObject go in myButtons)
        {
            go.SetActive(false);
        }

        for (int i = 0; i <= level; ++i) // Levels begins from 0
        {
            myButtons[i].SetActive(true);
        }
    }
}
