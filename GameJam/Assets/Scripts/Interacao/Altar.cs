using UnityEngine;

public class Altar : MonoBehaviour, Interacao {
    public Transform holder;
    public bool estaSegurando => holder.childCount > 0;

    public void Interagir() {
        if (estaSegurando && !Player.Instance.estaSegurando) {
            Player.Instance.SegurarItem(holder.GetChild(0));
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
    }

    public bool PodeInteragir() {
        return (estaSegurando && !Player.Instance.estaSegurando) || (!estaSegurando && Player.Instance.estaSegurando);
    }
}
