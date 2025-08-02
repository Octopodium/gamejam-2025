using UnityEngine;

public class PlataformaMovelVariada : MonoBehaviour {

    public Vector3[] posicoes;


    bool movendo = false;
    Vector3 destino;
    public float speed = 2.0f; 


    void Awake(){
        transform.position = posicoes[0];
        destino = posicoes[0];
    }

    void FixedUpdate(){
        if (!movendo) return;


        transform.position = Vector3.MoveTowards(transform.position, destino, speed * Time.fixedDeltaTime);

        if (Vector3.Distance(transform.position, destino) < 0.01f) {
            movendo = false;
        }
    }

    public void Ativar() {
        movendo = true;
    }

    public void Desativar() {
        movendo = false;
    }

    public void IrPara(int posicao) {
        if (posicao < 0 || posicao >= posicoes.Length) {
            Debug.LogError("Posição inválida: " + posicao);
            return;
        }
        destino = posicoes[posicao];
        movendo = true;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        foreach (var pos in posicoes) {
            Gizmos.DrawSphere(pos, 0.1f);
        }
    }
}
