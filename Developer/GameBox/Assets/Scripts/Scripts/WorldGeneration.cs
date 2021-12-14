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
    private int count = 0;
    private int holeSpawn;

    private bool nullObj;
    private bool holeOk = false;
    private bool nullLine;
    private bool stopCreate = false;

    private void Awake()
    {
        max = player.MaxRoads();
        size = (max - 1) / 2;
        spawnPos = 8;
        spawnDecor = spawnPos;
        holeSpawn = 70;

        for (int i = 0; i < maxRoads - 4; i++)
        {
            
            CreateRoad();

            if (!nullObj)
            {
                CreateDecor();
            }

            if (holeOk)
            {
                holeSpawn = 150;
            }

            if (spawnPos % 30 == 0)
            {
                Present();
            }

            spawnPos += roadLenght;
            nullObj = !nullObj;

        }

        if (spawnPos - 20 == maxRoads * roadLenght && !stopCreate)
        {
            CreateDecor();
            CreateDecor();
            CreateDecor();
            CreateDecor();
            CreateDecor();
            Instantiate(finish, new Vector3(0, -1, spawnPos), transform.rotation);
            stopCreate = true;

        }
    }

    private void CreateRoad()
    {
        count++;

        if (count == 5)
        {
            nullLine = false;
            count = 0;
        }
        else
        {
            nullLine = true;
        }

        if (spawnPos == holeSpawn)
        {
            spawnPos += 8;
            Instantiate(holePrefab[Random.Range(0, holePrefab.Length)], new Vector3(0, 0, spawnPos), transform.rotation);
            spawnPos += 2;
            holeOk = true;
        }

        for (int i = - size; i < size + 1; i++)
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
        Quaternion a = Quaternion.Euler(0,-180,0);

        Instantiate(linePrefab[Random.Range(0, linePrefab.Length)], new Vector3(size * roadLenght + roadLenght + 1f, 0, spawnDecor + 1), a);
        spawnDecor += 4;
    }

    private void CreateTraps(int i)
    {
        GameObject[] massive = roadPrefab;

        if (maxTrap == 2)
        {
            massive = grassPrefab;
        }
        else
        {
            int a = Random.Range(1, 3);

            switch (a)
            {
                case 1:
                    massive = trapsPrefab;
                    maxTrap++;
                    break;

                case 2:
                    if (maxTrap < 2)
                    {
                        massive = trapsPrefab;
                        maxTrap++;
                    }
                    else
                    {
                        massive = grassPrefab;
                    }
                    break;
            }

        }

        Instantiate(massive[Random.Range(0, massive.Length)], new Vector3(i * roadLenght, 0, spawnPos), transform.rotation);
    }

    private void Present()
    {
        if (maxBonus < 1)
        {
            int b = Random.Range(0, 2);
            int c = 0;

            for (int k = 0; k < 4; k++)
            {
                Instantiate(bonusPrefab[b], new Vector3(roadLenght, 0, spawnPos + c), transform.rotation);
                c += 2;
            }
            maxBonus++;
        }
    }
}
