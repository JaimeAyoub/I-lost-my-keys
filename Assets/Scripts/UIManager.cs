using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject tutorialCanvas;

    void Start()
    {
        mainCanvas.SetActive(true);
        tutorialCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SwitchToTutorial()
    {
        mainCanvas.SetActive(false);
        tutorialCanvas.SetActive(true);
    }

    public void SwitchToMain()
    {
        mainCanvas.SetActive(true);
        tutorialCanvas.SetActive(false);
    }

    public void ChangeSceneToMain()
    {
        SceneManager.LoadSceneAsync(1);
    }
}