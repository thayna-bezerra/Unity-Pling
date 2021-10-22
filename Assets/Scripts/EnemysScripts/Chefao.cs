using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chefao : MonoBehaviour
{
    //Controle de Movimentação
    public float speed = 3;
    public bool dir = false;
    public float limitScreen = 1.5f;

    //Controle dos spawms
    public GameObject tiro;
    public Transform spawnPosition;
    public float delaySpawn = 1, minDelay = 1, maxDelay = 5;

    //controle de morte
    public Rigidbody2D rb2d;
    ChefaoStatus chefaoStatus;
    BoxCollider2D bc2D;
    private float timeDead = -0.4f;


    private void Start()
    {
        bc2D = GetComponent<BoxCollider2D>();

        //chefao sem interação com a gravidade
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.bodyType = RigidbodyType2D.Kinematic;

        chefaoStatus = GetComponent<ChefaoStatus>();

    }

    void Update()
    {
        if(chefaoStatus.currentLife > 0)
        {
            HorinzontalMove();
            SpawnTiros();
        }
        else
        {
            //morte
            bc2D.enabled = false;
            rb2d.bodyType = RigidbodyType2D.Dynamic;
            rb2d.gravityScale = timeDead;
            timeDead += Time.deltaTime;

            if (transform.position.y < -7) Destroy(this.gameObject);
        }
    }

    void HorinzontalMove()
    {
        if(!dir)
        {
             transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
        }
        else
        {
             transform.position -= new Vector3(speed, 0, 0) * Time.deltaTime;
        }

        //mudar dir de acordo com o limite de tela
        if (transform.position.x < -limitScreen) dir = false;
        else if (transform.position.x > 1.5f) dir = true;
    }

    void SpawnTiros()
    {
        if (delaySpawn > 0)
        {
            delaySpawn -= Time.deltaTime;
        }

        else if (delaySpawn <= 0)
        {
            delaySpawn = Random.Range(minDelay, maxDelay);
            Instantiate(tiro, spawnPosition.position, Quaternion.identity);
        }
    }
}
