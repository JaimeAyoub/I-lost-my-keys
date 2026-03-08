using System;
using Lean.Touch;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    public TextMeshProUGUI endScoreText;
    public TextMeshProUGUI HighScoreText;


    public float remainingTime;

    public float maxTime;

    public Slider sliderTimeValiue;

    public GameObject EndCanvas;

    public bool isTimeRunOut = false;

    public AnimationUI animationUI;

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
            Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y) && !isKeying && !isTimeRunOut) //Swipe Izquierda 
        {
            AudioManager.instance.ResetPitch();
            StartKeying();
            AudioManager.instance.PlaySFX(SoundType.NextDoor, 0.7f);
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

        if (sliderTimeValiue != null)
        {
            sliderTimeValiue.maxValue = maxTime;
            sliderTimeValiue.value = 0;
        }

        ResetGame();
    }

    void Update()
    {
        if (!isTimeRunOut)
            addTimer();
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

    public void addTimer()
    {
        if (remainingTime < maxTime)
        {
            remainingTime += (1 * Time.deltaTime);
            sliderTimeValiue.value = remainingTime;
        }
        else
        {
            EndGame();
        }
    }

    public void SetHighScore(int score)
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
            HighScoreText.text = score.ToString();
        }
    }

    public void EndGame()
    {
        if (animationUI != null)
        {
            if (EndCanvas)
                animationUI.EndCanvas(EndCanvas);
        }

        endScoreText.text = correctKeys.ToString();
        SetHighScore(correctKeys);
        isTimeRunOut = true;
        animationUI.SubeGrid();
    }

    public void ResetGame()
    {
        remainingTime = 0;
        isTimeRunOut = false;
        correctKeys = 0;
        isKeying = false;
        correctKeysTextValue.text = correctKeys.ToString();
        GridManager.instance.ResetGrid();
        DoorsManager.instance.OpenDoor();
        animationUI.SubeEndCanvas(EndCanvas);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}