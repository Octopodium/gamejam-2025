using UnityEngine;

public class ChaoGrudavel : MonoBehaviour {
    public void Grudar(Transform item) {
        if (item == null) return;

        item.SetParent(transform, true);
    }

    public void Desgrudar(Transform item) {
        if (item == null) return;

        item.SetParent(null, true);
    }
}
