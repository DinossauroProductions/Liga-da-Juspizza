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

    [SerializeField]
    private Sprite pratoSujoSprite;

    public bool estaSujo = false;
    public bool servido = false;


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

                MesaScript mesa = colisoes[i].GetComponentInChildren<MesaScript>();

                if(mesa.ocupado == false || mesa.servida == true){
                    continue;
                }

                // //se ainda estiver na mão do player, saia dela
                // if(transform.parent != null) 
                //     transform.parent.parent.GetComponent<PlayerController>().DropItem(this);

                //se cole na mesa
                transform.SetParent(colisoes[i].transform);
                transform.position = new Vector3(colisoes[i].transform.position.x, colisoes[i].transform.position.y+0.1f, 0);

                //GetComponent<Rigidbody2D>().simulated = false;

                //dizer para a mesa que ela foi servida!
                mesa.servirPrato(this);
                servido = true;

            }
        }  
        
    }

    public void ficarSujo(){

        estaSujo = true;
        GetComponentInChildren<SpriteRenderer>().sprite = pratoSujoSprite;

    }

    public void sumir(){

        transform.parent.GetComponentInChildren<MesaScript>().servida = false;
        Destroy(gameObject);

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
