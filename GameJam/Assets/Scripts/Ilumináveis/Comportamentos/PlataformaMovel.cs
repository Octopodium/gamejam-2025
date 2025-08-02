using UnityEngine;

[RequireComponent(typeof(Iluminavel))]
public class PlataformaMovel : MonoBehaviour, Reacao, IResetavel {
    public bool loop = true;
    public bool reativo = true;

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
            if (!loop) {
                movendo = false;
                return;
            }
            destino = (destino == posicaoInicial) ? posicaoFinal : posicaoInicial;
        }
    }

    public void Resetar() {
        transform.position = posicaoInicial;
        destino = posicaoFinal;
        movendo = false;
    }

    public void Ativar() {
        movendo = true;
    }

    public void Desativar() {
        movendo = false;
    }

    public void Reagir(Cores cor) {
        if (!reativo) return;
        movendo = cor == corMexer;
    }

    public void MoveToTarget(){
        destino = posicaoInicial;
    }

    public void ReturnToStart(){
        destino = posicaoFinal;
    }
}
