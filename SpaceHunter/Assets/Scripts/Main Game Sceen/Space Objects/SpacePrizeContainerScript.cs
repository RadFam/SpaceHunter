using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacePrizeContainerScript : MonoBehaviour
{
    [SerializeField]
    private List<ResourceControl> resPrefabs = new List<ResourceControl>();
    
    // Start is called before the first frame update
    public void CreatePrize(Vector3 coordinates, int num)
    {
        ResourceControl rs = Instantiate(resPrefabs[num]);
        rs.transform.position = coordinates;
        rs.myCollider.enabled = true;
    }
}
