using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCountPortals : MonoBehaviour
{

    public PlanetPortalScript portalPrefab;
    public int difficulty; // from 2 to 4
    protected Collider myCollider;
    protected List<Vector3> possiblePlaces;
    protected List<Vector3> possibleAngles;
    protected int gatesOpened;

    // Start is called before the first frame update
    void Start()
    {
        gatesOpened = 0;
        myCollider = GetComponent<Collider>();

        possiblePlaces = new List<Vector3>();
        possibleAngles = new List<Vector3>();
        possiblePlaces.Add(new Vector3(0.0f,-1.0f, 0.0f));
        possiblePlaces.Add(new Vector3(1.0f, 0.0f, 0.0f));
        possiblePlaces.Add(new Vector3(0.0f, 0.0f, -1.0f));
        possiblePlaces.Add(new Vector3(-1.0f, 0.0f, 0.0f));
        possiblePlaces.Add(new Vector3(0.0f, 0.0f, 1.0f));
        possiblePlaces.Add(new Vector3(0.58f, -0.58f, 0.58f));
        possiblePlaces.Add(new Vector3(0.58f, -0.58f, -0.58f));
        possiblePlaces.Add(new Vector3(-0.58f, -0.58f, 0.58f));
        possiblePlaces.Add(new Vector3(-0.58f, -0.58f, -0.58f));
        possibleAngles.Add(new Vector3(0.0f, 0.0f, -180.0f));
        possibleAngles.Add(new Vector3(0.0f, 0.0f, 0.0f));
        possibleAngles.Add(new Vector3(0.0f, -90.0f, 0.0f));
        possibleAngles.Add(new Vector3(0.0f, 0.0f, 0.0f));
        possibleAngles.Add(new Vector3(0.0f, 90.0f, 0.0f));
        possibleAngles.Add(new Vector3(0.0f, -45.0f, -22.5f));
        possibleAngles.Add(new Vector3(0.0f, 45.0f, -22.5f));
        possibleAngles.Add(new Vector3(0.0f, 45.0f, 22.5f));
        possibleAngles.Add(new Vector3(0.0f, -45.0f, 22.5f));

        // Нужно сгенерировать произвольное число врат
        List<Vector3> subList = new List<Vector3>();
        List<Vector3> subList2 = new List<Vector3>();
        float fDiff = difficulty;
        int cnt = 0;
        foreach (Vector3 item in possiblePlaces)
        {
            float rnd = Random.Range(0.0f, 1.0f);
            if (rnd <= (fDiff-subList.Count)/(possiblePlaces.Count-cnt) && subList.Count < difficulty)
            {
                subList.Add(item);
                subList2.Add(possibleAngles[cnt]);
            }
            cnt++;
        }

        // Ставим врата
        float dist = myCollider.bounds.size.x*0.75f;
        for (int i = 0; i < subList.Count; ++i)
        {
            PlanetPortalScript pp = Instantiate(portalPrefab);
            pp.gameObject.transform.parent = gameObject.transform;
            Vector3 subCoords = subList[i];
            Vector3 localAngles = new Vector3(Vector3.Angle(subCoords, pp.gameObject.transform.forward), Vector3.Angle(subCoords, pp.gameObject.transform.up), Vector3.Angle(subCoords, pp.gameObject.transform.right));
            pp.SetParams(i, this, subCoords*dist, subList2[i]);
        }
    }

    public void SendSignal(int myNum)
    {
        gatesOpened++;
        if (gatesOpened == difficulty)
        {
            PlanetTreasure PT = GetComponent<PlanetTreasure>();
            PT.OnPlanetTreasure();
        }
    }
}
