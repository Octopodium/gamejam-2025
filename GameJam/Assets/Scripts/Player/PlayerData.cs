using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Player data é uma classe de variaveis e metodos auxiliares do player, que vai armazenar alguns valores, checagens e funcoes prontas que seram utilizadas no player 

[CreateAssetMenu(menuName = "Player Data")] 
public class PlayerData : ScriptableObject
{   
    //variaveis de calculo auxiliar 
    [Header("Gravity")]
    public float gravityStrength;
    public float gravityScale;
    public float fallGravityMult;

    [Space(20)]

    //variaveis de calculo auxilar do jump
    [Header("Jump")]
    public float jumpHeigth;
    public float jumpTime;
    public float jumpForce;
    
    //checagem e calculo padrao para definir a gravidade 
    private void OnChecks()
    {
        gravityStrength = -(2 * jumpHeigth) / (jumpTime * jumpTime);
        gravityScale = gravityStrength / Physics.gravity.y;
        jumpForce = Mathf.Abs(gravityStrength) * jumpTime;
    }

}
