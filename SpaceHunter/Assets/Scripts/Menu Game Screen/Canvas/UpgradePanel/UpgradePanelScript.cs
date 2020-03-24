using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelScript : MonoBehaviour
{
    public ConstGameCtrl.PlayerShipUpgrades upgradeType;
    public Image statusImage;
    public Image upgradeImage;

    [SerializeField]
    List<Sprite> myUpgradeImages = new List<Sprite>();
    [SerializeField]
    List<Sprite> myStatusImages = new List<Sprite>();

    private int myCurrLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateStatus();
    }

    // Update is called once per frame
    public void UpdateStatus()
    {
        myCurrLevel = ConstGameCtrl.instance.GetUpgradeLevel(upgradeType);

        statusImage.sprite = myStatusImages[myCurrLevel];
        upgradeImage.sprite = myUpgradeImages[Mathf.Min(2, myCurrLevel)];
    }

    public void OnUpgradeImageClicked()
    {
        if (ConstGameCtrl.instance.TryToUpgrade(upgradeType))
        {
            UpdateStatus();
        }
    }
}
