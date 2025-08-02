using UnityEngine;

public class Iluminavel : MonoBehaviour
{
    Reacao reacao;
    public void Reagir(){
        if(reacao != null) reacao.Reagir();
    }

}
