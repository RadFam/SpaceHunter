using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetMapScript : MonoBehaviour
{
    public GameObject framePrefab;

    public List<GameObject> frames = new List<GameObject>();
    
    public void ReloadEnemies(List<Vector3> coords, List<float> scales)
    {
        // Здесь лучше сделать пул объектов !!!!

        frames.Clear();
        for (int i = 0; i < coords.Count; ++i)
        {
            GameObject newFrame = Instantiate(framePrefab);
            newFrame.SetActive(true);
            newFrame.transform.position = coords[i];
            newFrame.transform.localScale = new Vector3(scales[i], scales[i], 0.0f);
            frames.Add(newFrame);
        }
    }

    public void MoveEnemies(List<Vector3> coords, List<float> scales)
    {
        for (int i = 0; i < coords.Count; ++i)
        {
            frames[i].transform.position = coords[i];
            frames[i].transform.localScale = new Vector3(scales[i], scales[i], 0.0f);
        }
    }
}
