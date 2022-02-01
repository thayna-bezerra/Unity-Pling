using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownController : MonoBehaviour
{
    [Space(10)]

    [Header("Contador")]
    public int countdownTime;
    public Text countdownText;

    [Space(10)]

    [Header("Objetos que serão Pausados")]
    public GameObject GameController; //Objeto com o Script GameController
    public GameObject Player; //Objeto com o Script PlayerMove
    public GameObject spriteBoss; //Para ocultar a sprite do Chefão antes do contador na fase final

    void Start() { StartCoroutine(CountdownToStart()); } //Chamando o método do IEnumerator "CountdownToStart()" assim q inicia

    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0) //contador definido 3 -> enquanto 3 menor que 0
        {
            //desativar estes scripts ENQUANTO  3 menor que 0
            GameController.GetComponent<GameController>().enabled = false;
            Player.GetComponent<PlayerMove>().enabled = false;

            //desativar sprite do Boss ENQUANTO 3 menor que 0
            spriteBoss.SetActive(false);

            //Passar o valor da variavel Countdown para o Text
            countdownText.text = countdownTime.ToString();

            //como "tornar o tempo real", esperar 1 segundo
            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        //após sair do laço de repetição, o text apresentará "GO"
        countdownText.text = "GO!";

        //após sair do laço de repetição, REATIVAR os scripts e o sprite do Boss
        GameController.GetComponent<GameController>().enabled = true;
        Player.GetComponent<PlayerMove>().enabled = true;
        spriteBoss.SetActive(true);

        //esperar 1 segundo até desativar o Text do countdown na tela
        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);
    }
}
