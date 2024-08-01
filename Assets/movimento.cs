using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class movimento : MonoBehaviour
{
    private float horizontalInput; // Sera respondavel por amazenar os valores de entrada do nosso teclado
    private Rigidbody2D rb; //ira fazere refencia ao objeto rigidbody do nosso personagem
   [SerializeField] private int velocidade = 5;
    [SerializeField] private Transform peDoPersonagem;
    [SerializeField] private LayerMask chaoLayer;

    private bool estaNoChao; // variavel sera verdadeira sempre que o personagem estiver no chao
                             // se tornaram falsa no momento em que ele nao estiver mais tocando no chao

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private int movendoHash = Animator.StringToHash("movendo");
    private int saltandoHash = Animator.StringToHash("saltando");

    private void Awake() //referenciando 
    {
        rb = GetComponent<Rigidbody2D>(); //basicamente esse comando ira procurar no nosso personagem o componente do tipo rigidbody, ao encontralo, anexara  a variavel rb
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Start()
    {
        
    }

    
    void Update() //leituas continuas de entrada sejam feitas sempre no metodo  de UpDate, para evitar perdas e duplicações
    {

        horizontalInput = Input.GetAxis("Horizontal"); // esse comando ira capturar nossas setas de esquerda e direita do nosso teclado

        if(Input.GetKeyDown(KeyCode.Space) && estaNoChao) //se eu aperta minha tecla de espaço, vamos entra dentro desse if
                                          //dessa forma so sera permitido o salto se a gente aperta a tecla, e o personagem ja estiver no 
        {
            rb.AddForce(Vector2.up * 600);  //passamos para esse metodo a direçao(vector2) da força que queremos adicionar e sua intensidade


        }
                      //esse comando cria um circulo invisivel em determinado ponto que descidimos e verifica quais objetos colider estao dentro da sua area 
        estaNoChao = Physics2D.OverlapCircle(peDoPersonagem.position, 0.2f, chaoLayer);
                                         //ponto central,raio do circulo invisivel, qual layer queremos verificar

        animator.SetBool("movendo", horizontalInput != 0);
        animator.SetBool("saltando", !estaNoChao);

        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false; // fique virado para o lado direito
        }
        // se nao
        else if(horizontalInput < 0)  //significa que eu estou me movendo para a esquerda
        { 
        spriteRenderer.flipX = true;
        }
    }


    private void FixedUpdate() // logicas continuas envolvendo fisica, devem ser executadas no metodo FixedUpDate
    {
        rb.velocity = new Vector2(horizontalInput * velocidade, rb.velocity.y);

    }

}
