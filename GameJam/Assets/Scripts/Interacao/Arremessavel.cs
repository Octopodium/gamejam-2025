using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Arremessavel : MonoBehaviour, Interacao {

    public Rigidbody rb { get; private set; }

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    public void Interagir() {
        if (!Player.Instance.estaSegurando) Player.Instance.SegurarItem(transform);
    }

    public bool PodeInteragir() {
        return !Player.Instance.estaSegurando && !rb.isKinematic;
    }   

    public void OnHold() {
        if (rb == null) rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    public void OnRelease() {
        if (rb == null) rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
    }
}
