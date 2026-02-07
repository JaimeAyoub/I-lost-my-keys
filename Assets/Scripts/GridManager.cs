using System;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;

    public List<GameObject> boxList = new List<GameObject>();

    public List<GameObject> boxSelectedlist = new List<GameObject>();

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
        if (swipe.y > Mathf.Abs(swipe.x))
        {
            returnSelected();
        }
    }

    void Start()
    {
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
            Debug.Log("Diste clic en: " + hit.collider.gameObject.name);
            hit.collider.gameObject.GetComponent<BoxScript>().CheckSelected();
        }
    }

    public void addSelected(GameObject box)
    {
        if (!boxSelectedlist.Contains(box))
            boxSelectedlist.Add(box);
    }

    public void removeSelected(GameObject box)
    {
        if (boxSelectedlist.Contains(box))
            boxSelectedlist.Remove(box);
    }

    public void returnSelected()
    {
        if (boxSelectedlist.Count > 0)
        {
            foreach (GameObject box in boxSelectedlist)
            {
                Debug.Log("Los cuadros elegidos son: " + box.GetComponent<BoxScript>().ID);
            }
        }
        else
        {
            Debug.Log("Lista vacia");
        }
    }
}