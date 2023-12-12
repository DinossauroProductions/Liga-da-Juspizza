using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;


[RequireComponent(typeof(Rigidbody2D))]
public class PratoScript : MonoBehaviour
{

    // Reference to the rigidbody
    private Rigidbody2D rigidBody;
    public Rigidbody2D RigidBody => rigidBody;


    // Start is called before the first frame update
    void Start()
    {

        rigidBody = GetComponent<Rigidbody2D>();

        //Transform transform = GetComponent<Transform>();
        //transform.position = new Vector2(-7.5f, 4.3f);
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void checarMesa(){

        Collider2D[] colisoes = {};
            
        //ContactFilter2D filtro = new ContactFilter2D();
        //filtro.SetLayerMask(LayerMask.GetMask("Ignore Raycast"));

         colisoes = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius, 0b0000000000000000000000000100);


        for(int i = 0; i < colisoes.Length; i++){
            if(colisoes[i].gameObject.CompareTag("Mesa")){

                if(colisoes[i].GetComponentInChildren<MesaScript>().ocupado == false){
                    continue;
                }

                // //se ainda estiver na mão do player, saia dela
                // if(transform.parent != null) 
                //     transform.parent.parent.GetComponent<PlayerController>().DropItem(this);

                //se cole na mesa
                transform.SetParent(colisoes[i].transform);
                transform.position = new Vector3(colisoes[i].transform.position.x, colisoes[i].transform.position.y+0.1f, 0);

                GetComponent<Rigidbody2D>().simulated = false;

                //dizer para a mesa que ela foi servida!
                colisoes[i].transform.GetComponentInChildren<MesaScript>().servida = true;

            }
        }  
        
    }


    // private void OnTriggerEnter2D(Collider2D other){

    //     if(other.CompareTag("Mesa")){

    //         if(transform.parent == null){

    //             colidiuComMesa = true;
    //             mesaColisora = other;

    //         }
    //     }

    // }
}
