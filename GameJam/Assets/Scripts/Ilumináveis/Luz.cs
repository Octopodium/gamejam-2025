using UnityEngine;
using System.Collections.Generic;

public class Luz : MonoBehaviour{
    public float raioIluminacao = 4.1f;
    public LayerMask ilumiavelLayer;
    Collider[] collidersIluminaveis;
    Iluminavel iluminavel;

    public void Start(){
        collidersIluminaveis = new Collider[8];
    }
    
    public void FixedUpdate(){
        ChecarIluminaveis();
    }

    public Cores GetCor(){
        return GameManager.Instance.GetCor();
    }

    List<Iluminavel> iluminaveisNoRange = new List<Iluminavel>();

    public void ChecarIluminaveis(){
        List<Iluminavel> iluminaveisAtuais = new List<Iluminavel>();

        int iluminaveisQuant = Physics.OverlapSphereNonAlloc(transform.position, raioIluminacao, collidersIluminaveis, ilumiavelLayer);
        for(int i = 0; i < iluminaveisQuant; i++){
            iluminavel = collidersIluminaveis[i].GetComponent<Iluminavel>();
            iluminavel.Reagir(GameManager.Instance.GetCor());

            if (iluminaveisNoRange.Contains(iluminavel)){
                iluminaveisNoRange.Remove(iluminavel);
            }

            iluminaveisAtuais.Add(iluminavel);
        }

        for (int i = iluminaveisNoRange.Count - 1; i >= 0; i--) {
            iluminavel = iluminaveisNoRange[i];
            iluminavel.Reagir(Cores.VAZIO);
            iluminaveisNoRange.RemoveAt(i);
        }

        iluminaveisNoRange = iluminaveisAtuais;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, raioIluminacao);
    }

}
