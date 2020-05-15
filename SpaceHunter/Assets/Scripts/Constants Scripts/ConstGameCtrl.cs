using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConstGameCtrl : MonoBehaviour {

    public enum PlanetSurprize { Gold = 0, Health, Shield, Fuel, Niinite, Amberill, Malachite, Grimadlin, DatinumColcium, Regolit, RedGold, Ktors, TurquoiseDust, Ilumnit,
                                WhisperStone, Millir, Tulwiat, Turtus, KazurGravium, Tibulum, Ilmenite, BlackKhalit, Sorit, Calcium,
                                TaoDannik, Smytit, Sandstone, UltramarAdamant, Daogai, Redrebium, Bastianum, Lorvarcor, Latriel, Witchstone,
                                Melnibonum, Massaracsheet, Zorim, Alvesid, Matrium, YashmitSalt, Asphylit, Miltanium, Kilubus, Sirlic,
                                Nemesid, Latrimeumstone, Koyperit, Kryptonit, RainbowCorall, Oortenstone, Alexandrit, Ogamit, Coronium, Nebulium};

    public enum PlayerShipUpgrades { health, shield, fuel, radar, engine, maneuver, weapon};

    public static ConstGameCtrl instance = null;
    public PrizeCollection mainPC;
    public List<GeoPrize> allPrizes;

    public SpaceShop playerGoods;
    public List<GeoPrize> playerCollection; // Минералы, которые игрок успел собрать
    public Dictionary<string, int> playerInventory;
    public Dictionary<string, int> playerStorage;

    public CollectEvent cEvent;
    public Transform playerTMPro_01;

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
    private int playerWeapon_level; public int PWeapon { get { return playerWeapon_level; } set { playerWeapon_level = value; } }// Сила орудия игрока
    [SerializeField]
    private int playerProgress_level; public int PProgress { get { return playerProgress_level; } set { playerProgress_level = value; } }// Уровень, которого достиг игрок по ходу прохождения игры
    [SerializeField]
    private int playerMoney_level; public int PMoney { get { return playerMoney_level; } set { playerMoney_level = value; } }// Количество денег у игрока
    [SerializeField]
    private List<string> playerMinerals_collection = new List<string>(); public List<string> PMinerals { get { return playerMinerals_collection; } set { playerMinerals_collection = value; } }//Названия накопленных минералов

    private int currentLevel; public int CurrentLevel { get { return currentLevel; } set { currentLevel = value; } }

    private SavebleEntity sEntity;

    // Use this for initialization
    void Start () 
    {
        DontDestroyOnLoad(this.gameObject);

        playerHealth_level = 0;
        playerShield_level = 0;
        playerFuel_level = 0;
        playerRadar_level = 0;
        playerEngine_level = 0;
        playerManeuver_level = 0;
        playerWeapon_level = 0;
        playerProgress_level = 0;
        playerMinerals_collection.Clear();

        currentLevel = 4;

        if (instance == null)
        {
            instance = this;
            allPrizes = mainPC.GamePrize;
            playerInventory = new Dictionary<string, int>();
            playerStorage = new Dictionary<string, int>();

            //playerCollection = allPrizes;

            /*
            playerCollection.Add(allPrizes[0]);
            playerCollection.Add(allPrizes[1]);
            playerCollection.Add(allPrizes[7]);
            playerCollection.Add(allPrizes[14]);
            playerCollection.Add(allPrizes[26]);
            playerCollection.Add(allPrizes[33]);
            playerCollection.Add(allPrizes[40]);
            playerCollection.Add(allPrizes[41]);
            playerCollection.Add(allPrizes[46]);
            playerCollection.Add(allPrizes[49]);
            */

            playerStorage.Add("Огненный нинит", 5);
            playerStorage.Add("Амберил", 10);
            playerStorage.Add("Малахит", 8);
            playerStorage.Add("Гримадлин", 9);

            sEntity = new SavebleEntity();
        }	
	}

    public void ZeroNewGame()
    {
        playerCollection.Clear();
        playerInventory = new Dictionary<string, int>();

        playerHealth_level = 0;
        playerShield_level = 0;
        playerFuel_level = 0;
        playerRadar_level = 0;
        playerEngine_level = 0;
        playerManeuver_level = 0;
        playerWeapon_level = 0;
        playerProgress_level = 0;
        playerMinerals_collection.Clear();

        currentLevel = 0;
    }

    public GeoPrize GetPrizeParams(int num)
    {
        //return allPrizes[0];
        return allPrizes[num];
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

    // Вызвывается, когда миссия удачно завершается (игрок не погиб!!!)
    public void MoveMineralsFromInventoryToPlayerCollection()
    {
        List<string> tmpMineralNames = new List<string>();
        foreach (KeyValuePair<string, int> keyValue in playerInventory)
        {
            bool newMineral = PlayerGetNewCollectible(keyValue.Key);
            int res = keyValue.Value;
            if (newMineral)
            {
                res -= 1;
            }
            if (res > 0) // Что-то осталось для продажи
            {
                // Передача минералов на склад
                string minName = keyValue.Key;
                if (playerStorage.ContainsKey(minName))
                {
                    int val = playerStorage[minName];
                    playerStorage[minName] = val + res;
                }
                else
                {
                    playerStorage.Add(minName, res);
                }
            }
        }

        playerInventory.Clear();
    }

    public void EmptyPlayerStorage()
    {
        playerStorage.Clear();
    }

    public bool PlayerGetNewCollectible(string mineralName)
    {
        int ind = allPrizes.FindIndex(x => x.prizeName == mineralName);
        int ind_2 = playerCollection.FindIndex(x => x.prizeName == mineralName);
        if (ind_2 == -1)
        {
            playerCollection.Add(allPrizes[ind]);
            FillPlayerCollectionNames();
            return true;
        }
        return false;
    }

    // Для отрисовки текущей коллекции // В таком виде не работает. Расчет того, как рисовать идет в скрипте, прикрепленном к UI форме
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
        FillPlayerCollectionNames();
        sEntity.UpdateData();
        Debug.Log(sEntity);
        string filenamePath = Path.Combine(Application.persistentDataPath, filename + ".shs");
        Debug.Log(filenamePath);
        using (FileStream stream = File.Open(filenamePath, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, sEntity);
        }
    }

    public void Load(string filenamePath)
    {
        Debug.Log(filenamePath);
        if (!File.Exists(filenamePath))
        {
            Debug.Log("File doesnot exist");
            return;
        }
        using (FileStream stream = File.Open(filenamePath, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            sEntity = (SavebleEntity)formatter.Deserialize(stream);
        }

        sEntity.ReloadData();

        /*
        Debug.Log(PHealth);
        Debug.Log(PShield);
        Debug.Log(PFuel);
        Debug.Log(PRadar);
        Debug.Log(PEngine);
        Debug.Log(PManeuver);
        Debug.Log(PWeapon);
        Debug.Log(PProgress);
        Debug.Log(PMoney);
        foreach(string str in PMinerals)
        {
            Debug.Log(str);
        }
        */

        FillPlayerCollectionGeo();
    }

    public int GetUpgradeLevel(PlayerShipUpgrades pUp)
    {
        switch (pUp)
        {
            case PlayerShipUpgrades.health:
                return playerHealth_level;
            case PlayerShipUpgrades.shield:
                return playerShield_level;
            case PlayerShipUpgrades.fuel:
                return playerFuel_level;
            case PlayerShipUpgrades.radar:
                return playerRadar_level;
            case PlayerShipUpgrades.engine:
                return playerEngine_level;
            case PlayerShipUpgrades.maneuver:
                return playerManeuver_level;
            case PlayerShipUpgrades.weapon:
                return playerWeapon_level;
        }
        return 0;
    }

    public void SetUpgradeLevel(PlayerShipUpgrades pUp)
    {
        switch (pUp)
        {
            case PlayerShipUpgrades.health:
                playerHealth_level++;
                return;
            case PlayerShipUpgrades.shield:
                playerShield_level++;
                return;
            case PlayerShipUpgrades.fuel:
                playerFuel_level++;
                return;
            case PlayerShipUpgrades.radar:
                playerRadar_level++;
                return;
            case PlayerShipUpgrades.engine:
                playerEngine_level++;
                return;
            case PlayerShipUpgrades.maneuver:
                playerManeuver_level++;
                return;
            case PlayerShipUpgrades.weapon:
                playerWeapon_level++;
                return;
        }
    }

    public bool TryToUpgrade(PlayerShipUpgrades pUp)
    {
        if (GetUpgradeLevel(pUp) == 3)
        {
            return false;
        }
        else
        {
            if (playerMoney_level >= playerGoods.ShopProducts[(int)pUp].upgradeDefinition[GetUpgradeLevel(pUp)+1].cost) // Золота хватает
            {
                OnChangeMoneyLevel(-1 * playerGoods.ShopProducts[(int)pUp].upgradeDefinition[GetUpgradeLevel(pUp) + 1].cost);
                SetUpgradeLevel(pUp);
                return true;
            }
            else // золота не хватает
            {
                return false;
            }
        }

        return false;
    }

    public int GetMaxPlayerParam(PlayerShipUpgrades psu)
    {
        int level = 0;

        switch ((int)psu)
        {
            case 0:
                level = playerHealth_level;
                break;
            case 1:
                level = playerShield_level;
                break;
            case 2:
                level = playerFuel_level;
                break;
            case 3:
                level = playerRadar_level;
                break;
            case 4:
                level = playerEngine_level;
                break;
            case 5:
                level = playerManeuver_level;
                break;
            case 6:
                level = playerWeapon_level;
                break;
            default:
                break;
        }

        int val = 0;
        val = playerGoods.ShopProducts[(int)psu].upgradeDefinition[level].property;
        return val;
    }

    public void OnChangeMoneyLevel(int moneyChangeValue)
    {
        playerMoney_level += moneyChangeValue;
    }

    public void OnChangeFuelLevel(int fuelChangeValue)
    {

    }

    public void OnChangeHabar(string prize, int value)
    {
        PlanetSurprize ps = (PlanetSurprize)System.Enum.Parse(typeof(PlanetSurprize), prize);


        if (ps == PlanetSurprize.Gold)
        {
            Debug.Log("Money");
            OnChangeMoneyLevel(value);
            cEvent.Raise(ps, playerMoney_level);
        }
        else if (ps == PlanetSurprize.Fuel)
        {
            Debug.Log("Fuel");
            cEvent.Raise(ps, value);
        }
        else if (ps == PlanetSurprize.Health)
        {
            // Ничего уже не делаем
            Debug.Log("Health");
            cEvent.Raise(ps, value);
        }
        else if (ps == PlanetSurprize.Shield)
        {
            // Ничего уже не делаем
            Debug.Log("Shield");
            cEvent.Raise(ps, value);
        }
        else
        {
            // Добавляем в инвентарь минерал
            Debug.Log("Mineral");
            AddMineralToInventory(prize);
            cEvent.Raise(ps, 1);
        }
    }

    public void ChangeGameScene(int value)
    {
        string scenename;

        // value = -1 - go to menu scene
        if (value == -1)
        {
            scenename = "MenuGameScene";
        }
        else
        {
            if (value == 0)
            {
                scenename = "MainGameScene";
            }
            else
            {
                scenename = "MainGameScene_" + value.ToString();
            }
        }

        SceneManager.LoadScene(scenename, LoadSceneMode.Single);
    }
}
