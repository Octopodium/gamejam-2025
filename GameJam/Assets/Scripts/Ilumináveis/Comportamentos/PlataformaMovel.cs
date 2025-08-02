using UnityEngine;

[RequireComponent(typeof(Iluminavel))]
public class PlataformaMovel : MonoBehaviour, Reacao {
    public Vector3 posicaoInicial;
    public Vector3 posicaoFinal;


    bool movendo = false;
    Vector3 destino;
    public float speed = 2.0f; 
    public Cores corMexer;


    void Awake(){
        transform.position = posicaoInicial;
        destino = posicaoFinal;
    }

    void FixedUpdate(){
        if (!movendo) return;


        transform.position = Vector3.MoveTowards(transform.position, destino, speed * Time.fixedDeltaTime);

        if (Vector3.Distance(transform.position, destino) < 0.01f) {
            destino = (destino == posicaoInicial) ? posicaoFinal : posicaoInicial;
        }
    }

    public void Reagir(Cores cor) {
        movendo = cor == corMexer;
    }

    public void MoveToTarget(){
        destino = posicaoInicial;
    }

    public void ReturnToStart(){
        destino = posicaoFinal;
    }
}
