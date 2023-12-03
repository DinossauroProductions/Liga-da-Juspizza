using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject pratoPrefab;

    public ArrayList pratos;

    private float counter;

    // Start is called before the first frame update
    void Start()
    {
        pratos = new ArrayList
        {
            Capacity = 5
        };
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //adicionar os pratos
        counter += Time.deltaTime;
        if(counter >= 2){

            counter = 0;

            if(pratos.Count < 6){

                GameObject pratoAdicionado = Instantiate(
                    pratoPrefab, 
                    determinarPosicaoPrato((uint)Math.Abs(pratos.Count)), 
                    Quaternion.identity
                    );

                pratos.Add(pratoAdicionado);

            }
            else{
                Debug.Log("Os pratos estão lotados!!");
            }

        }

        //adicionar os clientes
    }

    private Vector2 determinarPosicaoPrato(uint n){
        return new Vector2(-7.5f, 4.1f + n * -1.6f);
    }
}
