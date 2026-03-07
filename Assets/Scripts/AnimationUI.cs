using System;
using UnityEngine;
using DG.Tweening;
using Unity.Cinemachine;
using System.Threading.Tasks;

public class AnimationUI : MonoBehaviour
{
    public AnimationCurve animationCurve;

    public float animationDuration;

    public Transform objetctToAnimate;

    public Tween currentTween;

    public CinemachineVirtualCameraBase cinemachine;
    private CinemachineBasicMultiChannelPerlin noise;

    


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

    public void BajaGrid()
    {
        Vector2 startPosition = endPosition;
        float offSetY = 10;

        startPosition = new Vector2(endPosition.x, startPosition.y + offSetY);

        objetctToAnimate.position = startPosition;
        objetctToAnimate.gameObject.SetActive(true);
        currentTween?.Kill();

        currentTween = objetctToAnimate.DOMoveY(endPosition.y, animationDuration).SetEase(animationCurve);
    }

    public void SubeGrid()
    {
        currentTween?.Kill();
        currentTween = objetctToAnimate.DOMoveY(objetctToAnimate.transform.position.y + 10, animationDuration)
            .SetEase(animationCurve);
    }

    public void IncorrectAnimation()
    {
        currentTween?.Kill();
        currentTween = objetctToAnimate.DOMoveY(objetctToAnimate.transform.position.y + 5, animationDuration)
            .SetEase(animationCurve).OnComplete(() =>
            {
                GameManager.instance.CameraShake(1.0f,1.0f,0.25f);
                objetctToAnimate.DOMoveY(0, animationDuration).SetEase(animationCurve);
            });
    }


}