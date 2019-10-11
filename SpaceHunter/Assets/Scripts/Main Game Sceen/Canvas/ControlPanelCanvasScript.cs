using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanelCanvasScript : MonoBehaviour {

    public Slider fuelLevel;
    public Slider healthLevel;
    public Slider plasmaLevel;

    public Image shieldImage;
    public List<Sprite> shieldState;

    public CommonSceneParams CSP;

    // Use this for initialization
	void Start () 
    {
        shieldImage.sprite = shieldState[4];
	}

    public void UpdateHealth(float currHealth)
    {
        float fullHealth = CSP.pIH;
        healthLevel.value = currHealth / fullHealth;
    }

    public void UpdateShield(float currShield)
    {
        float fullShield = CSP.pIS;
        int res = (int)(currShield / fullShield * 4.0f);
        shieldImage.sprite = shieldState[res];

        // Здесь еще сделать звуковой(!) эффект ломающегося щита, когда выбивается одна из его четвертей
    }

    public void UpdateFuel(float currFuel)
    {
        float fullFuel = CSP.pFV;
        fuelLevel.value = currFuel / fullFuel;
    }

    public void UpdatePlasma(int currPlasma, int fullPlasma)
    {
        plasmaLevel.value = ((float)(currPlasma)) / fullPlasma;
    }
}
