using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListPathPoints : MonoBehaviour
{
    [Serializable]
    public struct AllWayPaths
    {
        public int pathNum;
        public List<Transform> pathPoints;

        public AllWayPaths(int num, List<Transform> trnsfrm)
        {
            pathNum = num;
            pathPoints = new List<Transform>();
            foreach (Transform trn in trnsfrm)
            {
                pathPoints.Add(trn);
            }
        }
    }

    [Header("Number of path / List of points")]
    public List<AllWayPaths> allPaths = new List<AllWayPaths>();
    private int keyNum = 0;

    public void InputPath(List<Transform> points)
    {
        allPaths.Add(new AllWayPaths(keyNum, points));
        keyNum++;
    }

    public List<Transform> GetPath(int num) => allPaths[num].pathPoints;
}
