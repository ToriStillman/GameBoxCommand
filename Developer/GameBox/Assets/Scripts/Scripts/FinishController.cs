using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().StopSpeed();
            ChooseLevel();
        }
    }

    public void ChooseLevel()
    {
        var a = GameObject.Find("Player");
        var level = a.GetComponent<LevelController>();
        var coals = a.GetComponent<BonusController>().maxCoal();
        var presents = a.GetComponent<BonusController>().maxPresent();

        if (coals > presents)
        {
            level.NextScene(2);
        }
        else if (coals < presents)
        {
            level.NextScene(1);
        }
    }
}
