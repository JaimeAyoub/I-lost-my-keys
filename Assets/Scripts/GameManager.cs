using System;
using Lean.Touch;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isKeying = false;

    public bool isKeyCorrect = false;

    public int correctKeys = 0;

    public CinemachineVirtualCameraBase cinemachine;
    private CinemachineBasicMultiChannelPerlin noise;


    public TextMeshProUGUI correctKeysTextValue;

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
        
        if (cinemachine != null)
        {
            noise = cinemachine.GetComponent<CinemachineBasicMultiChannelPerlin>();
        }
        else
        {
            Debug.LogWarning("No se asigno Cinemachine tonto");
        }
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
        DoorsManager.instance.OpenDoor();
        isKeyCorrect = true;
        isKeying = false;
        correctKeys++;
        if (correctKeysTextValue != null)
        {
            correctKeysTextValue.text = correctKeys.ToString();
        }
    }

    public async Awaitable CameraShake(float amplitude, float frequency, float duration)
    {
        noise.AmplitudeGain = amplitude;
        noise.FrequencyGain = frequency;
        await Task.Delay((int)(duration * 1000));
        noise.AmplitudeGain = 0;
        noise.FrequencyGain = 0;
    }
}