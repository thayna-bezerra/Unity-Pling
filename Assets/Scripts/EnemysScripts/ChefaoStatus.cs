using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChefaoStatus : MonoBehaviour
{

    public int maxLife = 100;
    public int currentLife;

    public Slider barraDeVida;
    public GameObject explosao;

    void Start()
    {
        currentLife = maxLife;
        barraDeVida.maxValue = maxLife;
    }

    void Update()
    {
        barraDeVida.value = currentLife;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tiro"))
        {
            Destroy(collision.gameObject);
            currentLife -= 10;

            Instantiate(explosao, collision.transform.position, Quaternion.identity);
        }
    }
}
