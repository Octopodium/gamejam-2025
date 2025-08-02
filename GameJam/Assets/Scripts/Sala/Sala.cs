using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IResetavel {
    void Resetar();
}

public class Sala : MonoBehaviour {
    public List<Transform> resetaveis = new List<Transform>();
    List<IResetavel> _resetaveis = new List<IResetavel>();
    public Transform spawnPoint;
    public string proximaSala;

    public bool luzNoPlayerNoInicio = true;
    public Transform luzHolderInicio;

    void Start() {
        foreach (var resetavel in resetaveis) {
            if (resetavel.TryGetComponent<IResetavel>(out var resetavelComponent)) {
                _resetaveis.Add(resetavelComponent);
            }
        }

        GameManager.Instance.SetSalaAtual(this);
    }

    public void AddResetavel(IResetavel resetavel) {
        if (!_resetaveis.Contains(resetavel)) {
            _resetaveis.Add(resetavel);
        }
    }

    public void RemoveResetavel(IResetavel resetavel) {
        if (reseting) {
            StartCoroutine(RemoveResetavelAsync(resetavel));
            return;
        }

        if (_resetaveis.Contains(resetavel)) {
            _resetaveis.Remove(resetavel);
        }
    }

    public IEnumerator RemoveResetavelAsync(IResetavel resetavel) {
        yield return new WaitUntil(() => !reseting);
        if (_resetaveis.Contains(resetavel)) {
            _resetaveis.Remove(resetavel);
        }
    }

    bool reseting = false;

    public void ResetarSala() {
        reseting = true;
        foreach (var resetavel in _resetaveis) {
            resetavel.Resetar();
        }

        Player.Instance.Resetar();
        Player.Instance.transform.position = spawnPoint.position;
        Player.Instance.transform.rotation = spawnPoint.rotation;

        LuzPrincipalTag luzQueExiste = FindFirstObjectByType<LuzPrincipalTag>();
        if (luzQueExiste != null) {
            Destroy(luzQueExiste.gameObject);
        }

        GameObject luz = Instantiate(GameManager.Instance.luzPrefab, luzHolderInicio);
        Arremessavel arremessavel = luz.GetComponent<Arremessavel>();
        arremessavel.OnHold();

        if (luzNoPlayerNoInicio) {
            Player.Instance.SegurarItem(luz.transform);
        }

        reseting = false;

    }

    public void RequestPassarDeSala() {
        GameManager.Instance.PassarDeSala();
    }

    public void SetAsSpawnPoint(Transform newSpawnPoint) {
        spawnPoint = newSpawnPoint;
    }

}
