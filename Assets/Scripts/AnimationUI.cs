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

    [Header("----Valores para Camara Shake---")]
    public float shakeTimer;

    public float intensityCameraShake;
    public float amplitudeCameraShake;


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
        if (cinemachine != null)
        {
            noise = cinemachine.GetComponent<CinemachineBasicMultiChannelPerlin>();
        }
        else
        {
            Debug.LogWarning("No se asigno Cinemachine tonto");
        }
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
                CameraShake();
                objetctToAnimate.DOMoveY(0, animationDuration).SetEase(animationCurve);
            });
    }

    public async Awaitable CameraShake()
    {
        noise.AmplitudeGain = amplitudeCameraShake;
        noise.FrequencyGain = intensityCameraShake;
        await Task.Delay((int)(shakeTimer * 1000));
        noise.AmplitudeGain = 0;
        noise.FrequencyGain = 0;
    }
}