using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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

    public List<GeoPrize> playerCollection; // Минералы, которые игрок успел собрать
    public int playerCurrentGold;
    public Dictionary<string, int> playerInventory;

    [SerializeField]
    private int playerHealth_level; public int PHealth { get { return playerHealth_level; } set { playerHealth_level = value; } }// Здоровье игрока
    [SerializeField]
    private int playerShield_level; public int PShield { get { return playerShield_level; } set { playerShield_level = value; } }// Броня игрока
    [SerializeField]
    private int playerFuel_level; public int PFuel { get { return playerFuel_level; } set { playerFuel_level = value; } }// Время для прохождения карты
    [SerializeField]
    private int playerRadar_level; public int PRadar { get { return playerRadar_level; } set { playerRadar_level = value; } }// Дальность видимости игрока
    [SerializeField]
    private int playerEngine_level; public int PEngine { get { return playerEngine_level; } set { playerEngine_level = value; } }// Скорость линейного движения игрока
    [SerializeField]
    private int playerManeuver_level; public int PManeuver { get { return playerManeuver_level; } set { playerManeuver_level = value; } }// Скорость поворотов игрока
    [SerializeField]
    private int playerProgress_level; public int PProgress { get { return playerProgress_level; } set { playerProgress_level = value; } }// Уровень, которого достиг игрок по ходу прохождения игры
    [SerializeField]
    private int playerMoney_level; public int PMoney { get { return playerMoney_level; } set { playerMoney_level = value; } }// Количество денег у игрока
    [SerializeField]
    private List<string> playerMinerals_collection = new List<string>(); public List<string> PMinerals { get { return playerMinerals_collection; } set { playerMinerals_collection = value; } }//Названия накопленных минералов

    private SavebleEntity sEntity;

    // Use this for initialization
    void Start () 
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            instance = this;
            allPrizes = mainPC.GamePrize;
            playerInventory = new Dictionary<string, int>();

            sEntity = new SavebleEntity();
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
            // Где-то здесь должна идти продажа....
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
        FillPlayerCollectionNames();
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

    public void FillPlayerCollectionNames()
    {
        playerMinerals_collection.Clear();

        foreach (GeoPrize gp in playerCollection)
        {
            playerMinerals_collection.Add(gp.prizeName);
        }
    }

    public void FillPlayerCollectionGeo()
    {
        playerCollection.Clear();

        foreach(string name in playerMinerals_collection)
        {
            int ind = allPrizes.FindIndex(x => x.prizeName == name);
            if (ind != -1)
            {
                playerCollection.Add(allPrizes[ind]);
            }
        }
    }

    public void Save(string filename)
    {
        sEntity.UpdateData();
        string filenamePath = Path.Combine(Application.persistentDataPath, filename + ".shs");
        using (FileStream stream = File.Open(filenamePath, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, sEntity);
        }
    }

    public void Load(string filenamePath)
    {
        if (!File.Exists(filenamePath))
        {
            return;
        }
        using (FileStream stream = File.Open(filenamePath, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            sEntity = (SavebleEntity)formatter.Deserialize(stream);
        }

        sEntity.ReloadData();
        FillPlayerCollectionGeo();
    }
}
