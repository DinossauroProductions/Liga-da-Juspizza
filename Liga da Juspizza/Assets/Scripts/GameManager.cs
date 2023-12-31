using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject pratoPrefab;
    [SerializeField] private GameObject clientePrefab; 
    private static GameObject[] pratos;
    public static ArrayList clientes;

    private static int pratosRequisitados = 0;
    private float pratoContador, pratoContadorMax;
    private float clienteContador, clienteContadorMax;

    //MesasController.mesas

    // Pontuação

    public static int pontuação = 0;


    public static void solicitarPrato(){
        pratosRequisitados++;
    }
    

    // Start is called before the first frame update
    void Start()
    {
        pratos = new GameObject[6];
        pratoContador = 0;
        pratoContadorMax = 2;

        clientes = new ArrayList{
            Capacity = 6
        };
        clienteContador = 0;
        clienteContadorMax = 6;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //adicionar os pratos
        if (pratosRequisitados > 0)
        {
            pratoContador += Time.fixedDeltaTime;
            if (pratoContador >= pratoContadorMax)
            {

                //Debug.Log("Tentar spawnar prato");
                

                pratoContador = 0;
                pratoContadorMax = RandomNumberGenerator.GetInt32(3) + 4; 

                for (int i = 0; i < pratos.Length; i++)
                {

                    if (pratos[i] == null)
                    {

                        GameObject pratoAdicionado = Instantiate(
                            pratoPrefab,
                            determinarPosicaoPrato((uint)i),
                            Quaternion.identity
                        );

                        pratos[i] = pratoAdicionado;
                        pratosRequisitados--;
                        break;
                    }

                }

            }
        }


        clienteContador += Time.fixedDeltaTime;
        if(clienteContador >= clienteContadorMax){

            clienteContador = 0;
            clienteContadorMax = 6 + RandomNumberGenerator.GetInt32(3) * clientes.Count;

            if(clientes.Count < clientes.Capacity){
                GameObject clienteAdicionado = Instantiate(clientePrefab);
                clientes.Add(clienteAdicionado);
                //clienteAdicionado.GetComponent<ClienteScript>().;
            }

        }
    }

    public static void removerPrato(GameObject prato){

        for(int i = 0; i < pratos.Length; i++){

            if(pratos[i] == prato){

                pratos[i] = null;
                return;

            }

        }

    }

    public static void removerCliente(GameObject cliente){

        clientes.Remove(cliente);

    }

    private Vector2 determinarPosicaoPrato(uint n){
        return new Vector2(-7.5f, 4.1f + n * -1.6f);
    }

    public void atualizarPontuação(){

    }
}
