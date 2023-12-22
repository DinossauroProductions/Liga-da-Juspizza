using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClienteScript : MonoBehaviour
{
    // Reference to the rigidbody
    private Rigidbody2D rigidBody;
    public Rigidbody2D RigidBody => rigidBody;

    // Movimentação inicial

    private Vector2 targetPosition;
    private static Vector3 startPosition = new(10, -0.81f, 1.5f);
    private static Vector2 startPositionTarget = new(6.5f, -0.81f);


    // Animação

    [SerializeField]
    private Sprite[] sprites = new Sprite[16];
    private enum direçãoAnimaçãoIndex{
        CIMA = 2,
        BAIXO = 0,
        ESQUERDA = 3,
        DIREITA = 1
    }
    private direçãoAnimaçãoIndex direçãoAnimação = direçãoAnimaçãoIndex.BAIXO;
    private int animaçãoStep = 0;
    private SpriteRenderer spriteRenderer;
    private double animationCounter = 0, animationCounterMax = 140;


    private Vector2 direção, localAnterior;


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Clente Start function");
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        transform.position = startPosition.CloneViaFakeSerialization();
        startPositionTarget.y += UnityEngine.Random.Range(-1.0f, 1.0f) / 2;
        targetPosition = startPositionTarget.CloneViaFakeSerialization();

        RigidBody.isKinematic = false;
        direção = new Vector2();
        localAnterior = (Vector2) transform.position;

        //Transform transform = GetComponent<Transform>();
        //transform.position = new Vector2(-7.5f, 4.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if(targetPosition != Vector2.zero){
            Vector2 distancia = (Vector3) targetPosition - transform.position;
            transform.position += (Vector3) distancia * (1.5f * Time.deltaTime);
            
            if(distancia.magnitude < 0.3f)
                targetPosition = Vector2.zero;
        }

        if(transform.parent){
            if(transform.parent.GetComponentInChildren<MesaScript>() != null){
                spriteRenderer.sprite = sprites[(int)direçãoAnimaçãoIndex.BAIXO * 4];
            }
            else{
            Animar();
            }
        }
        else{
            Animar();
        }

    }

    private void Animar(){

        // Incrementar animações
        animationCounter += Time.deltaTime * 1000;

        if(animationCounter >= animationCounterMax){
            animationCounter -= animationCounterMax;

            //Debug.Log("Framde de animação");


            direção = (Vector2) transform.position - localAnterior;

            localAnterior = (Vector2) transform.position;

            float x = Math.Abs(direção.x);
            float y = Math.Abs(direção.y);


            // Se X for maior ou igual a Y, então é horizontal

            if(x >= y){
                direçãoAnimação = direção.x > 0 ? 
                direçãoAnimaçãoIndex.DIREITA :   // Se X for positivo, então é pra direita
                direçãoAnimaçãoIndex.ESQUERDA;   // Se X for negativo, então é pra esquerda
            }
            // Se Y for maior que X, então é vertical
            else{
                direçãoAnimação = direção.y > 0 ? 
                direçãoAnimaçãoIndex.CIMA :      // Se Y for positivo, então é pra cima
                direçãoAnimaçãoIndex.BAIXO;      // Se Y for negativo, então é pra baixo
            }
            
            if(!direção.Equals(Vector2.zero)){

                // Se o npc estiver se movendo, a animação acontece
                animaçãoStep++;

                if(animaçãoStep >= 4){
                    animaçãoStep = 0;

                }
                spriteRenderer.sprite = sprites[(int) direçãoAnimação * 4 + animaçãoStep];

            }
            else{

                spriteRenderer.sprite = sprites[(int) direçãoAnimação * 4];

            }

        }

    }

    public void colidirComMesa(MesaScript mesa){

        //Debug.Log("Colisão recebida");

        if(transform.parent != null){

            //Debug.Log("transfowmda");

            transform.parent.parent.GetComponent<PlayerController>().DropClient(this);
            transform.SetParent(mesa.transform.parent);
            mesa.GetComponent<MesaScript>().ocupado = true;
            mesa.GetComponent<MesaScript>().resetarVariaveisMesa();
            GameManager.solicitarPrato();
            transform.position = mesa.transform.position + new Vector3(0, 1.3f, 1f);
            transform.
            GetComponent<Rigidbody2D>().simulated = false;
        }
        
        
    }

    public void sairDoRestaurante(){

        GameManager.removerCliente(gameObject);
        Destroy(gameObject);

    }

    
}
