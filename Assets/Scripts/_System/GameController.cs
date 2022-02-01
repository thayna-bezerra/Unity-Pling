using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Atributos Player")]
    public int VidaPlayer = 3;
    public int danoPlayer = 10;

    [Space(10)]

    [Header("Coletáveis")]
    public int MoedaColet = 0;
    public int coletavelA = 0, coletavelAMAX; 
    public int coletavelB = 0, coletavelBMAX;

    [Space(10)]

    [Header("Objetos que serão spawnados")]
    public GameObject Inimigo;
    public GameObject ColetavelA;
    public GameObject ColetavelB;
    public GameObject MoedaColetavel;

    [Space(10)]

    [Header("Controle de Tempo dos Spawns - Coletáveis")]
    public float delaySpawnInimigo = 3; 
    public float delaySpawnColetavelA = 2.1f; 
    public float delaySpawnColetavelB = 2.5f; 
    public float delaySpawnMoedaColetavel = 2.5f; 

    [Space(10)]

    [Header("Panel de Vitória e Derrota")]
    public GameObject panelWins;
    public GameObject panelGameOver;

    [Space(10)]

    [Header("HUD")]
    public Text tMoedaColet;
    public Text tcoletavelA;
    public Text tcoletavelB;
    public GameObject[] VidasHUD = new GameObject[3];

    [Space(10)]

    public PlayerMove player;
    ChefaoStatus chefaoStatus; // identificar quanto de vida tem o chefão

    //[System.NonSerialized] Para esconder uma variável pública no Inspector
    //[SerializeField] Para Serializar uma variável privada // Para a variável aparecer no Inspector

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "FaseChefao") //acessando
        {
            chefaoStatus = GameObject.FindGameObjectWithTag("Chefao").GetComponent<ChefaoStatus>();
        }
        
        panelWins.SetActive(false); //desabilita panel de vitoria
        
        Time.timeScale = 1; //Escada do tempo = 1 (normal)

        //carrega o valor do objetivo Max de acordo com a fase escolhida
        //coletavelAMAX = PlayerPrefs.GetInt("objetivoMaxMaca");
        //coletavelBMAX = PlayerPrefs.GetInt("objetivoMaxLaranja");
    }

    private void Update()
    {
        if (VidaPlayer > 0)
        {
            //os spawns so acontecem quando não for a tela do chefão
            if (SceneManager.GetActiveScene().name != "FaseChefao") { SpawnAll(); }

            else if (SceneManager.GetActiveScene().name == "FaseChefao")
            {
                if(chefaoStatus.currentLife <= 0) { SpawnAll(); }
            }

            ObjetivoConcluido();
            panelGameOver.SetActive(false);
        }

        else { panelGameOver.SetActive(true); }

            HUDdisplay();
    }

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

    void HUDdisplay()
    {
        tMoedaColet.text = MoedaColet.ToString();
        tcoletavelA.text = coletavelA.ToString() + "/" + coletavelAMAX.ToString();
        tcoletavelB.text = coletavelB.ToString() + "/" + coletavelBMAX.ToString();

        if (coletavelA >= coletavelAMAX) { tcoletavelA.text = "<color=#ff0000>" +  coletavelA.ToString() + "/" + coletavelAMAX.ToString() + "</color>"; } 
        if (coletavelB >= coletavelBMAX) { tcoletavelB.text = "<color=#ff0000>" +  coletavelB.ToString() + "/" + coletavelBMAX.ToString() + "</color>"; }
       
        #region HUD Life
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
        #endregion
    }

    public void ObjetivoConcluido()
    {
        if (coletavelA >= coletavelAMAX && coletavelB >= coletavelBMAX) //verificar se o objetivo foi concluido
        {
            panelWins.SetActive(true);
            Time.timeScale = 0; //tempo zerado -> animação do panelWin não vai rodar
        }
    }

    public void saveLevel()
    {
        if (PlayerPrefs.GetInt("LevelComplete") < SceneManager.GetActiveScene().buildIndex)
        {
            PlayerPrefs.SetInt("LevelComplete", SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene("MenuInicial");
        }

        else { SceneManager.LoadScene("MenuInicial"); }
    }  

    public void restartLevel() { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
    public void seguirLevel() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1); }
}    