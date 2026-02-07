using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class GridGenerator : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public float distanceBetweeninX = 1.0f;
    public float distanceBetweeninY = 1.0f;

    public GameObject box;


    private void Awake()
    {
        GenerateGrid();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void GenerateGrid()
    {
        int idGenerator = 0;
        for (float i = 0; i < width; i += distanceBetweeninX)
        {
            for (float j = 0; j < height; j += distanceBetweeninY)
            {
                GameObject newBox = Instantiate(box, new Vector3(i, j, 0), Quaternion.identity,
                    GridManager.instance.transform);
                newBox.GetComponent<BoxScript>().ID = idGenerator;
                newBox.name = "Box" + idGenerator;
                idGenerator++;
                GridManager.instance.boxList.Add(newBox);
            }
        }
    }
}