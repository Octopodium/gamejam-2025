using UnityEngine;

public class Iluminavel : MonoBehaviour {
    Reacao reacao;

    void Awake() {
        reacao = GetComponent<Reacao>();
    }

    public void Reagir(Cores cor){
        if(reacao != null) reacao.Reagir(cor);
    }

}
