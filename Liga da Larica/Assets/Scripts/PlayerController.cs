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
    private Transform plateSlot;
    // Reference to the currently held item.
    private PratoScript pickedItem = null;
    private Transform clientSlot;
    private ClienteScript followingClient = null;

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
        item.transform.SetParent(plateSlot);
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
    }

    private void leadClient(ClienteScript cliente){
        // Assign reference
        followingClient = cliente;
        // Disable rigidbody and reset velocities
        cliente.RigidBody.velocity = Vector2.zero;
        // Set Slot as a parent
        cliente.transform.SetParent(clientSlot);

        cliente.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    public void DropClient(ClienteScript cliente)
    {
        //Debug.Log("Cliente \"entregue\"");
        // Remove reference
        followingClient = null;
        // Remove parent
        cliente.transform.SetParent(null);
        // Enable rigidbody
        //cliente.RigidBody.isKinematic = false;
        cliente.gameObject.layer = LayerMask.NameToLayer("Selecionáveis");
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

            if(pickedItem != null || followingClient != null){
                if(pickedItem != null)
                    DropItem(pickedItem);
                if(followingClient != null)
                    DropClient(followingClient);
            }
            else{
                //Atira um raycast para verificar se há itens
                RaycastHit2D[] hit;
                hit = Physics2D.RaycastAll(transform.position, anguloAnterior);

                // Verifica se atingiu algo
                if (!(hit.Length == 0))
                {
                    for (int i = 0; i < hit.Length; i++)
                    {
                        //Verifica se foi próximo
                        if(hit[i].distance < 2f){
                            //Verifica se foi um prato
                            if (hit[i].collider.CompareTag("Prato")){
                                var pickable = hit[i].collider.GetComponent<PratoScript>();
                                PickItem(pickable);
                            }
                            //Verifica se foi um cliente
                            else if (hit[i].collider.CompareTag("Cliente")){
                                var pickable = hit[i].collider.GetComponent<ClienteScript>();
                                leadClient(pickable);
                            }
                        }
                        
                    }
                
                }

            }
        }

        if(pickedItem != null){
            pickedItem.GetComponent<Transform>().position = transform.position + (Vector3) anguloAnterior * speed * 0.2f + new Vector3(0, 0, -1);
        }
        if(followingClient != null){
            Transform posicaoCliente = followingClient.GetComponent<Transform>();
            Vector2 distancia = transform.position - posicaoCliente.position;
            if(distancia.magnitude > 2f){
                //Debug.Log(distancia);
                posicaoCliente.position += (Vector3) distancia * (3f * Time.deltaTime);
            }
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
