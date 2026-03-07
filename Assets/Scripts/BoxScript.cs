using UnityEngine;
using DG.Tweening;

public class BoxScript : MonoBehaviour
{
    public bool isSelected = false;
    public int ID;
    Tween tween;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CheckSelected()
    {
        if (!isSelected)
        {
            Selected();
        }

        else
        {
            Deselected();
        }
    }

    public void Selected()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = Color.green;
        Color newColor = sr.color;
        newColor.a = 0.25f;
        sr.color = newColor;
        isSelected = true;
        GridManager.instance.addSelected(this.gameObject);
    }

    public void Deselected()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = Color.white;
        Color newColor = sr.color;
        newColor.a = 0.0f;
        sr.color = newColor;
        isSelected = false;
        GridManager.instance.removeSelected(this.gameObject);
    }

    public void AnimateWrongAnimation()
    {
        tween.Kill();
        Color actualColor;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        actualColor = sr.color;
       tween =  sr.DOColor(Color.red, 0.3f).SetLoops(2, LoopType.Yoyo);
    }

    public void NeedsToSelectAnimation()
    {
        DOTween.KillAll();
        Color actualColor;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        actualColor = sr.color;
        sr.DOColor(Color.blue, 0.3f).SetLoops(2, LoopType.Yoyo);
    }
}