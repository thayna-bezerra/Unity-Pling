using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerMove player; //instanciando o code de PlayerMove e chamando o objeto player
    public GameObject smoke; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //O objeto CHÃO recebe a tag GROUND
        if(collision.gameObject.tag == "ground")
        {
            player.isJumping = false; //quando o groundcheck do player colide com o chão
            Instantiate (smoke, transform.position, transform.rotation); //spawn da fumaça quando o player volta para o chão
        }
    }
}
