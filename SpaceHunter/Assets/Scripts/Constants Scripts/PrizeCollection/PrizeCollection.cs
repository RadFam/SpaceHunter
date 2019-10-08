using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GeoPrize
{
    public string prizeName;
    public string prizeDescription;
    public Sprite prizeSprite;
    public MeshFilter prizeMesh;
    public Material prizeMaterial;
}

[CreateAssetMenu(fileName = "FullPrizeCollection", menuName = "Prize Collection", order = 53)]
public class PrizeCollection : ScriptableObject 
{
    [SerializeField]
    private List<GeoPrize> allPrizeCollection;

    public List<GeoPrize> GamePrize
    {
        get { return allPrizeCollection; }
    }
}
