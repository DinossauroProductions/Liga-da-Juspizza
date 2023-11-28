using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //private Transform transform;
    private Rigidbody2D body;

    private bool up, down, left, right = false;

    [SerializeField] private float speed = 40f;

    // Start is called before the first frame update
    void Start()
    {
        //transform = GetComponent<Transform>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ReceberInput();
        ProcessarInput();
    }

    void ProcessarInput(){

        Vector2 direção = new();

        if(up){
            direção.y += 1;
            up = false;
        }
        if(down){
            direção.y -= 1;
            down = false;
        }
        if(left){
            direção.x -= 1;
            left = false;
        }
        if(right){
            direção.x += 1;
            right = false;
        }

        direção.Normalize();

        float speedDinamica = speed;

        direção = direção * speedDinamica;

        body.velocity += direção;
        body.velocity /= 2;

    }

    void ReceberInput(){
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
            //Cima
            up = true;

        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
            //Baixo
            down = true;

        }

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            //Esquerda
            left = true;

        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            //Cima
            right = true;

        }
    }
}
