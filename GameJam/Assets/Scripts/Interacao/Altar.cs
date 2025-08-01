using UnityEngine;

public class Altar : MonoBehaviour, Interacao {
    public Transform holder;

    public void Interagir() {
        // Implementar lógica de interação com o altar
        Debug.Log("Interagindo com o Altar");
    }

    public bool PodeInteragir() {
        return true; // Exemplo: sempre pode interagir
    }
}
