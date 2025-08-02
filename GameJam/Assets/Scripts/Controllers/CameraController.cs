using UnityEngine;
using Unity.Cinemachine;

public class CameraController : MonoBehaviour {
    public CinemachineCamera cinemachineCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        if (Player.Instance != null) {
            cinemachineCamera.Follow = Player.Instance.transform;
            cinemachineCamera.LookAt = Player.Instance.transform;
        } else {
            Debug.LogWarning("Player instance not found. Camera will not follow the player.");
        }
    }

}
