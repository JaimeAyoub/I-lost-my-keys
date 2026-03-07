using System;
using Lean.Touch;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isKeying = false;

    public bool isKeyCorrect = false;

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

    protected virtual void OnEnable()
    {
        LeanTouch.OnFingerSwipe += HandleFingerSwipe;
    }


    private void HandleFingerSwipe(LeanFinger finger)
    {
        Vector2 swipe = finger.SwipeScreenDelta;
        Debug.Log(swipe.y);
        if (swipe.x < -GridManager.instance.sensitivityToSwipe &&
            Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y) && !isKeying) //Swipe Izquierda 
        {
           StartKeying();
        }
    }

    protected virtual void OnDisable()
    {
        LeanTouch.OnFingerSwipe -= HandleFingerSwipe;
    }


    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    void Update()
    {
    }

    public void StartKeying()
    {
        isKeyCorrect = false;
        isKeying = true;
        GridManager.instance.StartNewDoor();
        DoorsManager.instance.NextDoor();
    }

    public void CorrectKey()
    {
        GridManager.instance.ResetGrid();
        isKeyCorrect = true;
        isKeying = false;
    }
}