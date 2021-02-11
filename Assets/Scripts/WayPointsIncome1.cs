using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointsIncome1 : MonoBehaviour
{

    public static Transform[] pointsRecolector;

    void Awake()
    {

        pointsRecolector = new Transform[transform.childCount];
        for (int i = 0; i < pointsRecolector.Length; i++)
        {
            pointsRecolector[i] = transform.GetChild(i);

        }

    }

}
