using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownController : MonoBehaviour
{
    public int countdownTime;
    public Text countdownText;

    public GameObject GameController; //objeto que ta o script gc
    public GameObject Player; //script player move
    public GameObject spriteBoss;

    void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            GameController.GetComponent< GameController > ().enabled = false;
            Player.GetComponent<PlayerMove> ().enabled = false;
            spriteBoss.SetActive(false);

            countdownText.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        countdownText.text = "GO!";

        GameController.GetComponent<GameController>().enabled = true;
        Player.GetComponent<PlayerMove>().enabled = true;
        spriteBoss.SetActive(true);

        yield return new WaitForSeconds(1f);

        countdownText.gameObject.SetActive(false);
    }

}
