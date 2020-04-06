using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPanelScript : MonoBehaviour
{
    [SerializeField]
    List<GameObject> panels = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenNeedPanel(int panelNum)
    {
        panels[panelNum].SetActive(true);

        if (panelNum == 1)
        {
            panels[panelNum].GetComponent<LevelSubPanelScript>().UnlockButtons(ConstGameCtrl.instance.PProgress);
        }

        if (panelNum == 2)
        {
            panels[panelNum].GetComponent<CollectionPanelScaleScript>().CalculateScaling();
            panels[panelNum].GetComponent<CollectionPanelScaleScript>().DrawCollection();
        }

        if (panelNum == 3)
        {
            panels[panelNum].GetComponent<SpaceShopCtrl>().UpdateAllUpgrades();
        }

        /*
        switch (panelNum)
        {
            case 0:
                panels[panelNum].SetActive(true);
                break;
            case 1:
                panel = GameObject.Find("LevelPanel");
                panel.SetActive(true);
                break;
            case 2:
                panel = GameObject.Find("CollectionPanel");
                panel.SetActive(true);
                break;
            case 3:
                panel = GameObject.Find("ShopPanel");
                panel.SetActive(true);
                break;
        }
        */
    }
}
