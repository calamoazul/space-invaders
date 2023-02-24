using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CargarEscena : MonoBehaviour
{

    // Empezar partida
   public void CargarPartida() {
        PlayerPrefs.DeleteAll();
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    //Cargar Menú Principal
    public void CargarMenu() {
        SceneManager.LoadScene(0);
         Time.timeScale = 1;
    }
    // Cargar Pantalla de Instrucciones
    public void CargarInstrucciones() {
        SceneManager.LoadScene(2);
    }

    //Abandonar Partida
     public void AbandonarPartida() {
        Application.Quit();
    }
}
