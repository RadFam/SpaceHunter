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

[CreateAssetMenu(fileName = "PlanetPrizeByLevels", menuName = "Planet Treasure By Level and Type", order = 54)]
public class PlanetTypePrize
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
        while ((level >= allPlanetPrizes[0].acceptableLevels[cnt]) && (cnt < allPlanetPrizes[0].acceptableLevels.Count))
        {
            cnt++;
        }

        foreach (PlanetPrizeByLevels ppbl in allPlanetPrizes)
        {
            ansList.Add(ppbl.probabilityByLevel[cnt]);
        }
        
        return ansList;
    }

    public ConstGameCtrl.PlanetSurprize GetNameByProbability(int level, float probability)
    {
        ConstGameCtrl.PlanetSurprize name = allPlanetPrizes[0].nameOfPrize;

        List<float> cummulProbList = new List<float>();

        int cnt = 0;
        while ((level >= allPlanetPrizes[0].acceptableLevels[cnt]) && (cnt < allPlanetPrizes[0].acceptableLevels.Count))
        {
            cnt++;
        }

        cummulProbList.Add(allPlanetPrizes[0].probabilityByLevel[cnt]);

        for (int i = 1; i < allPlanetPrizes.Count; ++i )
        {
            cummulProbList.Add(cummulProbList[i-1] + allPlanetPrizes[i].probabilityByLevel[cnt]);
        }

        for (int i = 0; i < cummulProbList.Count; ++i)
        {
            if (probability <= cummulProbList[i])
            {
                name = allPlanetPrizes[i].nameOfPrize;
                break;
            }
        }

        return name;
    }
}
