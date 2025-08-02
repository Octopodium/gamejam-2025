using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public string sceneName;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ResumeButton()
    {
        GameManager.Instance.Despausar();
    }

    public void BackToMenuButton()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SliderControl(float value)
    {
        audioSource.volume = value;
    }

}
