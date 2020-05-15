using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GeoPrize
{
    public string prizeName;
    public string prizeDescription;
    public int prizeCost;
    public Sprite prizeSprite;
    public MeshFilter prizeMesh;
    public Material prizeMaterial;
}

[CreateAssetMenu(fileName = "FullPrizeCollection", menuName = "ScriptableObjects/Prize Collection", order = 2)]
public class PrizeCollection : ScriptableObject 
{
    [SerializeField]
    private List<GeoPrize> allPrizeCollection;

    public List<GeoPrize> GamePrize
    {
        get { return allPrizeCollection; }
    }
}
