using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpaceShopGood
{
    public ConstGameCtrl.PlayerShipUpgrades upgradeType;
    public List<ProductRate> upgradeDefinition;
}

[System.Serializable]
public struct ProductRate
{
    public string name;
    public int property;
    public int cost;
}

[CreateAssetMenu(fileName = "SpaceShopVariety", menuName = "ScriptableObjects/Space Shop Variety", order = 4)]
public class SpaceShop : ScriptableObject
{
    [SerializeField]
    private List<SpaceShopGood> allSpaceShopVariety;

    public List<SpaceShopGood> ShopProducts
    {
        get { return allSpaceShopVariety; }
    }
}
