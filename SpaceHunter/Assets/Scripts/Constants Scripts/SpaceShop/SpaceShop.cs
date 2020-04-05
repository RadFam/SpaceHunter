using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ProductRate
{
    public int property;
    public int cost;
}

[CreateAssetMenu(fileName = "SpaceShopVariety", menuName = "ScriptableObjects/Space Shop Variety", order = 4)]
public class SpaceShop : ScriptableObject
{
    [SerializeField]
    private Dictionary<ConstGameCtrl.PlayerShipUpgrades, ProductRate> allSpaceShopVariety;

    public Dictionary<ConstGameCtrl.PlayerShipUpgrades, ProductRate> ShopProducts
    {
        get { return allSpaceShopVariety; }
    }
}
