using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //private Transform transform;
    private Rigidbody2D body;
    private new Transform transform;

    private Vector2 angulo;
    private Vector2 anguloAnterior;
    private Vector2 direção;

    private bool up = false, down = false, left = false, right = false, action_button = false;

    [SerializeField] 
    private float speed = 40f;

    //[SerializeField] private ArrayList r_pratos;

    // Reference to the slot for holding picked item.
    [SerializeField]
    private Transform slot;
    // Reference to the currently held item.
    private PratoScript pickedItem = null;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        body = GetComponent<Rigidbody2D>();
        angulo = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        ReceberInput();
        ProcessarInput();

        


    }

    private void PickItem(PratoScript item)
    {
        Debug.Log("Pickup");
        // Assign reference
        pickedItem = item;
        // Disable rigidbody and reset velocities
        item.RigidBody.isKinematic = true;
        item.RigidBody.velocity = Vector2.zero;
        item.RigidBody.angularVelocity = 0;
        // Set Slot as a parent
        item.transform.SetParent(slot);
        // Reset position and rotation
        item.transform.localPosition = Vector3.zero;
        item.transform.localEulerAngles = Vector3.zero;
    }
    private void DropItem(PratoScript item)
    {
        Debug.Log("Drop");
        // Remove reference
        pickedItem = null;
        // Remove parent
        item.transform.SetParent(null);
        // Enable rigidbody
        item.RigidBody.isKinematic = false;
        // Add force to throw item a little bit
        item.RigidBody.AddForce(item.transform.forward * 2, ForceMode2D.Impulse);
    }

    void ProcessarInput(){

        angulo = Vector2.zero;

        // Transformar os botões pressionados em um vetor de direção
        if(up){
            angulo.y += 1;
            up = false;
        }
        if(down){
            angulo.y -= 1;
            down = false;
        }
        if(left){
            angulo.x -= 1;
            left = false;
        }
        if(right){
            angulo.x += 1;
            right = false;
        }

        angulo.Normalize();

        // Processar o vetor para mover o player
        if(!angulo.Equals(Vector2.zero)){
            anguloAnterior = angulo;
        }
        else{

        }
        float speedDinamica = speed;

        direção = angulo * speedDinamica;

        body.velocity += direção;
        body.velocity /= 2;


        // Utilizar o vetor para olhar para frente em busca de um objeto para pegar
        if(action_button){
            action_button = false;
            //Debug.Log("Botão de ação processado");

            if (pickedItem != null){
                //Debug.Log("Item dropado");
                DropItem(pickedItem);
            }
            else{
                //Atira um raycast para verificar se há itens
                RaycastHit2D hit;
                hit = Physics2D.Raycast(transform.position, anguloAnterior);
                //Debug.Log("Procurando itens: "+hit.Serialize());
                if(hit){
                    if (hit.collider.CompareTag("Prato")){
                        //Debug.Log("Item encontrado, procurando item do tipo pegável: "+hit.Serialize());
                        var pickable = hit.collider.GetComponent<PratoScript>();
                        //Debug.Log("Item pego");
                        PickItem(pickable);
                    
                    }
                }
            }
        }

        if(pickedItem != null){
            pickedItem.GetComponent<Transform>().position = transform.position + (Vector3) anguloAnterior * speed * 0.2f + new Vector3(0, 0, -1);
        }
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
        if(Input.GetKeyDown(KeyCode.E)){
            //Botão de interação
            action_button = true;
        }
    }
}
