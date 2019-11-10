using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlanetCharacter", menuName = "Planet by Type", order = 55)]
public class PlanetByType
{
    public string planetName;
    public float planetHealth;
    public MeshFilter planetMesh;
    public Material planetMaterial;
    public PlanetPrizeByLevels planetTreasures;
}
