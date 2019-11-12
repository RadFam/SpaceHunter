using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstGameCtrl : MonoBehaviour {

    public enum PlanetSurprize { Gold = 0, Health, Shield, Fuel, Niinite, Amberill, Malachite, Mineral_04, Mineral_05, Mineral_06, Mineral_07, Mineral_08, Mineral_09, Mineral_10,
                                Mineral_11, Mineral_12, Mineral_13, Mineral_14, Mineral_15, Mineral_16, Mineral_17, Mineral_18, Mineral_19, Mineral_20,
                                Mineral_21, Mineral_22, Mineral_23, Mineral_24, Mineral_25, Mineral_26, Mineral_27, Mineral_28, Mineral_29, Mineral_30,
                                Mineral_31, Mineral_32, Mineral_33, Mineral_34, Mineral_35, Mineral_36, Mineral_37, Mineral_38, Mineral_39, Mineral_40,
                                Mineral_41, Mineral_42, Mineral_43, Mineral_44, Mineral_45, Mineral_46, Mineral_47, Mineral_48, Mineral_49, Mineral_50};

    public static ConstGameCtrl instance = null;
    public PrizeCollection mainPC;
    private List<GeoPrize> allPrizes;

    public List<GeoPrize> playerCollection;
    public int playerCurrentGold;
    public Dictionary<string, int> playerInventory;


	// Use this for initialization
	void Start () 
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            instance = this;
            allPrizes = mainPC.GamePrize;
            playerInventory = new Dictionary<string, int>();
        }	
	}

    public GeoPrize GetPrizeParams(int num)
    {
        return allPrizes[0];
        //return allPrizes[num];
    }

    public void AddMineralToInventory(string minName)
    {
        if (playerInventory.ContainsKey(minName))
        {
            int val = playerInventory[minName];
            playerInventory[minName] = val + 1;
        }
        else
        {
            playerInventory.Add(minName, 1);
        }
    }

    public void MoveMineralsFromInventoryToPlayerCollection()
    {
        List<string> tmpMineralNames = new List<string>();
        foreach (KeyValuePair<string, int> keyValue in playerInventory)
        {
            PlayerGetNewCollectible(keyValue.Key);
            int res = keyValue.Value - 1;
            if (res == 0)
            { 
                tmpMineralNames.Add(keyValue.Key);
            }
        }

        // Если что, останутся минералы на продажу
        foreach (string s in tmpMineralNames)
        {
            playerInventory.Remove(s);
        }
    }

    public void PlayerGetNewCollectible(string mineralName)
    {
        int ind = allPrizes.FindIndex(x => x.prizeName == mineralName);
        int ind_2 = playerCollection.FindIndex(x => x.prizeName == mineralName);
        if (ind_2 == -1)
        {
            playerCollection.Add(allPrizes[ind]);
        }
    }

    // Для отрисовки текущей коллекции
    public List<GeoPrize> CurrentPlayerCollection()
    {
        List<GeoPrize> returnList = new List<GeoPrize>(50);

        int ind = -1;
        for (int i = 0; i < 50; ++i )
        {
            ind = playerCollection.FindIndex(x => x.prizeName == allPrizes[i].prizeName);
            if (ind >= 0)
            {
                returnList[i] = allPrizes[i];
            }
        }

        return returnList;
    }
}
