using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MesaScript : MonoBehaviour
{

    public bool ocupado = false;

    public bool servida = false;

    public float ticker = 0;
    public int contadorPaciencia = 0;
    public int contadorComida = 0;
    public int pontuacaoTempo = 100;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    // Update is called once per frame
    void Update()
    {

        if(ocupado){

            ticker += Time.deltaTime;

            if(ticker >= 1){
                ticker -= 1;
                
                


                // Tick para consumir a comida
                if(servida){

                    contadorComida++;
                    
                    if(contadorComida >= 15){

                        ocupado = false;
                        //servida = false;
                        
                        transform.parent.GetComponentInChildren<ClienteScript>().sairDoRestaurante();

                        transform.parent.GetComponentInChildren<PratoScript>().ficarSujo();

                    }

                    return;
                }

                else{
                    contadorPaciencia++;

                    // Tick para esperar a comida
                    if(contadorPaciencia >= 23){

                        pontuacaoTempo--;

                        if(pontuacaoTempo < 50){
                            pontuacaoTempo = 50;
                        }

                    }
                }

            }

        }
        
    }

    public void resetarVariaveisMesa(){

        ticker = 0;
        contadorPaciencia = 0;
        contadorComida = 0;
        pontuacaoTempo = 100;

    }

    public void servirPrato(PratoScript prato){

        servida = true;
        GameManager.pontuação += pontuacaoTempo;

    }



    private void OnTriggerEnter2D (Collider2D other){
        //Debug.Log("Colisao");
        if(!ocupado && !servida)
            if(other.CompareTag("Cliente")){
                //Debug.Log("Colisão com cliente");
                other.GetComponent<ClienteScript>().colidirComMesa(this);
            }
    }

    
}
