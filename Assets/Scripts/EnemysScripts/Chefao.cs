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

    void Update()
    {
        //mudar dir de acordo com o limite de tela
        if (transform.position.x < -limitScreen) dir = false;
        else if (transform.position.x > 1.5f) dir = true;

        HorinzontalMove();

        if(delaySpawn > 0)
        {
            delaySpawn -= Time.deltaTime;
        }

        else if (delaySpawn <= 0)
        {
            delaySpawn = Random.Range(minDelay, maxDelay);
            Instantiate(tiro, spawnPosition.position, Quaternion.identity);
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

    }
}
