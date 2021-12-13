using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceController : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private List<ScriptableObjects> dataList;

    //[SerializeField] private Text distanceText;
    //[SerializeField] private Text distanceText2;
    //[SerializeField] private Text distanceTextAll;
    //[SerializeField] private Text distanceTextScore;
    //[SerializeField] private Text distanceRes;

    [SerializeField] private int score;

    private Transform playerPos;

    private float startDistance;
    private float nextDistance;
    private float distanceNow;
    private float maxScore;

    private int bonusDist;

    private void Start()
    {
        startDistance = 0;
        distanceNow = startDistance;
        nextDistance = 200;
    }

    private void Update()
    {
        //distanceText.text = Mathf.Round(distanceNow).ToString();

        playerPos = player.transform;
        distanceNow = playerPos.position.z;

        if (Mathf.Round(distanceNow) == nextDistance)
        {
            NewSpeed();
            nextDistance = distanceNow * 2;
            bonusDist += 1000;
        }
    }

    private void NewSpeed()
    {
        player.GetSpeed(1);
    }

    public void LoadAllDis()
    {
        //distanceRes.text = ("Distance " + Mathf.Round(distanceNow) + " * " + (score + bonusDist)).ToString();

        var sc = GetComponent<BonusController>();
        int a = sc.ScoreBonus();

        maxScore = a + (score * Mathf.Round(distanceNow));
        //distanceText2.text = maxScore.ToString();

        dataList[0].disMax += Mathf.Round(distanceNow);

        if (distanceNow > dataList[2].disScr)
        {
            dataList[2].disScr = Mathf.Round(distanceNow);
        }

        if (maxScore >= dataList[1].disAll)
        {
            dataList[1].disAll = maxScore;
        }
    }

    public void LoadMAxDis()
    {
        //distanceTextAll.text = dataList[1].disAll.ToString();
        //distanceTextScore.text = dataList[2].disScr.ToString();
    }

}
