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
    private ArrayList pratos;
    public ArrayList clientes;

    private float pratoContador, pratoContadorMax;
    private float clienteContador, clienteContadorMax;

    
    

    // Start is called before the first frame update
    void Start()
    {
        pratos = new ArrayList
        {
            Capacity = 6
        };
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
        pratoContador += Time.fixedDeltaTime;
        if(pratoContador >= pratoContadorMax){

            pratoContador = 0;
            pratoContadorMax = RandomNumberGenerator.GetInt32(6) + 6;

            if(pratos.Count < pratos.Capacity){

                GameObject pratoAdicionado = Instantiate(
                    pratoPrefab, 
                    determinarPosicaoPrato((uint)Math.Abs(pratos.Count)), 
                    Quaternion.identity
                    );

                pratos.Add(pratoAdicionado);

            }
            else{
                //Debug.Log("Os pratos estão lotados!!");
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
        //adicionar os clientes
    }

    private Vector2 determinarPosicaoPrato(uint n){
        return new Vector2(-7.5f, 4.1f + n * -1.6f);
    }
}
