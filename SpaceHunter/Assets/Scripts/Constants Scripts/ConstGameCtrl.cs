using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstGameCtrl : MonoBehaviour {

    public enum PlanetSurprize { Gold = 0, Health, Shield, Fuel, Niinite, Amberill, Malachite, Grimadlin, DatinumColcium, Regolit, RedGold, Ktors, TurquoiseDust, Ilumnit,
                                WhisperStone, Millir, Tulwiat, Turtus, KazurGravium, Tibulum, Ilmenite, BlackKhalit, Sorit, Calcium,
                                TaoDannik, Smytit, Sandstone, UltramarAdamant, Daogai, Redrebium, Bastianum, Lorvarcor, Latriel, Witchstone,
                                Melnibonum, Massaracsheet, Zorim, Alvesid, Matrium, YashmitSalt, Asphylit, Miltanium, Kilubus, Sirlic,
                                Nemesid, Latrimeumstone, Koyperit, Kryptonit, RainbowCorall, Oortenstone, Alexandrit, Ogamit, Coronium, Nebulium};

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
