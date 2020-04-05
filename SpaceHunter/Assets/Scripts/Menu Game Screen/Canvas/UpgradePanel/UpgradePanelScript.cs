using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradePanelScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ConstGameCtrl.PlayerShipUpgrades upgradeType;
    public Image statusImage;
    public Image upgradeImage;
    public CostToolTip myToolTip;

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
            myToolTip.OnCloseTooltip();
            UpdateStatus();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (myCurrLevel < 3)
        {
            int nextLevel = myCurrLevel + 1;
            string name = ConstGameCtrl.instance.playerGoods.ShopProducts[(int)upgradeType].upgradeDefinition[nextLevel].name;
            int cost = ConstGameCtrl.instance.playerGoods.ShopProducts[(int)upgradeType].upgradeDefinition[nextLevel].cost;
            myToolTip.OnShowTooltip(name, cost);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (myCurrLevel < 3)
        {
            myToolTip.OnCloseTooltip();
        }
    }
}
