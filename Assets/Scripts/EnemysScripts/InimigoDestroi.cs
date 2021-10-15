using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoDestroi : MonoBehaviour
{
    public PlayerMove p;

    private void Start()
    {
        p = GetComponent<PlayerMove>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }

   // destruído quando sai da tela
    void Update()
    {
        if(transform.position.y < -6) //posição fora da tela
        {
            Destroy(this.gameObject);
        }
    }
}