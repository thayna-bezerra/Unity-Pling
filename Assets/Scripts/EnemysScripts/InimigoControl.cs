using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoControl : MonoBehaviour
{
    public GameController GameController;
    public GameObject explosao, particula;
    
    public Rigidbody2D rb2d; //Variavel de referencia do Rb2d
    
    void Start()
    {    //Acessando o componente Rb2D e realizando alteração no valor da Gravidade
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = Random.Range(0.2f,0.4f); //GRAVIDADE   //(0.4f, 1)

        GameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        OutLayout(); //método para destruir o objeto quando este sair do layout
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //COLISÃO COM O PLAYER
        if(collision.gameObject.tag == "Player" && GameController.VidaPlayer > 0)
        {
            if(gameObject.tag == "ColetavelA")
            {
               GameController.coletavelA++;
               //GameController.Pontos++; //+10 pontos para cada coletavel
               Instantiate (particula, transform.position, Quaternion.identity);
            }

            else if(gameObject.tag == "ColetavelB")
            {
               GameController.coletavelB++;
               //GameController.Pontos++; //+10 pontos para cada coletavel
               Instantiate (particula, transform.position, Quaternion.identity);
            }
            
            else if(gameObject.tag == "Moeda")
            {
               GameController.MoedaColet++;
               Instantiate (particula, transform.position, Quaternion.identity);
            }

            else if(gameObject.tag == "Inimigo")
            {
               GameController.VidaPlayer--;
                Instantiate(particula, transform.position, Quaternion.identity);
            }

           Destroy(this.gameObject);
        } 

        //COLISÃO COM O TIRO (ATAQUE PLAYER)
        else if (collision.gameObject.tag == "Tiro") 
        {
            Instantiate(explosao, transform.position, Quaternion.identity); //spawnando fx de explosão para quando o tiro colidir em algo

            Destroy(this.gameObject); //DESTRUIR o objeto tiro (destruir a bala)
            Destroy(collision.gameObject); //DESTRUIR o objeto que colidiu com o tiro

            GameController.MoedaColet =+5; //+5 moedas por matar inimigo

           //GameController.Pontos++; //+5 pontos por matar inimigo
        } 
    }

    public void OutLayout() //destroi o objeto fora da tela
    {
        if(transform.position.y < -7)
        {
           Destroy(this.gameObject);
        }
    }
}