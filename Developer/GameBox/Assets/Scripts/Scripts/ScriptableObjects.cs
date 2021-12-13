using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptParametrs", menuName = "ScrParam", order = 51)]
public class ScriptableObjects : ScriptableObject
{
    [SerializeField] private float distanceMax;
    [SerializeField] private float distanceAll;
    [SerializeField] private float distanceScore;
    [SerializeField] private bool sceneActive;

    public float disMax
    {
        get
        {
            return distanceMax;
        }
        set
        {
            distanceMax = value;
        }
    }

    public float disAll
    {
        get
        {
            return distanceAll;
        }
        set
        {
            distanceAll = value;
        }
    }

    public float disScr
    {
        get
        {
            return distanceScore;
        }
        set
        {
            distanceScore = value;
        }
    }

    public bool cinemaOff
    {
        get
        {
            return sceneActive;
        }
        set
        {
            sceneActive = value;
        }

    }
}
