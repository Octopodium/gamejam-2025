using UnityEngine;

public class Iluminavel : MonoBehaviour
{
    Reacao reacao;
    public void Reagir(Color cor){
        if(reacao != null) reacao.Reagir();
    }

}
