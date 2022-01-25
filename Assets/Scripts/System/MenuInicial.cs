using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInicial : MonoBehaviour
{
    //Habilitar \\ desabilitar paneis do menu inicial
    public GameObject panelTelaInicial, panelSelecaoFase, panelSettings;

    public Sprite[] faseBloqueada;

    public Button[] btnLevel;
    public int btnDesbloqueados = 0;

    private void Start()
    {
        panelTelaInicial.SetActive(true);
        panelSelecaoFase.SetActive(false);

        panelSettings.SetActive(false);

        //Valor dessa var é igual ao valor da fase carregada (fase1 = 1, fase2 = 2..
        btnDesbloqueados = PlayerPrefs.GetInt("LevelComplete");
    }

    private void Update()
    {
        //valor de i sempre é inicializado com o valor que esta na var btnDesbloqueado
        //esse valor de inicialização reflete em qual btn do array btnLevel, sera habilitado
        for (int i = btnDesbloqueados; i < btnLevel.Length; i++)
        {
            btnLevel[i].interactable = false;
            btnLevel[i].image.sprite = faseBloqueada[i];
        }
    }

    #region Panels

    public void ativarSelecaoFasel()
    {
        SoundControl.sounds.click.Play();
        panelTelaInicial.SetActive(false);
        panelSelecaoFase.SetActive(true);
    }

    public void ativarPanelInicial()
    {
        SoundControl.sounds.click.Play();
        panelTelaInicial.SetActive(true);
        panelSelecaoFase.SetActive(false);

        panelSettings.SetActive(false);
    }
    #endregion

    public void Settings() { panelSettings.SetActive(true); }


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

        Application.Quit();
    }
}
