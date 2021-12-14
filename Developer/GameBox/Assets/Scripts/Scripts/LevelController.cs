using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject cutscene;
    [SerializeField] private List<ScriptableObjects> dataList;

    private void Awake()
    {
        OffScene();
        dataList[0].cinemaOff = true;
    }

    public void ReloadScene()
    {
        var gameD = GetComponent<DistanceController>();
        gameD.LoadAllDis();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OffScene()
    {
        if (dataList[0].cinemaOff == true)
        {
            //cutscene.SetActive(false);
        }
    }

    public void NextScene(int num)
    {
        SceneManager.LoadScene(num);
    }

}
