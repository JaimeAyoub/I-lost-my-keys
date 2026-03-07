using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Lean.Touch;
using DG.Tweening;
using System;

public class DoorsManager : MonoBehaviour
{
    public static DoorsManager instance;

    public GameObject doorPrefab;
    public GameObject doorOpenPrefab;
    public Transform spawnPoint;

    public int doorsToSpawn = 5;

    public float spaceBetweenDoors;
    public List<GameObject> doorsQueue;

    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
   

    void Start()
    {
        GenerateFirstDoors();
    }


    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     Debug.Log(doorsQueue.Count);
        // }

        // if (Input.GetKeyDown(KeyCode.R))
        // {
        //     NextDoor();
        // }
    }

    void GenerateFirstDoors()
    {
        float separation = 0;
        for (int i = 0; i < doorsToSpawn; i++)
        {
            GameObject newDoor = Instantiate(doorPrefab,
                new Vector3(spawnPoint.position.x + separation, spawnPoint.position.y, spawnPoint.position.z)
                , Quaternion.identity, this.transform);
            doorsQueue.Add(newDoor);

            separation += spaceBetweenDoors;
        }
    }

    public void NextDoor()
    {
        for (int i = 0; i < doorsQueue.Count; i++)
        {
            if (i == 0)
            {
                doorsQueue[i].GetComponent<SpriteRenderer>().sprite =
                    doorOpenPrefab.GetComponent<SpriteRenderer>().sprite;
                doorsQueue[i].transform.DOLocalMoveX(doorsQueue[i].transform.localPosition.x - 5.0f, 0.25f)
                    .OnComplete(() =>
                    {
                        Destroy(doorsQueue[0]);
                        doorsQueue.RemoveAt(0);
                    });
            }
            else
            {
                doorsQueue[i].transform.DOLocalMoveX(doorsQueue[i - 1].transform.localPosition.x, 0.25f);
            }
        }
        addDoorToList();
    }

    void addDoorToList()
    {
        GameObject newDoor = Instantiate(doorPrefab,
            new Vector3(doorsQueue[doorsQueue.Count - 1].transform.position.x + spaceBetweenDoors,
                doorsQueue[doorsQueue.Count - 1].transform.position.y,
                doorsQueue[doorsQueue.Count - 1].transform.position.z)
            , Quaternion.identity, this.transform);
        doorsQueue.Add(newDoor);
    }
}