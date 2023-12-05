using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MesaScript : MonoBehaviour
{

    public bool ocupado = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D (Collider2D other){
        //Debug.Log("Colisao");
        if(other.CompareTag("Cliente")){
            //Debug.Log("Colisão com cliente");
            other.GetComponent<ClienteScript>().colidirComMesa(this);
        }
    }
}
