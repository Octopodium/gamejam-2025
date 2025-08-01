using UnityEngine;

public class Interagivel : MonoBehaviour {
    Interacao interacao;

    void Awake()  {
        interacao = GetComponent<Interacao>();
    }

    public void Interagir() {
        if (interacao != null) interacao.Interagir();
    }

    public bool PodeInteragir() {
        if (interacao != null) return interacao.PodeInteragir();
        return false;
    }
}
