using UnityEngine;

public class SeguradorDeQueda : MonoBehaviour {

    public Transform overrideRespawn;
    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") || other.CompareTag("LampadaPrincipal")) {
            GameManager.Instance.Morte();

            if (overrideRespawn != null) GameManager.Instance.TPPlayerTo(overrideRespawn);
        }
    }
}
