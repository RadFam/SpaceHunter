using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionPanelScaleScript : MonoBehaviour
{
    // Start is called before the first frame update

    public ScaleWindowScript sws;
    public DescriptionSubPanelScript dsps;

    public void CalculateScaling()
    {
        sws.ScaleInnerContent();
    }

    public void DrawCollection()
    {
        sws.RedrawCollection();
    }

    public void OpenMineralInfoPanel(int num)
    {
        dsps.gameObject.SetActive(true);
        dsps.SetText(ConstGameCtrl.instance.allPrizes[num].prizeName + "\n\n" + ConstGameCtrl.instance.allPrizes[num].prizeDescription);
    }
}
