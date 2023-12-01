using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CozinhaScript : MonoBehaviour
{

    [SerializeField] private GameObject pratoPrefab;

    public ArrayList pratos;

    private float counter;

    private static Vector3 distancia = new Vector3(0, -1.6f, 0);

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
        counter += Time.deltaTime;
        if(counter >= 2){

            counter = 0;

            if(pratos.Count < 6){

                Vector3 posição = new Vector3(-7.5f, 4.1f, 0) + distancia * pratos.Count;

                GameObject pratoAdicionado = Instantiate(pratoPrefab, posição, Quaternion.identity);

                pratos.Add(pratoAdicionado);

            }
            else{
                Debug.Log("Os pratos estão lotados!!");
            }

        }
    }
}
