using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInicial : MonoBehaviour
{
    //Habilitar \\ desabilitar paneis do menu inicial
    public GameObject panelTelaInicial, panelSelecaoFase;

    public Button[] btnLevel;
    public int btnDesbloqueados = 0;

    private void Start()
    {
        panelTelaInicial.SetActive(true);
        panelSelecaoFase.SetActive(false);

        //Valor dessa var � igual ao valor da fase carregada (fase1 = 1, fase2 = 2..
        btnDesbloqueados = PlayerPrefs.GetInt("LevelComplete");
    }

    private void Update()
    {
        //valor de i sempre � inicializado com o valor que esta na var btnDesbloqueado
        //esse valor de inicializa��o reflete em qual btn do array btnLevel, sera habilitado
        for (int i = btnDesbloqueados; i < btnLevel.Length; i++)
        {
            btnLevel[i].interactable = false;
        }
    }

    #region Panels

    public void ativarSelecaoFasel()
    {
        panelTelaInicial.SetActive(false);
        panelSelecaoFase.SetActive(true);
    }

    public void ativarPanelInicial()
    {
        panelTelaInicial.SetActive(true);
        panelSelecaoFase.SetActive(false);
    }
    #endregion

    public void levelName(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void objMaca(int objMaca)
    {
        PlayerPrefs.SetInt("objetivoMaxMaca", objMaca);
    }

    public void objLaranja(int objLaranja)
    {
        PlayerPrefs.SetInt("objetivoMaxLaranja", objLaranja);
    }

    public void zerarSaves()
    {
        PlayerPrefs.DeleteAll();
    }
}
