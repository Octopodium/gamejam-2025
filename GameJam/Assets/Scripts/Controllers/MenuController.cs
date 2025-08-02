using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject panelCredits;
    public string sceneName;

    public void PlayGameButton()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void PanelCredits()
    {
        panelCredits.SetActive(!panelCredits.activeInHierarchy);
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }
}
