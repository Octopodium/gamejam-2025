using UnityEngine;

[RequireComponent(typeof(Iluminavel))]
public class Aparecedores : MonoBehaviour, Reacao {
    public Transform aparecedor;
    public Cores corAparece;

    public void Start() {
        aparecedor.gameObject.SetActive(false);
    }

    public void Reagir(Cores cor) {
        if (cor == corAparece) aparecedor.gameObject.SetActive(true);
        else aparecedor.gameObject.SetActive(false);
    }
}
