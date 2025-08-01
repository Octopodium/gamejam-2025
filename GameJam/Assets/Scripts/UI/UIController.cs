using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public static UIController Instance;

    public Text indicador;
    Interagivel ultimoInteragivel;


    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
    }

    public void MostrarIndicador(Interagivel interagivel) {
        if (interagivel == null) {
            indicador.gameObject.SetActive(false);
            ultimoInteragivel = null;
            return;
        }

        if (ultimoInteragivel != interagivel) {
            indicador.text = "Pressione E para interagir";
            indicador.gameObject.SetActive(true);
            ultimoInteragivel = interagivel;
        }
    }
}
