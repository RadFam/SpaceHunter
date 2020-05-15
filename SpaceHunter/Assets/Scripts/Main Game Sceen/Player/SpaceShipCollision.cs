using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipCollision : MonoBehaviour {

    public Damagable myDamagable;
    public Transform prnt;

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision was");

        if (other.gameObject.tag == "Planet" || other.gameObject.tag == "Sun" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "Comet" || other.gameObject.tag == "Asteroid")
        {
            //Debug.Log("Hard object reached");

            myDamagable.TakeUltimateDamage();
        }

        if (other.gameObject.tag == "Prize") 
        {
            //Debug.Log("Treasure reached");

            TresureControl TC = other.gameObject.GetComponent<TresureControl>();
            string prizeName = TC.ReturnTreasureName();
            string prizeNameSec = TC.ReturnTreasureSecondName();
            //Debug.Log("prizeNameSec " + prizeNameSec);
            ConstGameCtrl.instance.OnChangeHabar(prizeName, 1); // Maybe prizeNameSec!!!
            //ConstGameCtrl.instance.AddMineralToInventory(prizeName);
            TC.SelfDisable();
            PlayerResourceValueScript.Create(1, prnt);
        }

        if (other.gameObject.tag == "Resource")
        {
            //Debug.Log("Resource reached");
            int value = 0;
            string resName = other.gameObject.GetComponent<ResourceControl>().myName;
            //Debug.Log("Res name: " + resName);
            //ControlPanelCanvasScript CPCS = FindObjectOfType<ControlPanelCanvasScript>();

            if (resName == "Health")
            {
                int value_0 = (int)myDamagable.currentHealth;
                myDamagable.RestoreHealth();
                value = (int)myDamagable.currentHealth;
                //CPCS.UpdateHealth(myDamagable.currentHealth);
                PlayerResourceValueScript.Create(value-value_0, prnt);
            }

            if (resName == "Shield")
            {
                //Debug.Log("Shield reached");
                int value_0 = (int)myDamagable.currentShield;
                myDamagable.RestoreShield();
                value = (int)myDamagable.currentShield;
                //CPCS.UpdateShield(myDamagable.currentShield);
                PlayerResourceValueScript.Create(value - value_0, prnt);
            }

            if (resName == "Fuel")
            {
                int maxFuel = ConstGameCtrl.instance.GetMaxPlayerParam(ConstGameCtrl.PlayerShipUpgrades.fuel);
                value = maxFuel / 5;
                CommonSceneParams CSP = FindObjectOfType<CommonSceneParams>();
                CSP.AddFuel(value);
                value = (int)CSP.pFV;
                PlayerResourceValueScript.Create(value, prnt);
            }

            if (resName == "Money")
            {
                // Отправляем в ConstGameCtrl
                int rnd = Random.Range(0,6);
                int upgrader = 1;
                int lev = ConstGameCtrl.instance.CurrentLevel;
                if (lev <= 19)
                {
                    upgrader = 4;
                }
                if (lev <= 13)
                {
                    upgrader = 3;
                }
                if (lev <= 8)
                {
                    upgrader = 2;
                }
                if (lev <= 3)
                {
                    upgrader = 1;
                }
                value = (50 + 10 * rnd) * upgrader;
                PlayerResourceValueScript.Create(value, prnt);
                resName = "Gold";
            }

            ConstGameCtrl.instance.OnChangeHabar(resName, value);

            other.gameObject.GetComponent<ResourceControl>().SelfDisable();
        }
    }
}
