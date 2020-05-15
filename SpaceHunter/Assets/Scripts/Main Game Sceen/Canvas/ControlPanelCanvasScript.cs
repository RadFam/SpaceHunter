using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanelCanvasScript : MonoBehaviour {

    public Slider fuelLevel;
    public Slider healthLevel;
    public Slider plasmaLevel;
    public Slider plasmaLevel_2;
    public Slider plasmaLevel_3;
    public Text moneyShow;
    public Text flightHistory;

    public GameObject endPanel;
    public Text endPanelText;

    public Image shieldImage;
    public List<Sprite> shieldState;

    public CommonSceneParams CSP;
    private PlayerGameLog playerLog;

    // Use this for initialization
    void Start()
    {
        shieldImage.sprite = shieldState[4];
        playerLog = gameObject.GetComponent<PlayerGameLog>();
    }

    public void UpdateHealth(float currHealth)
    {
        float fullHealth = CSP.pIH;
        healthLevel.value = currHealth / fullHealth;
    }

    public void UpdateShield(float currShield)
    {
        float fullShield = CSP.pIS;
        Debug.Log("CurrShield: " + currShield + "  fullShield: " + fullShield);
        //int res = (int)((fullShield - currShield) / fullShield * 4.0f);
        int res = 0;

        if (currShield == fullShield)
        {
            Debug.Log("Case 0");
            res = 0;
        }
        if (fullShield > currShield && currShield >= (2.0f / 3.0f) * fullShield)
        {
            Debug.Log("Case 1");
            res = 1;
        }
        if ((2.0f / 3.0f) * fullShield > currShield && currShield >= (1.0f / 3.0f) * fullShield)
        {
            Debug.Log("Case 2");
            res = 2;
        }
        if ((1.0f / 3.0f) * fullShield > currShield && currShield > 0)
        {
            Debug.Log("Case 3");
            res = 3;
        }
        if (currShield == 0)
        {
            Debug.Log("Case 4");
            res = 4;
        }

        if (res < 4)
        {
            shieldImage.sprite = shieldState[res];
            Color clr = shieldImage.color;
            clr.a = 1.0f;
            shieldImage.color = clr;
        }
        else
        {
            Color clr = shieldImage.color;
            clr.a = 0.0f;
            shieldImage.color = clr;
        }


        // Здесь еще сделать звуковой(!) эффект ломающегося щита, когда выбивается одна из его четвертей
    }

    public void UpdateFuel(float currFuel)
    {
        float fullFuel = CSP.pFV;
        fuelLevel.value = currFuel / fullFuel;
    }

    public void UpdateMoney(int value)
    {
        //int money = ConstGameCtrl.instance.PMoney;
        string playerMoney = "GOLD:/n" + value.ToString().PadLeft(5, '0');
        moneyShow.text = playerMoney;
    }

    public void UpdatePlasma(int currPlasma, int fullPlasma, int plasmaType)
    {
        if (plasmaType == 0)
        {
            plasmaLevel.value = ((float)(currPlasma)) / fullPlasma;
        }
        if (plasmaType == 1)
        {
            plasmaLevel_2.value = ((float)(currPlasma)) / fullPlasma;
        }
        if (plasmaType == 2)
        {
            plasmaLevel_3.value = ((float)(currPlasma)) / fullPlasma;
        }
    }

    public void UpdateHistory(string message)
    {
        /*
        string txt = flightHistory.text;
        txt = txt + message + "\n\n";
        flightHistory.text = txt;
        */
        string txt = message;
        txt = txt + "\n\n";
        playerLog.AddEvent(txt);
    }

    public void GetParameterUpdate(string prize, int value)
    {
        //Debug.Log("GetParametersUpdate was raised");
        //Debug.Log("I`ll get " + prize + " in value of " + value.ToString());
        if (prize == "Health") // ConstGameCtrl.PlanetSurprize.Health)
        {
            UpdateHealth(value);
            string message = " Восстановлено здоровья до " + value.ToString() + " HP";
            UpdateHistory(message);
        }
        else if (prize == "Shield") // ConstGameCtrl.PlanetSurprize.Shield)
        {
            UpdateShield((float)value);
            string message = " Восстановлено брони до " + value.ToString() + " SP";
            UpdateHistory(message);
        }
        else if (prize == "Fuel") // ConstGameCtrl.PlanetSurprize.Fuel)
        {
            UpdateFuel((float)value);
            string message = " Дополучено топлива до " + value.ToString() + " FP";
            UpdateHistory(message);
        }
        else if (prize == "Gold") // ConstGameCtrl.PlanetSurprize.Gold)
        {
            UpdateMoney(value);
            string message = " Получена валюта. Дебет: " + value.ToString() + " $";
            UpdateHistory(message);
        }
        else
        {
            ConstGameCtrl.PlanetSurprize ps = (ConstGameCtrl.PlanetSurprize)System.Enum.Parse(typeof(ConstGameCtrl.PlanetSurprize), prize);
            string mineral = ConstGameCtrl.instance.allPrizes[(int)ps - 4].prizeName;
            string message = " Собран минерал:\n " + mineral;
            UpdateHistory(message);
        }
    }

    public void ShowEndMessage(string endMessage)
    {
        endPanel.SetActive(true);
        endPanelText.text = endMessage;
    }

    public void CloseEndMessage()
    {
        endPanel.SetActive(false);
        ConstGameCtrl.instance.ChangeGameScene(-1);
    }
}
