using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class ClienteScript : MonoBehaviour
{
    // Reference to the rigidbody
    private Rigidbody2D rigidBody;
    public Rigidbody2D RigidBody => rigidBody;
    private Vector2 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        
        rigidBody = GetComponent<Rigidbody2D>();

        RigidBody.position = new Vector3(10, 0, 1.5f);
        targetPosition = new Vector2(6.5f, 0);


        //Transform transform = GetComponent<Transform>();
        //transform.position = new Vector2(-7.5f, 4.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if(targetPosition != Vector2.zero){
            Vector2 distancia = targetPosition - RigidBody.position;
            RigidBody.position += distancia * (1.5f * Time.deltaTime);
            
            if(distancia.magnitude < 0.3f)
                targetPosition = Vector2.zero;
        }

    }

    public void colidirComMesa(MesaScript mesa){

        Debug.Log("Colisão recebida");

        if(transform.parent != null){

            //Debug.Log("transfowmda");

            transform.parent.parent.GetComponent<PlayerController>().DropClient(this);
            mesa.GetComponent<MesaScript>().ocupado = true;
            transform.position = mesa.transform.position + new Vector3(0, 1f, 0);
        }
        
        
    }

    
}
