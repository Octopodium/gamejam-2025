using UnityEngine;

public class LightPost : MonoBehaviour{

    [SerializeField] private Light[] lights = new Light[2];


    public void TurnOnTheLights(){
        foreach(Light light in lights){
            light.gameObject.SetActive(true);
        }
    }

}
