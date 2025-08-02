using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Iluminavel))]
public class ReatorGenerico : MonoBehaviour, Reacao {
    public UnityEvent OnAzul;
    public UnityEvent OnVermelho;
    public UnityEvent OnVerde;
    public UnityEvent OnNada;

    public void Reagir(Cores cor) {
        if (cor == Cores.BLUE) {
            OnAzul?.Invoke();
        } else if (cor == Cores.RED) {
            OnVermelho?.Invoke();
        } else if (cor == Cores.GREEN) {
            OnVerde?.Invoke();
        } else {
            OnNada?.Invoke();
        }
    }
}
