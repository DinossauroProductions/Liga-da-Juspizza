using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClienteScript : MonoBehaviour
{
    // Reference to the rigidbody
    private Rigidbody2D rigidBody;
    public Rigidbody2D RigidBody => rigidBody;
    private Vector2 targetPosition;

    private static Vector3 startPosition = new(10, -0.81f, 1.5f);
    private static Vector2 startPositionTarget = new(6.5f, -0.81f);


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Clente Start function");
        rigidBody = GetComponent<Rigidbody2D>();

        transform.position = startPosition.CloneViaFakeSerialization();
        startPositionTarget.y += Random.Range(-1.0f, 1.0f) / 2;
        targetPosition = startPositionTarget.CloneViaFakeSerialization();

        RigidBody.isKinematic = false;

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
            transform.position = mesa.transform.position + new Vector3(0, 1f, 1f);
            transform.
            GetComponent<Rigidbody2D>().simulated = false;
        }
        
        
    }

    public void sairDoRestaurante(){

        GameManager.removerCliente(gameObject);
        Destroy(gameObject);

    }

    
}
