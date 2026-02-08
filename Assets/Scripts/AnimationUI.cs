using System;
using UnityEngine;
using DG.Tweening;

public class AnimationUI : MonoBehaviour
{
    public AnimationCurve animationCurve;

    public float animationDuration;

    public Transform objetctToAnimate;

    public Tween currentTween;

    public Vector2 endPosition;

    private void Awake()
    {
        if (objetctToAnimate)
        {
            endPosition = objetctToAnimate.position;
            objetctToAnimate.gameObject.SetActive(false);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartAnimation()
    {
        Vector2 startPosition = endPosition;
        float offSetY = 1080;

        startPosition = new Vector2(endPosition.x, startPosition.y + offSetY);

        objetctToAnimate.position = startPosition;
        objetctToAnimate.gameObject.SetActive(true);
        currentTween?.Kill();

        currentTween = objetctToAnimate.DOMoveY(endPosition.y, animationDuration).SetEase(animationCurve);
    }
}