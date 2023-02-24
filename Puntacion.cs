using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Puntacion : MonoBehaviour
{
    public int puntuacion;
    public TMP_Text puntos;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void enemigoDerrotado() {
        puntuacion = puntuacion + 100;
        puntos.SetText(puntuacion.ToString());
    } 


}
