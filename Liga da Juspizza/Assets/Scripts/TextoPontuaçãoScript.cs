using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextoPontuaçãoScript : MonoBehaviour
{
    private TextMeshProUGUI texto;

    void Start()
    {
        texto = transform.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //atualizarTexto("oi");
    }

    public void atualizarTexto(string valor){
        
        texto.text = valor;
        //Debug.Log(texto.text);

    }
}
