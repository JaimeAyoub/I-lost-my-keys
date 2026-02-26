using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class DoorsManager : MonoBehaviour
{
    public static DoorsManager instance;

    public GameObject doorPrefab;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(doorsQueue.Count);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            NextDoor();
        }
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

    void NextDoor()
    {
        
        for (int i = 1; i < doorsQueue.Count ; i++)
        {
            doorsQueue[i].transform.DOLocalMoveX(doorsQueue[i - 1].transform.localPosition.x,0.25f) ;
        }
        Destroy(doorsQueue[0]);
        doorsQueue.RemoveAt(0);
        

      
    }
}