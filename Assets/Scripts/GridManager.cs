using System;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;


public class GridManager : MonoBehaviour
{
    public static GridManager instance;

    public AnimationUI animationUI;

    public List<GameObject> boxList = new List<GameObject>();

    public List<int> SelectedIDs = new List<int>();

    public List<int> idToCompare = new List<int>();

    public float sensitivityToSwipe = 50.0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    protected virtual void OnEnable()
    {
        LeanTouch.OnFingerSwipe += HandleFingerSwipe;
        LeanTouch.OnFingerTap += CheckCollision;
    }

    protected virtual void OnDisable()
    {
        LeanTouch.OnFingerSwipe -= HandleFingerSwipe;

        LeanTouch.OnFingerTap -= CheckCollision;
    }

    private void HandleFingerSwipe(LeanFinger finger)
    {
        Vector2 swipe = finger.SwipeScreenDelta;
        Debug.Log(swipe.y);
        if (swipe.y < -sensitivityToSwipe && Mathf.Abs(swipe.y) > Mathf.Abs(swipe.x))
        {
            animationUI.ToptoDownAnimation();
        }

        if (swipe.y > 0 && Mathf.Abs(swipe.y) > Mathf.Abs(swipe.x))
        {
            Compare();
            animationUI.DownToTopAnimation();
        }
    }

    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckCollision(LeanFinger finger)
    {
        Vector3 fingerPos = Camera.main.ScreenToWorldPoint(finger.ScreenPosition);
        Vector2 fingerPosWorld = new Vector2(fingerPos.x, fingerPos.y);

        RaycastHit2D hit = Physics2D.Raycast(fingerPosWorld, Vector2.zero);
        if (hit.collider != null)
        {
            hit.collider.gameObject.GetComponent<BoxScript>().CheckSelected();
        }
    }

    public void addSelected(GameObject box)
    {
        if (!SelectedIDs.Contains(box.GetComponent<BoxScript>().ID))
            SelectedIDs.Add(box.GetComponent<BoxScript>().ID);
    }

    public void removeSelected(GameObject box)
    {
        if (SelectedIDs.Contains(box.GetComponent<BoxScript>().ID))
            SelectedIDs.Remove(box.GetComponent<BoxScript>().ID);
    }

    public void returnSelected()
    {
        if (SelectedIDs.Count > 0)
        {
            foreach (var box in SelectedIDs)
            {
                Debug.Log("Los cuadros elegidos son: " + box);
            }
        }
        else
        {
            Debug.Log("Lista vacia");
        }
    }

    void Compare()
    {
        int correctBoxes = 0;


        foreach (var t in idToCompare)
        {
            if (SelectedIDs.Contains(t))
            {
                correctBoxes++;
            }
            else
            {
                break;
            }
        }

        if (correctBoxes == idToCompare.Count)
        {
            Correcto();
        }
    }

    void Correcto()
    {
        Debug.Log("Correcto");
    }
}