using UnityEngine;
using System.Collections.Generic;

public interface IResetavel {
    void Resetar();
}

public class Sala : MonoBehaviour {
    public List<Transform> resetaveis = new List<Transform>();
    List<IResetavel> _resetaveis = new List<IResetavel>();
    public Transform spawnPoint;
    public string proximaSala;

    void Start() {
        foreach (var resetavel in resetaveis) {
            if (resetavel.TryGetComponent<IResetavel>(out var resetavelComponent)) {
                _resetaveis.Add(resetavelComponent);
            }
        }

        GameManager.Instance.SetSalaAtual(this);
    }

    public void ResetarSala() {
        foreach (var resetavel in _resetaveis) {
            resetavel.Resetar();
        }

        Player.Instance.Resetar();
    }

    public void RequestPassarDeSala() {
        GameManager.Instance.PassarDeSala();
    }

}
