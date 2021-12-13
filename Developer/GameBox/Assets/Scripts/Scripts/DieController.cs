using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieController : MonoBehaviour
{
    [SerializeField] private GameObject deathMenu;
    //[SerializeField] private GameObject menuGo;
    [SerializeField] private GameObject blackBack;

    public void DeathScreen()
    {
        deathMenu.SetActive(true);
        blackBack.SetActive(true);
        //menuGo.SetActive(false);
    }
}
