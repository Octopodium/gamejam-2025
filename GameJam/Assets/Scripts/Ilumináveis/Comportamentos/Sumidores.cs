using UnityEngine;

[RequireComponent(typeof(Iluminavel))]
public class Sumidores : MonoBehaviour, Reacao {
    public Transform sumidor;
    public Cores corAparece;

    public void Start() {
        sumidor.gameObject.SetActive(true);
    }

    public void Reagir(Cores cor) {
        if (cor != corAparece) sumidor.gameObject.SetActive(true);
        else sumidor.gameObject.SetActive(false);
    }
}
