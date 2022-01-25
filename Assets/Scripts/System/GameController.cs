using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //OBJETOS QUE SERÃO SPAWNADOS
    [Header("Objetos que serão spawnados")]
    public GameObject Inimigo;
    public GameObject ColetavelA;
    public GameObject ColetavelB;
    public GameObject MoedaColetavel;

    public GameObject panelWins, panelGameOver;
    
    [Header("--Atributos Player--")]
    public int VidaPlayer = 3;
    public int danoPlayer = 10;
    public GameObject[] VidasHUD = new GameObject[3];
     
    public int MoedaColet = 0;

    public int coletavelA = 0, coletavelAMAX; 
    public int coletavelB = 0, coletavelBMAX;

    [Header("--TEXTOS--")]
    public Text tMoedaColet;
    public Text tcoletavelA;
    public Text tcoletavelB;

    //Controle do tempo de spawn Inimigos/ColetavelA/ColetavelB
    [SerializeField] public float delaySpawnInimigo = 3, delaySpawnColetavelA = 2.1f, delaySpawnColetavelB = 2.5f, delaySpawnMoedaColetavel = 2.5f; 
  
    public PlayerMove player;

    ChefaoStatus chefaoStatus; // identificar quanto de vida tem o chefão

    private void Start()
    {
        //acessando 
        if(SceneManager.GetActiveScene().name == "FaseChefao")
        {
            chefaoStatus = GameObject.FindGameObjectWithTag("Chefao").GetComponent<ChefaoStatus>();
        }

        //carrega o valor do objetivo Max de acordo com a fase escolhida
        //coletavelAMAX = PlayerPrefs.GetInt("objetivoMaxMaca");
        //coletavelBMAX = PlayerPrefs.GetInt("objetivoMaxLaranja");


        //desabilita panel de vitoria
        panelWins.SetActive(false);

        //Escada do tempo = 1 (normal)
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (VidaPlayer > 0)
        {
            //os spawns so acontecem quando não for a tela do chefão
            if (SceneManager.GetActiveScene().name != "FaseChefao")
            {
                SpawnAll();
            }

            else if (SceneManager.GetActiveScene().name == "FaseChefao")
            {
                if(chefaoStatus.currentLife <= 0)
                {
                    SpawnAll();
                }
            }

            ObjetivoConcluido();
            panelGameOver.SetActive(false);
        }

        else
        {
            panelGameOver.SetActive(true);
        }

            HUDdisplay();
    }

    /*void ConditionWinFase()
    {
        if(coletavelA == coletavelAMAX && coletavelB == coletavelBMAX){
            coletavelA = 0;
            coletavelB = 0;
            //coletavelAMAX += 5;
            //coletavelBMAX += 5;
        }
    }*/

    #region Spawns
    void SpawnAll()
    {
        SpawnInimigo();

        //spawns caso já tenha ou não coletado os objetivos
        if (coletavelA < coletavelAMAX)
        {
            SpawnColetavelA();
        }
        if (coletavelB < coletavelBMAX)
        {
            SpawnColetavelB();
        }

        SpawnMoedaColetavel();
    }

    void SpawnInimigo ()
    {
        delaySpawnInimigo -= Time.deltaTime;

        if(delaySpawnInimigo <= 0 )
        {
            InstantiateObjects(Inimigo);
            delaySpawnInimigo = Random.Range(0.5f,5);
        }
    }

    void SpawnColetavelA ()
    {
        if (coletavelA < coletavelAMAX)
        {
           delaySpawnColetavelA -= Time.deltaTime;

          if(delaySpawnColetavelA <= 0)
          {
            InstantiateObjects(ColetavelA);
            delaySpawnColetavelA = Random.Range(0.5f,4);
          }
        }
    }
    
    void SpawnColetavelB ()
    {
        delaySpawnColetavelB -= Time.deltaTime;
        if(delaySpawnColetavelB <= 0)
        {
            InstantiateObjects(ColetavelB);
            delaySpawnColetavelB = Random.Range(0.5f,4);
        }
        
    }
    void SpawnMoedaColetavel ()
    {
        delaySpawnMoedaColetavel -= Time.deltaTime;
        if(delaySpawnMoedaColetavel <= 0)
        {
            InstantiateObjects(MoedaColetavel);
            delaySpawnMoedaColetavel = Random.Range(0.5f,3);
        }
        
    }

    void InstantiateObjects(GameObject objeto)
    {
        Instantiate (objeto, transform.position = new Vector3 (Random.Range(-2,2),6,0), transform.rotation);
    }

#endregion

    //exibir informações na tela
    void HUDdisplay()
    {
        tMoedaColet.text = MoedaColet.ToString();
        tcoletavelA.text = coletavelA.ToString() + "/" + coletavelAMAX.ToString();
        tcoletavelB.text = coletavelB.ToString() + "/" + coletavelBMAX.ToString();

        if (coletavelA >= coletavelAMAX)
        {
            tcoletavelA.text = "<color=#ff0000>" +  coletavelA.ToString() + "/" + coletavelAMAX.ToString() + "</color>";
        } 
        if (coletavelB >= coletavelBMAX)
        {
            tcoletavelB.text = "<color=#ff0000>" +  coletavelB.ToString() + "/" + coletavelBMAX.ToString() + "</color>";
        }

        //parte visual vida
        switch (VidaPlayer) 
        {
            case 3:
                VidasHUD[0].SetActive(false); //com uma vida desativado
                VidasHUD[1].SetActive(false); //com duas vidas desativado
                VidasHUD[2].SetActive(true);  //com 3 vidas
                break;

            case 2:
                VidasHUD[0].SetActive(false);
                VidasHUD[1].SetActive(true);
                VidasHUD[2].SetActive(false);
                break;

            case 1:
                VidasHUD[0].SetActive(true);
                VidasHUD[1].SetActive(false);
                VidasHUD[2].SetActive(false);
                break;

            default:
                VidasHUD[0].SetActive(false);
                VidasHUD[1].SetActive(false);
                VidasHUD[2].SetActive(false);
                break;

        }
    }

    public void ObjetivoConcluido()
    {
        //verificar se o objetivo foi concluido
        if (coletavelA >= coletavelAMAX && coletavelB >= coletavelBMAX)
        {
            panelWins.SetActive(true);
            Time.timeScale = 0; //caso 1 -> a animaçao no panel vai funcionar, porém o jogo continuará contando pontuação (caso for 1)
        }
    }

    public void saveLevel()
    {
        if (PlayerPrefs.GetInt("LevelComplete") < SceneManager.GetActiveScene().buildIndex)
        {
            PlayerPrefs.SetInt("LevelComplete", SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene("MenuInicial");
        }

        else
        {
             SceneManager.LoadScene("MenuInicial");
        }
    }  

    public void restartLevel() { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
    public void seguirLevel() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1); }
}    