using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellMineralControl : MonoBehaviour
{
    public Sprite defaultSprite;

    private SellMineralSubPanel SMSP;
    private GeneralSubMenu GSM;
    private bool canSell = false;
    private int lengStorage;
    private int currMinSell;
    private List<string> sellMinerals = new List<string>();
    private List<int> costMinerals = new List<int>();
    private List<int> deltaVolSell = new List<int>();
    private List<int> deltaCostSell = new List<int>();

    // Start is called before the first frame update
    void Awake() // Может и не сработать
    {
        SMSP = gameObject.GetComponent<SellMineralSubPanel>();
        GSM = gameObject.GetComponent<GeneralSubMenu>();
    }

    public void OnEnable()
    {
        SMSP = gameObject.GetComponent<SellMineralSubPanel>();
        sellMinerals.Clear();
        costMinerals.Clear();

        lengStorage = ConstGameCtrl.instance.playerStorage.Count;
        if (lengStorage > 0)
        {
            canSell = true;
            currMinSell = 0;

            foreach(KeyValuePair<string, int> keyValue in ConstGameCtrl.instance.playerStorage)
            {
                sellMinerals.Add(keyValue.Key);
                costMinerals.Add(ConstGameCtrl.instance.allPrizes.Find(x => x.prizeName == keyValue.Key).prizeCost);
            }

            // Рисуем картинки минералов в окошке
            DisplayStorageContent();
        }
    }

    void DisplayStorageContent()
    {
        string mainMineralName = sellMinerals[currMinSell];

        GeoPrize gprz = ConstGameCtrl.instance.allPrizes.Find(x => x.prizeName == mainMineralName);
        SMSP.SetMainImageInfo(gprz.prizeSprite, gprz.prizeCost, ConstGameCtrl.instance.playerStorage[mainMineralName]);

        if (currMinSell < sellMinerals.Count-1)
        {
            mainMineralName = sellMinerals[currMinSell + 1];

            gprz = ConstGameCtrl.instance.allPrizes.Find(x => x.prizeName == mainMineralName);
            SMSP.SetRightImageInfo(gprz.prizeSprite);
        }
        if (currMinSell > 0)
        {
            mainMineralName = sellMinerals[currMinSell - 1];

            gprz = ConstGameCtrl.instance.allPrizes.Find(x => x.prizeName == mainMineralName);
            SMSP.SetLeftImageInfo(gprz.prizeSprite);
        }

        if (currMinSell == sellMinerals.Count - 1)
        {
            SMSP.SetRightImageInfo(defaultSprite);
        }
    }

    public void StartStorageSell()
    {
        if (canSell)
        {
            GSM.isOverLocked = true;
            DisplayStorageContent();

            // Рассчитаем, по скольку минералов нужно продавать за такт корутины
            deltaVolSell.Clear();
            deltaCostSell.Clear();
            int vol = ConstGameCtrl.instance.playerStorage[sellMinerals[currMinSell]];
            for (int i = 0; i < 2; ++i)
            {
                int delta = (int)(vol/3);
                deltaVolSell.Add(delta);
                deltaCostSell.Add(delta * costMinerals[currMinSell]);
                vol -= delta;
            }
            deltaVolSell.Add(vol);
            deltaCostSell.Add(vol * costMinerals[currMinSell]);

            StartCoroutine(DecreaseMineralVolume());
        }
        else
        {
            ConstGameCtrl.instance.playerStorage.Clear();
            StartCoroutine(BeforeClosePause());
        }
    }

    public IEnumerator DecreaseMineralVolume()
    {

        for (int i = 0; i < 3; ++i)
        {
            ConstGameCtrl.instance.OnChangeMoneyLevel(deltaCostSell[i]);
            SMSP.SetNewVolume(deltaVolSell[i], ConstGameCtrl.instance.PMoney);

            yield return new WaitForSeconds(0.1f*(3-i));
        }

        currMinSell++;
        if (currMinSell >= sellMinerals.Count)
        {
            canSell = false;
        }
        StartStorageSell();
    }

    public IEnumerator BeforeClosePause()
    {
        yield return new WaitForSeconds(0.5f);
        ClosePanel();
    }

    public void ClosePanel()
    {
        GSM.isOverLocked = false;
        currMinSell = 0;
        canSell = false;
        sellMinerals.Clear();
        SMSP.SetMainImageInfo(defaultSprite, 0, 0);
        SMSP.SetRightImageInfo(defaultSprite);
        SMSP.SetLeftImageInfo(defaultSprite);

        // Закрыть саму панель
        gameObject.SetActive(false);
    }
}
