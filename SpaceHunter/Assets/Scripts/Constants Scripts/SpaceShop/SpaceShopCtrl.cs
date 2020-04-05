using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShopCtrl : MonoBehaviour
{
    public List<UpgradePanelScript> upgradeElems = new List<UpgradePanelScript>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateAllUpgrades()
    {
        foreach (UpgradePanelScript ups in upgradeElems)
        {
            ups.UpdateStatus();
        }
    }
}
