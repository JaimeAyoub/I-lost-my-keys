using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public bool isSelected = false;
    public int ID;

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
        newColor.a = 0.5f;
        sr.color = newColor;
        isSelected = true;
        GridManager.instance.addSelected(this.gameObject);
    }

    public void Deselected()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = Color.white;
        Color newColor = sr.color;
        newColor.a = 0.5f;
        sr.color = newColor;
        isSelected = false;
        GridManager.instance.removeSelected(this.gameObject);
    }
}