using NUnit.Framework;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public float distanceBetweeninX = 1.0f;
    public float distanceBetweeninY = 1.0f;

    public GameObject box;

    public GameObject[] boxList;

    void Start()
    {
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateGrid()
    {
        for(float i = 0; i<width; i+=distanceBetweeninX)
        {
            for(float j = 0; j < height; j+= distanceBetweeninY)
            {
                Instantiate(box, new Vector3(i, j, 0), Quaternion.identity);
                
            }
        }
    }


}
