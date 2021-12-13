using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    [SerializeField] private GameObject[] roadPrefab;
    [SerializeField] private GameObject[] trapsPrefab;
    [SerializeField] private GameObject[] bonusPrefab;
    [SerializeField] private GameObject[] grassPrefab;
    [SerializeField] private GameObject[] decorPrefab;
    [SerializeField] private GameObject[] linePrefab;
    [SerializeField] private GameObject[] holePrefab;
    [SerializeField] private GameObject finish;

    [SerializeField] private Transform playerPosition;

    private List<GameObject> roadActive = new List<GameObject>();

    private int max;
    private int maxTrap = 0;
    private int maxBonus = 0;
    private int spawnPos;
    private int spawnDecor;

    private int roadLenght = 2;
    private int size;
    private int maxRoads = 110;

    private bool nullObj;
    private bool stopCreate = false;

    private void Awake()
    {
        max = player.MaxRoads();
        size = (max - 1) / 2;
        spawnPos = 8;
        spawnDecor = spawnPos;

        for (int i = 0; i < maxRoads - 4; i++)
        {
            CreateRoad();
            spawnPos += roadLenght;
            nullObj = !nullObj;
        }

        if (spawnPos - 10 == maxRoads * roadLenght && !stopCreate)
        {
            CreateRoad();
            CreateDecor();
            CreateDecor();
            stopCreate = true;
            Instantiate(finish, new Vector3(0, -1, spawnPos + 2), transform.rotation);
            
        }
    }

    private void CreateRoad()
    {
        if (!nullObj)
        {
            CreateDecor();
        }

        if (spawnPos == 42)
        {
            spawnPos += 8;
            GameObject nextRoad = Instantiate(holePrefab[Random.Range(0, holePrefab.Length)], new Vector3(0, 0, spawnPos), transform.rotation);
            roadActive.Add(nextRoad);
            spawnPos += 2;
        }

        for (int i = - size; i < size + 1; i++)
        {
            GameObject[] massive = roadPrefab;
            GameObject nextRoad = Instantiate(massive[Random.Range(0, massive.Length)], new Vector3(i * roadLenght, 0, spawnPos), transform.rotation);
            roadActive.Add(nextRoad);
            CreateTraps(i);
        }

        maxTrap = 0;
        maxBonus = 0;
        max = player.MaxRoads();
        size = (max - 1) / 2;
    }

    private void DeleteRoad()
    {
        for (int i = 0; i < size - 1; i++)
        {
            Destroy(roadActive[i]);
            roadActive.RemoveAt(i);
        }
    }

    private void CreateDecor()
    {
        GameObject nextRoad = Instantiate(linePrefab[Random.Range(0, linePrefab.Length)], new Vector3(-size * roadLenght - roadLenght * 2 + 1, 0, spawnDecor + 1), transform.rotation);
        roadActive.Add(nextRoad);

        Quaternion a = Quaternion.Euler(0,-180,0);

        nextRoad = Instantiate(linePrefab[Random.Range(0, linePrefab.Length)], new Vector3(size * roadLenght + roadLenght + 1f, 0, spawnDecor + 1), a);
        roadActive.Add(nextRoad);
        spawnDecor += 4;
    }

    private void CreateTraps(int i)
    {
        GameObject[] massive = roadPrefab;

        if (maxBonus > 1 && maxTrap > 2)
        {
            massive = grassPrefab;
        }
        else
        {
            int a = Random.Range(1, 4);

            switch (a)
            {
                case 1:
                    if (maxTrap == max - size || nullObj || maxBonus == 0)
                    {
                        massive = bonusPrefab;
                        maxBonus++;
                    }
                    else
                    {
                        massive = trapsPrefab;
                        maxTrap++;
                    }
                    break;

                case 2:
                    if (maxTrap < max - 1 && !nullObj)
                    {
                        massive = trapsPrefab;
                        maxTrap++;
                    }
                    else
                    {
                        massive = bonusPrefab;
                        maxBonus++;
                    }
                    break;

                case 3:
                    massive = grassPrefab;
                    break;
            }

        }

        GameObject nextRoad = Instantiate(massive[Random.Range(0, massive.Length)], new Vector3(i * roadLenght, 0, spawnPos), transform.rotation);
        roadActive.Add(nextRoad);
    }
}
