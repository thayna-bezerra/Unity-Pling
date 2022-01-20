using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
//private AudioSource sound;

    [Header("Controle de movimentação/pulo")]
    public float speed = 5;
    public float jump = 5;

    //detectar o chão/evitar dois pulos
    public bool isJumping; //padrão FALSE
    public bool IsAtk;
    public bool IsActive;

    public bool isDamage; //

    [Header ("----------------------------------")]
    [System.NonSerialized] public Rigidbody2D pulo;
    [System.NonSerialized] public Animator AnimacoesPlayer;
    
    [Header ("Controle")]
    public Joystick joystick;
    public GameController gc;

    [Header ("Objetos para spawn")]
    public GameObject WaterBullet, smoke;

    public Transform SpawnPosition;

    public float cont = 0.5f;//

    private void Start()
    {
        //sound = GetComponent<AudioSource>();

        gc = GameObject.Find("GameController").GetComponent<GameController>();
        pulo = GetComponent<Rigidbody2D>();
        AnimacoesPlayer = GetComponent<Animator>();
    }

    private void Update()
    {
        if(gc.VidaPlayer > 0)
        {
            MovePlayer(); 
            AnimStatePlayer();
        }

        else
        {
            AnimacoesPlayer.Play("DeadPlayer");
        }
    }

    public void MovePlayer()
    {
        float a = joystick.Horizontal; //variável 'a' recebendo as propriedades do joystick
        transform.position += a * new Vector3(speed * Time.deltaTime, 0f, 0f); //manipulando a variável do joystick

       //mirror -- seguindo coordenada X positivo / negativo
        if(a < 0)
        {
            IsActive = true; //está ativo andando
            transform.eulerAngles = new Vector3(0f, 180f, 0f); 
        }

        else if (a > 0)
        {
            IsActive = true; //está ativo andando
            transform.eulerAngles = new Vector3(0f, 0f, 0f); 
        }

        else 
        {
            IsActive = false; //parado
        }
    }

    public void JumpPlayer()
    {
        if(!isJumping) //se não estiver pulando (variável false_)
        {  
            Instantiate (smoke, transform.position, transform.rotation); //spawn da fumaça quando o player sai do chão
            isJumping = true;  //esta pulando fica verdadeiro quando a tecla é pressionada
            pulo.AddForce(new Vector2 (0f, jump), ForceMode2D.Impulse); //física do pulo
            SoundControl.sounds.somPulo.Play();
        }
    }

    public void AnimStatePlayer()
    {
        if (isDamage == false)
        {
            if (IsAtk) //se player estiver atacando // animação de ataque
            {
                //primeira condição pois caso o player NÃO esteja atacando
                // as outras animações poderão ser chamadas normalmente
                AnimacoesPlayer.Play("AtirarPlayer");
            }

            else //senão estiver atacando
            {
                if (!isJumping) // e se estiver no chão (groundCheck colidindo com o chão / player NÃO estiver pulando)
                {
                    if (IsActive) //se o player estiver caminhando (em movimentação)
                    {
                        AnimacoesPlayer.Play("WalkPlayer");
                    }

                    else //SE NÃO ESTIVER CAMINHANDO -- isActive no false -- gato parado
                    {
                        AnimacoesPlayer.Play("IdlePlayer");
                    }
                }

                else if (isJumping) //se NÃO estiver no chão -- isJumping TRUE -- animação de PULAR
                {
                    AnimacoesPlayer.Play("JumpPlayer");
                }
            }
        }

        else if (isDamage == true)
        {
            cont -= Time.deltaTime;
            AnimacoesPlayer.Play("DanoPlayer");

            if (cont < 0) { isDamage = false; }
        }
    }

    /*
    public void AnimStatePlayer ()
    {
        if (IsAtk) //se player estiver atacando // animação de ataque
        {
            //primeira condição pois caso o player NÃO esteja atacando
            // as outras animações poderão ser chamadas normalmente
            AnimacoesPlayer.Play("AtirarPlayer");
        }

        else //senão estiver atacando
        {
            if (!isJumping) // e se estiver no chão (groundCheck colidindo com o chão / player NÃO estiver pulando)
            {
                if (IsActive) //se o player estiver caminhando (em movimentação)
                {
                    AnimacoesPlayer.Play("WalkPlayer");
                }

                else //SE NÃO ESTIVER CAMINHANDO -- isActive no false -- gato parado
                {
                    AnimacoesPlayer.Play("IdlePlayer");
                }
            }

            else if (isJumping == true) //se NÃO estiver no chão -- isJumping TRUE -- animação de PULAR
            {
                AnimacoesPlayer.Play("JumpPlayer");
            }

        }
    }*/
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Inimigo")
        {
            isDamage = true;
            cont = 0.5f;
        }
    }

    public void Atirar()
    {
        if(!IsAtk)
        {
            IsAtk = true;
            SoundControl.sounds.somTiro.Play();
            Instantiate(WaterBullet, SpawnPosition.position, transform.rotation);
            Invoke("SpawnTiro", 0.2f);
        }
    }

    public void SpawnTiro() { IsAtk = false; }
}