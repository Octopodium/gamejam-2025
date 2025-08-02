using UnityEngine;

public class Altar : MonoBehaviour, Interacao {
    public Transform holder;
    public bool estaSegurando => holder.childCount > 0;

    public GameObject segurandoAzul, segurandoVermelho, segurandoVerde;
    public GameObject segurandoNada;

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
        if (segurandoAzul != null) segurandoAzul.SetActive(true);
        if (segurandoVermelho != null) segurandoVermelho.SetActive(false);
        if (segurandoVerde != null) segurandoVerde.SetActive(false);
        if (segurandoNada != null) segurandoNada.SetActive(false);

        if (cor == Cores.BLUE && segurandoAzul != null) {
            segurandoAzul.SetActive(true);
        } else if (cor == Cores.RED && segurandoVermelho != null) {
            segurandoVermelho.SetActive(true);
        } else if (cor == Cores.GREEN && segurandoVerde != null) {
            segurandoVerde.SetActive(true);
        } else if (cor == Cores.VAZIO && segurandoNada != null) {
            segurandoNada.SetActive(true);
        }
    }

    public bool PodeInteragir() {
        return (estaSegurando && !Player.Instance.estaSegurando) || (!estaSegurando && Player.Instance.estaSegurando);
    }
}
