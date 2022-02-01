using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
    public static SoundControl sounds;

    [Space(10)]

    [Header("Configuração dos Sons")]
    public bool audioOn;
    public Button btnAudio;
    public Sprite btnEmptyOff;
    public Sprite btnOn;

    [Space(10)]

    [Header("Efeitos Sonoros")]
    public AudioSource somTiro;
    public AudioSource somPulo;
    public AudioSource somColectedFruits;
    public AudioSource somColectedCoins;
    public AudioSource click;
    public AudioSource somDanoNoPlayer;
    public AudioSource somDanoTiro;


    private void Awake() { sounds = this; } //inicializar //instanciar classe

    public void MusicGame() //esse metodo sera chamado no botão "btnAudio"
    {
        audioOn = !audioOn; //verificar qual estado atual da bool audioOn

        if (audioOn == true)
        {
            AudioListener.volume = 1; 
            btnAudio.image.sprite = btnOn;
        }

        else if (audioOn == false)
        {
            AudioListener.volume = 0;
            btnAudio.image.sprite = btnEmptyOff;
        }
    }


    // somTiro = Script: PlayerMove, Linha: 124 (Método Atirar())
    // somPulo = Script: PlayerMove, Linha: 85 (Método JumpPlayer())

    // somCollectedFruits = Script: InimigoControl, Linha: 34 e 41 
    // somCollectedCoins = Script: InimigoControl, Linha: 48 

    // somDanoTiro = Script: InimigoControl, Linha: 70 
    // somDanoNoPlayer = Script: InimigoControl, Linha: 55
}