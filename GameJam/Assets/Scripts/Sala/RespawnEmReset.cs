using UnityEngine;
public class RespawnEmReset : MonoBehaviour, IResetavel {
    public Transform respawnPoint;
    Sala salaAtual;

    void Start() {
        salaAtual = FindFirstObjectByType<Sala>();
        salaAtual.AddResetavel(this);
    }

    public void Resetar() {
        if (gameObject == null) return;
        GameObject reInstananciado = Instantiate(gameObject, respawnPoint.position, respawnPoint.rotation);
        reInstananciado.transform.SetParent(respawnPoint, true);
        reInstananciado.transform.localScale = Vector3.one;
        salaAtual?.RemoveResetavel(this);
        Destroy(gameObject);
    }
}
