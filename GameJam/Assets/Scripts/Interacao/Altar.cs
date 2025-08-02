using UnityEngine;
using UnityEngine.Events;

public class Altar : MonoBehaviour, Interacao {
    public Transform holder;
    public bool estaSegurando => holder.childCount > 0;


    public UnityEvent OnAzul;
    public UnityEvent OnVermelho;
    public UnityEvent OnVerde;
    public UnityEvent OnNada;

    void Start() {
        if (holder.childCount == 0) {
            AtualizarBaseadoEmCor(Cores.VAZIO);
        } else {
            Luz luz = holder.GetChild(0).GetComponent<Luz>();
            if (luz != null) {
                AtualizarBaseadoEmCor(luz.GetCor());
            } else {
                AtualizarBaseadoEmCor(Cores.VAZIO);
            }
        }
    }

    public void Interagir() {
        if (estaSegurando && !Player.Instance.estaSegurando) {
            Player.Instance.SegurarItem(holder.GetChild(0));
            AtualizarBaseadoEmCor(Cores.VAZIO);
        } else if (!estaSegurando && Player.Instance.estaSegurando) {
            RecebeItem(Player.Instance.itemSegurado);
        }
    }

    public void RecebeItem(Transform item) {
        if (item == null) return;

        item.SetParent(holder);
        item.localPosition = Vector3.zero;
        item.localRotation = Quaternion.identity;

        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb != null) {
            rb.isKinematic = true;
        }

        Luz luz = item.GetComponent<Luz>();
        if (luz != null) {
            Cores cor = luz.GetCor();
            AtualizarBaseadoEmCor(cor);
        }
    }

    public void AtualizarBaseadoEmCor(Cores cor) {
        if (cor == Cores.BLUE) {
            OnAzul?.Invoke();
        } else if (cor == Cores.RED) {
            OnVermelho?.Invoke();
        } else if (cor == Cores.GREEN) {
            OnVerde?.Invoke();
        } else if (cor == Cores.VAZIO) {
            OnNada?.Invoke();
        }
    }

    public bool PodeInteragir() {
        return (estaSegurando && !Player.Instance.estaSegurando) || (!estaSegurando && Player.Instance.estaSegurando);
    }
}
