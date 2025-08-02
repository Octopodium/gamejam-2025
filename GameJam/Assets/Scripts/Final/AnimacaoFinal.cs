using UnityEngine;

public class AnimacaoFinal : MonoBehaviour
{
    public GameObject luz;
    public Animator anim;

    public Sala sala;

    public void StartAnimation()
    {
        anim.SetTrigger("Final");

    }
}
