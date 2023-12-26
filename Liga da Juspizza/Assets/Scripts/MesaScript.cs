using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MesaScript : MonoBehaviour
{

    public bool ocupado = false;

    public bool servida = false;

    public float ticker = 0;
    public int contadorPaciencia = 0;
    public int contadorComida = 0;
    public int pontuacaoTempo = 50;
    [SerializeField]
    private TextoPontuaçãoScript texto;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

        GameObject[] objs = SceneManager.GetActiveScene().GetRootGameObjects();

        //Debug.Log(objs.Serialize());
        for(int i = 0; i < objs.Length; i++){
            TextoPontuaçãoScript obj = objs[i].GetComponentInChildren<TextoPontuaçãoScript>();
            if(obj){
                //Debug.Log("loop" + obj.Serialize());

                texto = obj;

            }
        }
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
                    if(contadorPaciencia >= 14){

                        pontuacaoTempo--;

                        if(pontuacaoTempo < 20){
                            pontuacaoTempo = 20;
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
        pontuacaoTempo = 50;

    }

    public void servirPrato(PratoScript prato){

        servida = true;
        GameManager.pontuação += pontuacaoTempo;
        //Debug.Log("Atualizando pontos");
        texto.atualizarTexto("Pontos: " + GameManager.pontuação);

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
