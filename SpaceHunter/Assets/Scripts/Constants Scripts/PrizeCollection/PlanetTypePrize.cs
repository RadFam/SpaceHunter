using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlanetPrizeByLevels
{
    public ConstGameCtrl.PlanetSurprize nameOfPrize;
    public List<int> acceptableLevels;
    public List<float> probabilityByLevel;
}

[CreateAssetMenu(fileName = "PlanetPrizeByLevels", menuName = "ScriptableObjects/Planet Treasure", order = 3)]
public class PlanetTypePrize : ScriptableObject
{
    [SerializeField]
    private List<PlanetPrizeByLevels> allPlanetPrizes;

    public List<PlanetPrizeByLevels> GetPlanetTreasure
    {
        get { return allPlanetPrizes; }
    }

    public List<float> GetProbabilitiesByLevel(int level)
    {
        List<float> ansList = new List<float>();

        int cnt = 0;
        while ((cnt < allPlanetPrizes[0].acceptableLevels.Count) && (level >= allPlanetPrizes[0].acceptableLevels[cnt]))
        {
            cnt++;
        }
        cnt--;

        foreach (PlanetPrizeByLevels ppbl in allPlanetPrizes)
        {
            ansList.Add(ppbl.probabilityByLevel[cnt]);
        }
        
        return ansList;
    }

    public ConstGameCtrl.PlanetSurprize GetNameByProbability(int level, float probability)
    {
        //Debug.Log("Level: " + level.ToString() + "  Probability: " + probability.ToString());

        ConstGameCtrl.PlanetSurprize name = allPlanetPrizes[0].nameOfPrize;

        List<float> cummulProbList = new List<float>();
        List<ConstGameCtrl.PlanetSurprize> specNames = new List<ConstGameCtrl.PlanetSurprize>();

        int cnt = 0;
        while ((cnt < allPlanetPrizes[0].acceptableLevels.Count) && (level >= allPlanetPrizes[0].acceptableLevels[cnt]))
        {
            cnt++;
        }
        cnt--;

        //Debug.Log("Cnt: " + cnt.ToString());

        int firstPrize = 0;
        for (int i = 0; i < allPlanetPrizes.Count; ++i)
        {
            if (allPlanetPrizes[i].probabilityByLevel[cnt] > 0)
            {
                firstPrize = i;
                break;
            }
        }

        //Debug.Log("firstPrize: " + firstPrize.ToString());

        cummulProbList.Add(allPlanetPrizes[firstPrize].probabilityByLevel[cnt]); // насчет нуля нельзя быть уверенным!!!
        //Debug.Log("First list add");
        specNames.Add(allPlanetPrizes[firstPrize].nameOfPrize);
        //Debug.Log("Second list add");
        float prevVal = cummulProbList[0];
        //Debug.Log("prevVal: " + prevVal.ToString());
        Debug.Log("Cummul: " + prevVal.ToString() + "  " + allPlanetPrizes[firstPrize].nameOfPrize);

        for (int i = firstPrize+1; i < allPlanetPrizes.Count; ++i )
        {
            if (allPlanetPrizes[i].probabilityByLevel[cnt] > 0)
            {
                prevVal += allPlanetPrizes[i].probabilityByLevel[cnt];
                cummulProbList.Add(prevVal);
                specNames.Add(allPlanetPrizes[i].nameOfPrize);
                Debug.Log("Cummul: " + prevVal.ToString() + "  " + allPlanetPrizes[i].nameOfPrize);
            }    
        }

        //Debug.Log("Probability: " + probability);

        for (int i = 0; i < cummulProbList.Count; ++i)
        {
            if (probability <= cummulProbList[i])
            {
                //Debug.Log("i: " + i);
                name = specNames[i];
                break;
            }
        }

        //Debug.Log("Returned name: " + name);

        return name;
    }
}
