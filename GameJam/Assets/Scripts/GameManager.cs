using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour {
    public static GameManager Instance { get; private set; }
    public InputRef inputRef;

    public GameObject player;
    public GameObject panelPause;

    public GameObject luzPrefab;
    

    public Color red, blue, green;
    public Cores cores;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }

        inputRef.PauseEvent += TogglePause;
    }

    void OnDestroy() {
        inputRef.PauseEvent -= TogglePause;
    }

    #region Pause

    public Action<bool> OnPauseChange;

    public void Pausar()
    {
        Time.timeScale = 0f;
        OnPauseChange?.Invoke(true);
        panelPause.SetActive(true);
    }

    public void Despausar()
    {
        Time.timeScale = 1f;
        OnPauseChange?.Invoke(false);
        panelPause.SetActive(false);
    }

    public void SetPause(bool pause) {
        if (pause) Pausar();
        else Despausar();
    }

    public void TogglePause() {
        if (Time.timeScale == 0f) Despausar();
        else Pausar();
    }

    #endregion


    #region Sala

    AsyncOperation cenaProx;
    Sala salaAtual;

    public void SetSalaAtual(Sala sala) {
        salaAtual = sala;
        PreloadSala(sala.proximaSala);

        player.transform.position = sala.spawnPoint.position;
        player.transform.rotation = sala.spawnPoint.rotation;
        player.SetActive(true);

        sala.ResetarSala();
    }

    public void TPPlayerTo(Transform target) {
        if (target != null) {
            player.transform.position = target.position;
            player.transform.rotation = target.rotation;
        } else {
            Debug.LogWarning("Target transform is null, cannot teleport player.");
        }
    }

    public void PreloadSala(string cenaName) {
        Debug.Log($"Preloading scene: {cenaName}");
        cenaProx = SceneManager.LoadSceneAsync(cenaName);
        cenaProx.allowSceneActivation = false;
    }

    public void PassarDeSala() {
        Player.Instance.transform.SetParent(transform);

        Scene cenaAtual = salaAtual.gameObject.scene;
        player.SetActive(false);

        cenaProx.allowSceneActivation = true;
        cenaProx = null;

        SceneManager.UnloadSceneAsync(cenaAtual);
    }

    public void ResetarSala() {
        if (salaAtual != null) {
            salaAtual.ResetarSala();
        }
    }

    public Sala GetSalaAtual() {
        return salaAtual;
    }

    #endregion

    public void Morte() {
        Debug.Log("Player morreu, reiniciando sala atual");
        ResetarSala();
    }

    public void DestroyThyself() {
            Destroy(gameObject);
            Instance = null; 
    }

    #region Color

    public Color GetColor(Cores cores){
        if(cores == Cores.RED) return red;
        if(cores == Cores.BLUE) return blue;
        if(cores == Cores.GREEN) return green;
        else{
            return red;
        }
    }

    public Color GetColor(){
        if(cores == Cores.RED) return red;
        if(cores == Cores.BLUE) return blue;
        if(cores == Cores.GREEN) return green;
        else{
            return red;
        }
    }

    public Cores GetCor() {
        return cores;
    }

    #endregion

}