using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    public static SoundControl sounds;

    public AudioSource somTiro, somPulo, somColectedFruits, somColectedCoins;
    public AudioSource somDanoNoPlayer, somDanoTiro;
    public AudioSource click, somGameOver, somWins;

    private void Awake()
    {
        sounds = this;

        // somTiro = Script: PlayerMove, Linha: 124 (Método Atirar())
        // somPulo = Script: PlayerMove, Linha: 85 (Método JumpPlayer())

        // somCollectedFruits = Script: InimigoControl, Linha: 34 e 41 
        // somCollectedCoins = Script: InimigoControl, Linha: 48 

        // somGameOver
        // somWinner

        // somDanoTiro = Script: InimigoControl, Linha: 70 
        // somDanoNoPlayer = Script: InimigoControl, Linha: 55

    }
}
