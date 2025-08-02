using UnityEngine;

public class Luz : MonoBehaviour{
    float raioIluminacao = 3.9f;
    public LayerMask ilumiavelLayer;
    Collider[] collidersIluminaveis;
    Iluminavel iluminavel;

    public void Start(){
        collidersIluminaveis = new Collider[8];
    }
    
    public void FixedUpdate(){
        ChecarIluminaveis();
    }

    public void ChecarIluminaveis(){
        int iluminaveisQuant = Physics.OverlapSphereNonAlloc(transform.position, raioIluminacao, collidersIluminaveis, ilumiavelLayer);
        for(int i = 0; i < iluminaveisQuant; i++){
            iluminavel = collidersIluminaveis[i].GetComponent<Iluminavel>();
            iluminavel.Reagir(GameManager.Instance.GetCor());
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, raioIluminacao);
    }

}
