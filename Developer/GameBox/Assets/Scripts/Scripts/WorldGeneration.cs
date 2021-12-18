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
    [SerializeField] private int maxRoads;

    [SerializeField] private Transform playerPosition;

    private int max;
    private int maxTrap = 0;
    private int maxBonus = 0;
    private int spawnPos;
    private int spawnDecor;

    private int roadLenght = 2;
    private int size;

    private int count = 0;
    private int holeSpawn;

    private bool nullObj;
    private bool nullLine;
    private bool stopCreate = false;

    private void Awake()
    {
        max = player.MaxRoads();
        size = (max - 1) / 2;
        spawnPos = 8;
        spawnDecor = spawnPos;

        for (int i = 0; i < maxRoads; i++)
        {
            if (!stopCreate)
            {
                CreateRoad();

                if (spawnPos % 30 == 0)
                {
                    Present();
                }

                spawnPos += roadLenght;
                nullObj = !nullObj;
            }

            if (spawnPos - (holeSpawn * 10) == maxRoads * roadLenght && !stopCreate)
            {
                Instantiate(finish, new Vector3(0, -1, spawnPos), transform.rotation);
                stopCreate = true;
            }
        }

        for (int i = 0; i < maxRoads / 2 - 2; i++)
        {
            if (!nullObj)
            {
                CreateDecor();
            }
        }

    }

    private void CreateRoad()
    {
        count++;

        if (count == 4)
        {
            nullLine = false;
            count = 0;
        }
        else
        {
            nullLine = true;
        }

        if (spawnPos % 100 == 0 && spawnPos < maxRoads * 2)
        {
            HoleCreate();
        }

        for (int i = -size; i < size + 1; i++)
        {
            GameObject[] massive = roadPrefab;
            Instantiate(massive[Random.Range(0, massive.Length)], new Vector3(i * roadLenght, 0, spawnPos), transform.rotation);

            if (!nullLine)
            {
                CreateTraps(i);
            }

        }

        maxTrap = 0;
        maxBonus = 0;
        max = player.MaxRoads();
        size = (max - 1) / 2;
    }

    private void CreateDecor()
    {
        Instantiate(linePrefab[Random.Range(0, linePrefab.Length)], new Vector3(-size * roadLenght - roadLenght * 2 + 1, 0, spawnDecor + 1), transform.rotation);
        Quaternion a = Quaternion.Euler(0, -180, 0);

        Instantiate(linePrefab[Random.Range(0, linePrefab.Length)], new Vector3(size * roadLenght + roadLenght + 1f, 0, spawnDecor + 1), a);
        spawnDecor += 4;
    }

    private void CreateTraps(int i)
    {
        GameObject[] massive = roadPrefab;

        int a = Random.Range(1, 4);

        switch (a)
        {
            case 1:
                massive = grassPrefab;
                break;

            case 2:
                if (maxTrap < 1)
                {
                    massive = trapsPrefab;
                    maxTrap++;
                }
                else massive = grassPrefab;
                break;

            case 3:
                if (maxTrap < 2)
                {
                    massive = trapsPrefab;
                    maxTrap++;
                }
                else massive = grassPrefab;
                break;
        }

        if (maxTrap < 1 && i == size)
        {
            massive = trapsPrefab;
            maxTrap++;
        }

        Instantiate(massive[Random.Range(0, massive.Length)], new Vector3(i * roadLenght, 0, spawnPos), transform.rotation);
    }

    private void Present()
    {
        int[] massive = new int[3] { -2, 0, 2 };

        if (maxBonus < 1)
        {
            int a = Random.Range(0, 3);
            int b = Random.Range(0, 2);
            int c = 0;

            for (int k = 0; k < 4; k++)
            {
                Instantiate(bonusPrefab[b], new Vector3(massive[a], 0, spawnPos + c), transform.rotation);
                c += 2;
            }
            maxBonus++;
        }
    }

    private void HoleCreate()
    {
        spawnPos += 8;
        Instantiate(holePrefab[Random.Range(0, holePrefab.Length)], new Vector3(0, 0, spawnPos), transform.rotation);
    }
}


