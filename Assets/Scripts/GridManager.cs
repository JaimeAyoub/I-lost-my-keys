using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Lean.Touch;
using UnityEditor;


public class GridManager : MonoBehaviour
{
    public static GridManager instance;

    public AnimationUI animationUI;

    public List<GameObject> boxList = new List<GameObject>();

    public List<int> SelectedIDs = new List<int>();

    public List<int> idToCompare = new List<int>();

    public float sensitivityToSwipe = 50.0f;

    public SpriteRenderer spriteKey;

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
        if (swipe.x > sensitivityToSwipe && Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y)) //Swipe dercha
        {
        }

        if (swipe.y > 0 && Mathf.Abs(swipe.y) > Mathf.Abs(swipe.x) &&
            GameManager.instance.isKeying) //Swipe hacia arriba.
        {
            Compare();
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
            hit.collider.gameObject.GetComponent<BoxScript>().CheckSelected();
        }
    }

    public void addSelected(GameObject box)
    {
        if (!SelectedIDs.Contains(box.GetComponent<BoxScript>().ID))
            SelectedIDs.Add(box.GetComponent<BoxScript>().ID);


        int maxCombo = 15;


        float progress = (float)SelectedIDs.Count / maxCombo;

        float targetPitch = Mathf.Lerp(1.5f, 2.5f, progress);

        GameManager.instance.CameraShake(0.25f, 0.25f, 0.03f);
        AudioManager.instance.PlaySFXWithPitch(0, targetPitch);
    }

    public void removeSelected(GameObject box)
    {
        if (SelectedIDs.Contains(box.GetComponent<BoxScript>().ID))
            SelectedIDs.Remove(box.GetComponent<BoxScript>().ID);
        GameManager.instance.CameraShake(0.25f, 0.25f, 0.03f);
    }


    void Compare()
    {
        int correctBoxes = 0;
        int incorrectBoxes = 0;
        List<BoxScript> incorrectBoxList = new List<BoxScript>();

        foreach (var t in SelectedIDs)
        {
            if (idToCompare.Contains(t))
            {
                correctBoxes++;
            }
            else
            {
                incorrectBoxes++;
                incorrectBoxList.Add(returnBoxByID(t));
            }
        }

        Debug.Log(incorrectBoxes);
        if (correctBoxes == idToCompare.Count && incorrectBoxes == 0)
        {
            GameManager.instance.CorrectKey();
        }
        else
        {
            Incorrect(incorrectBoxList);
        }
    }

    public void ResetGrid()
    {
        animationUI.SubeGrid();
        EmptySelectedIDs();
        UnselectBoxes();
    }

    public void SetNewKey(KeyScriptableObject key)
    {
        key = KeyManager.instance.GiveRandomKey();
        setIDs(key.IDs);
        SetKeySprite(key.sprite);
    }

    private void setIDs(List<int> IDs)
    {
        idToCompare.Clear();
        foreach (var key in IDs)
        {
            idToCompare.Add(key);
        }
    }

    private void SetKeySprite(Sprite sprite)
    {
        spriteKey.sprite = sprite;
    }

    private void EmptySelectedIDs()
    {
        SelectedIDs.Clear();
    }

    private void UnselectBoxes()
    {
        foreach (var box in boxList)
        {
            box.GetComponent<BoxScript>().Deselected();
        }
    }

    private void Incorrect(List<BoxScript> incorrectBoxList)
    {
        animationUI.IncorrectAnimation();
        if (incorrectBoxList.Count > 0)
        {
            foreach (BoxScript box in incorrectBoxList)
                box.AnimateWrongAnimation();
        }
    }

    public void StartNewDoor()
    {
        SetNewKey(KeyManager.instance.GiveRandomKey());
        animationUI.BajaGrid();
    }

    public BoxScript returnBoxByID(int id)
    {
        foreach (var t in boxList)
        {
            if (t.GetComponent<BoxScript>().ID == id)
            {
                return t.GetComponent<BoxScript>();
            }
        }

        return null;
    }
}