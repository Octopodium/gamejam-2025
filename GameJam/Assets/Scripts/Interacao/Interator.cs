using UnityEngine;

public class Interator : MonoBehaviour {
    public float raioInteracao = 3f; // Raio de interação
    public LayerMask layerInteragivel; // Layer dos objetos interagíveis
    Collider[] collidersInteragiveis;
    Interagivel ultimoInteragivel;



    void Start() {
        collidersInteragiveis = new Collider[8]; // Tamanho do array pode ser ajustado conforme necessário
        ultimoInteragivel = null;
    }

    void FixedUpdate() {
        ChecarInteragiveis();
    }

    public bool Interagir() {
        if (ultimoInteragivel != null) {
            ultimoInteragivel.Interagir();
            return true;
        }
        return false;
    }

    public bool ChecarInteragiveis() {
        // Checa por objetos interagíveis no raio de interação
        int quant = Physics.OverlapSphereNonAlloc(transform.position, raioInteracao, collidersInteragiveis, layerInteragivel);

        // Procura o interagível mais próximo (não podemos confiar na ordem padrão dos colliders)
        float menorDistancia = Mathf.Infinity;
        Interagivel interagivelMaisProximo = null;
        for (int i = 0; i < quant; i++) {
            Collider collider = collidersInteragiveis[i];
            if (collider == null) continue;


            Interagivel interagivelAtual = collider.GetComponent<Interagivel>();

            if (interagivelAtual == null || !interagivelAtual.PodeInteragir()) continue;

            float distancia = Vector3.Distance(transform.position, collider.transform.position);
            if (distancia < menorDistancia) {
                menorDistancia = distancia;
                interagivelMaisProximo = interagivelAtual;
            }
        }

        if (interagivelMaisProximo == null) {
            if (ultimoInteragivel != null) UIController.Instance.MostrarIndicador(null);
            ultimoInteragivel = null;
            return false;
        }

        // Trata do ultimo interagivel
        if (ultimoInteragivel != null) {
            if (ultimoInteragivel == interagivelMaisProximo) return true; // Se o interagível mais próximo for o mesmo que o último interagível, não faz nada
            UIController.Instance.MostrarIndicador(null);
        }

        UIController.Instance.MostrarIndicador(interagivelMaisProximo);
        ultimoInteragivel = interagivelMaisProximo;

        return true;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, raioInteracao);
    }
}
