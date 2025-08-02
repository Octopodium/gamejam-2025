using UnityEngine;
using UnityEngine.Events;


public class Trigger : MonoBehaviour {
    public string tagFilter = "";

    public UnityEvent<Collider> OnTriggerEnterEvent;
    public UnityEvent<Collider> OnTriggerExitEvent;


    void OnTriggerEnter(Collider other) {
        if (string.IsNullOrEmpty(tagFilter) || other.CompareTag(tagFilter)) {
            OnTriggerEnterEvent?.Invoke(other);
        }
    }

    void OnTriggerExit(Collider other) {
        if (string.IsNullOrEmpty(tagFilter) || other.CompareTag(tagFilter)) {
            OnTriggerExitEvent?.Invoke(other);
        }
    }

}
