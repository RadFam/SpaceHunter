using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellMineralSubPanel : GeneralMenu
{
    public Image leftImage;
    public Image rightImage;
    public Image mainImage;
    public Text costMineral;
    public Text valueMineral;
    public Text valueMoney;

    private bool canSell = false;
    private int costVal = 0;
    private int volVal = 0;

    private void Start()
    {
        
    }

    public void OnEnable()
    {
        
    }

    public void SetMainImageInfo(Sprite spr, int cost, int vol)
    {
        mainImage.sprite = spr;
        costMineral.text = cost.ToString();
        valueMineral.text = vol.ToString();

        costVal = cost;
        volVal = vol;
    }

    public void SetLeftImageInfo(Sprite spr)
    {
        leftImage.sprite = spr;
    }

    public void SetRightImageInfo(Sprite spr)
    {
        rightImage.sprite = spr;
    }

    public void SetNewVolume(int deltaVolume, int newMoney)
    {
        volVal -= deltaVolume;
        valueMineral.text = volVal.ToString();

        valueMoney.text = newMoney.ToString();
    }
}
