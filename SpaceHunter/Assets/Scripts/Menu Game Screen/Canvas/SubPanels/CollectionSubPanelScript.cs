using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionSubPanelScript : MonoBehaviour
{
    public DescriptionSubPanelScript dsps;
    public List<MineralImageScript> mineralImages = new List<MineralImageScript>(); // 50 - is the const length!

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ShowMineralDescription(int num)
    {
        dsps.SetText(ConstGameCtrl.instance.mainPC.GamePrize[num].prizeName + "/n/n" + ConstGameCtrl.instance.mainPC.GamePrize[num].prizeDescription);
    }
}
