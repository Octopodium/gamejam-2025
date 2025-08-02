using UnityEngine;
using UnityEngine.Events;

public class LuzBehaviour : MonoBehaviour{
    public Light outterLight;
    public MeshRenderer outterCircle, innerCircle;

    private float innerAlpha = 0.8f, outterAlpha = 0.0705f;

    public UnityAction<Color> OnMudaCor;

    public void Start(){
        OnMudaCor += MudaCor;
    }

    public void MudaCor(Color color){
        outterLight.color = color;

        color.a = innerAlpha;
        innerCircle.material.color = color;
        color.a = outterAlpha;
        outterCircle.material.color = color;
    }
}
