using UnityEngine;

public class DoorObject : MonoBehaviour
{

    public bool hasOpened = false;
    private SpriteRenderer spriteRenderer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor()
    {
        hasOpened = true;
    }
}
