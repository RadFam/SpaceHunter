using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleCross : MonoBehaviour
{
    public List<Transform> pointOfTeleport = new List<Transform>();
    public SpawnVortex spawnVortex;
    public SpaceShipControl player;

    private Transform spawn;
    private Transform exit;
    private Vector3 exitLine;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TeleportProcess();
        }
    }

    public void TeleportProcess()
    {
        player.FreezeAll();
        int ind = Random.Range(0, pointOfTeleport.Count);

        Vector3 suddenPoint = pointOfTeleport[ind].position;
        Quaternion suddenRotation = pointOfTeleport[ind].rotation;

        spawnVortex.transform.position = suddenPoint;
        spawnVortex.transform.rotation = suddenRotation;
        spawnVortex.gameObject.SetActive(true);
        spawn = spawnVortex.GetRespawn();
        exit = spawnVortex.GetExit();
        exitLine = exit.position - spawn.position;
        exitLine.Normalize();
        player.transform.position = spawn.position;
        player.transform.LookAt(exit.position, exit.up);
        StartCoroutine(PlayerExitFlight());
    }

    IEnumerator PlayerExitFlight()
    {
        while (Vector3.Distance(player.transform.position, exit.position) >= 1.0f)
        {
            player.transform.Translate(exitLine * 1f, Space.World);
            yield return new WaitForSeconds(0.01f);
        }

        spawnVortex.gameObject.SetActive(false);
        player.UnfreezeAll();
    }
}
