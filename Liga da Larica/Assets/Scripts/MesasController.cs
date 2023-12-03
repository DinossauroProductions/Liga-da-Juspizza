using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MesasController : MonoBehaviour
{

    public ArrayList mesas;



    // Start is called before the first frame update
    void Start(){

        mesas = new ArrayList();

        Transform[] children = transform.GetComponentsInChildren<Transform>();

        //Debug.Log(children[1].gameObject.Serialize());

        for (int i = 0; i < children.Length; i++){

            if (children[i].CompareTag("Mesa")){
                try{
                    mesas.Add(children[i].gameObject);
                }catch(NullReferenceException e){
                    Debug.Log(e);
                }
                
            }
        }
        //Debug.Log(mesas.Serialize());
    }

    // Update is called once per frame
    void Update(){

        
    }
}
