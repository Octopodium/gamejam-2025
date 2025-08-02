using UnityEngine;

public class SeguradorDeQueda : MonoBehaviour {
    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") || other.CompareTag("LampadaPrincipal")) {
            GameManager.Instance.Morte();
        }
    }
}
