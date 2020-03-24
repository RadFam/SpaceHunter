using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SavebleEntity
{
    private int health;
    private int shield;
    private int fuel;
    private int radar;
    private int engine;
    private int maneuver;
    private int weapon;
    private int progress;
    private int money;
    private List<string> minerals = new List<string>();
    public SavebleEntity()
    {

    }

    public void UpdateData()
    {
        health = ConstGameCtrl.instance.PHealth;
        shield = ConstGameCtrl.instance.PShield;
        fuel = ConstGameCtrl.instance.PFuel;
        radar = ConstGameCtrl.instance.PRadar;
        engine = ConstGameCtrl.instance.PEngine;
        maneuver = ConstGameCtrl.instance.PManeuver;
        weapon = ConstGameCtrl.instance.PWeapon;
        progress = ConstGameCtrl.instance.PProgress;
        money = ConstGameCtrl.instance.PMoney;

        minerals.Clear();
        minerals = ConstGameCtrl.instance.PMinerals;
    }

    public void ReloadData()
    {
        ConstGameCtrl.instance.PHealth = health;
        ConstGameCtrl.instance.PShield = shield;
        ConstGameCtrl.instance.PFuel = fuel;
        ConstGameCtrl.instance.PRadar = radar;
        ConstGameCtrl.instance.PEngine = engine;
        ConstGameCtrl.instance.PManeuver = maneuver;
        ConstGameCtrl.instance.PWeapon = weapon;
        ConstGameCtrl.instance.PProgress = progress;
        ConstGameCtrl.instance.PMoney = money;
        ConstGameCtrl.instance.PMinerals = minerals;
    }
}
