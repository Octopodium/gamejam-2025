using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Referências dos Paines do Menu")]
    public GameObject panelCredits;
    public GameObject panelSlider;
    public string sceneName;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayGameButton()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void PanelCredits()
    {
        panelCredits.SetActive(!panelCredits.activeInHierarchy);
    }

    public void ActiveVolumeSlider()
    {
        panelSlider.SetActive(!panelSlider.activeInHierarchy);  
    }

    public void SliderControl(float value)
    {
        audioSource.volume = value;
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }
}
