using UnityEngine;
using UnityEngine.Events;

public class LuzBehaviour : MonoBehaviour{
    public bool luzPrincipal = true;
    public Cores corNaoPrincipal = Cores.VAZIO;

    public Light outterLight;
    public MeshRenderer outterCircle;

    private float outterAlpha = 0.0705f;

    public UnityAction<Color> OnMudaCor;
    public InputRef inputRef;

    public void Start(){
        if (luzPrincipal) {
            inputRef = GameManager.Instance.inputRef;
            inputRef.SwitchEvent += MudaCor;
            CorInicial();
        } else {
            SetCor(corNaoPrincipal);
        }
    }

    public void OnDestroy(){
        if (luzPrincipal) {
            inputRef.SwitchEvent -= MudaCor;
        }
    }

    public void MudaCor(){
        if(Player.Instance.itemSegurado != transform){
            return;
        }

        if(GameManager.Instance.cores == Cores.GREEN){
            GameManager.Instance.cores = 0;
        }else{
            GameManager.Instance.cores++;
        }

        Color color = GameManager.Instance.GetColor();
        outterLight.color = color;

        //color.a = innerAlpha;
        //innerCircle.material.color = color;
        color.a = outterAlpha;
        outterCircle.material.color = color;
    }

    public void SetCor(Cores cor) {
        Color color = GameManager.Instance.GetColor(cor);
        outterLight.color = color;
        color.a = outterAlpha;
        outterCircle.material.color = color;
    }

    public void CorInicial(){
        Color color = GameManager.Instance.GetColor();
        outterLight.color = color;
        color.a = outterAlpha;
        outterCircle.material.color = color;
    }
}
