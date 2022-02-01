using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chefao : MonoBehaviour
{
    [Header("Controle de Movimentação")]
    public float speed = 3;
    public bool dir = false;
    public float limitScreen = 1.5f;

    [Space(10)]

    [Header("Controle de Spawns")]
    public GameObject tiro; //enemy sendo spawnado
    public Transform spawnPosition;
    public float delaySpawn = 1, minDelay = 1, maxDelay = 5;

    [Space(10)]

    [Header("Controle de Morte")]
    //controle de morte
    public Rigidbody2D rb2d;
    ChefaoStatus chefaoStatus;
    BoxCollider2D bc2D;
    private float timeDead = -0.5f;

    public Animator animBoss;

    [Space(10)]

    public bool isDamage = false;
    public bool isActive = false;
    public float cont = 0.2f;

    private void Start()
    {
        bc2D = GetComponent<BoxCollider2D>();
        chefaoStatus = GetComponent<ChefaoStatus>();
        rb2d = GetComponent<Rigidbody2D>();

        animBoss = GetComponent<Animator>();
        isActive = true;
        //chefao sem interação com a gravidade
    }

     void Update()
     {
         if(chefaoStatus.currentLife > 0)
         {
             if(transform.position.y == 7) { rb2d.bodyType = RigidbodyType2D.Dynamic; }

             else if (transform.position.y <= 2.26)
             {
                 rb2d.bodyType = RigidbodyType2D.Static;
                 HorinzontalMove();
                 SpawnTiros();
                 AnimStateBoss();
             }
         }

         else
         {
             animBoss.Play("deadBoss");
             //morte
             bc2D.enabled = false;

             rb2d.bodyType = RigidbodyType2D.Dynamic;
             rb2d.gravityScale = timeDead;
             timeDead += Time.deltaTime;

             if (transform.position.y < -7) Destroy(this.gameObject);
         }
     }

    void AnimStateBoss()
    {
        if (isActive == true) { animBoss.Play("Boss"); }

        else if (isDamage == true)
        {
            cont -= Time.deltaTime;
            animBoss.Play("damageBoss");

            if (cont < 0) { isDamage = false; isActive = true; }
        }
    }

    void HorinzontalMove()
    {
        if(!dir) { transform.position += new Vector3(speed, 0, 0) * Time.deltaTime; }

        else { transform.position -= new Vector3(speed, 0, 0) * Time.deltaTime; }

        //mudar dir de acordo com o limite de tela
        if (transform.position.x < -limitScreen) dir = false;
        else if (transform.position.x > 1.5f) dir = true;
    }

    void SpawnTiros()
    {
        if (delaySpawn > 0) { delaySpawn -= Time.deltaTime; }

        else if (delaySpawn <= 0)
        {
            delaySpawn = Random.Range(minDelay, maxDelay);
            Instantiate(tiro, spawnPosition.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tiro"))
        {
            isActive = false;
            isDamage = true;
            cont = 0.2f;
        }
    }
}
