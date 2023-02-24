using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsNave : MonoBehaviour
{
    //Script para registrar los ajustes de la nave
    public float velocidadMovimiento; //Establece la velocidad de la nave en el eje X
    private Transform _origin_shoot; //Variable de origen del disparo
    public GameObject shoot; //GameObject del Prefab del disparo
    public bool is_laser; //Variable para comprobar si el láser está activo o no
    public int totalEnemies; //Variable que recoge el número total de enemigos
    // Start is called before the first frame update
    private AudioSource audio;
    public GameObject menuPausa;
    public GameObject menuVictoria;
    public GameObject gameOver;
    public RectTransform vidasNaves;
    public float size_vida_nave = 48f;
    public int vidas;


    void Awake() {
        _origin_shoot = transform.Find("Gun"); //Registro para declarar el punto de origen del disparo
        audio = GetComponent<AudioSource>();
    }
    void Start() {
    if(PlayerPrefs.HasKey("VidasPlayer")) {
        vidas = PlayerPrefs.GetInt("VidasPlayer");
    }

    else {
        vidas = 3;
    }

    switch(vidas) {
        case 1: 
        vidasNaves.sizeDelta= new Vector2(size_vida_nave - 32f, 16f);
        break;
        case 2:
        vidasNaves.sizeDelta = new Vector2(size_vida_nave - 16f, 16f);
        break;
        case 3:
        vidasNaves.sizeDelta = new Vector2(size_vida_nave, 16f);
        break;
    }

    //Declaración de que el láser está desactivo y registro del total de enemigos
       is_laser = false;
       totalEnemies = 7;
       menuPausa.SetActive(false);
       gameOver.SetActive(false);
       menuVictoria.SetActive(false);
    }
    

    // Update is called once per frame
    void Update()
    {
        float desplazamientoHorizontal = Input.GetAxis("Horizontal");
        GetComponent<Rigidbody2D>().velocity = new Vector2 (desplazamientoHorizontal * velocidadMovimiento, GetComponent<Rigidbody2D>().velocity.y);
        //Condición para disparar al pusar la tecla Espacio
        if(Input.GetKeyDown(KeyCode.Space)) {
            //Condición para disparar únicamente si el láser está desactivado
            if( is_laser == false ) {
                    Shoot();
                    audio.Play();
            }
        }
        Victoria(); //Función para comprobar si se han cumplido los requisitos para ganar
        PausarPartida();
    }
    //Método para disparar
    public void Shoot() {
        
            Instantiate(shoot, _origin_shoot.position, Quaternion.identity);
            is_laser = true; //Variable para registrar que el láser está activo
    }
//Método que registra las colisiones y sus consecuencias

    void OnCollisionEnter2D( Collision2D collision) {
        if(collision.collider.tag == "Enemy") { //Contador de vidas
            vidas = vidas - 1;
            PlayerPrefs.SetInt("VidasPlayer", vidas);
            if(vidas > 0) {
                Volver();
            }
            else {
                cargarGameOver(); //Llamar a la función de game over
            }
        }

    }

    //Método para pausar partida
    public void PausarPartida() {
            if(Input.GetKeyDown(KeyCode.Escape)) {
                menuPausa.SetActive(true);
                Time.timeScale = 0;
            }
            
        }  
    //Método para reanudar partida

    public void ReanudarPartida() {
        menuPausa.SetActive(false);
        Time.timeScale = 1;
        
   
    }

    public void MenuPrincipal() {
        FindObjectOfType<CargarEscena>().CargarMenu();
    }
    //Método que registra que el láser está desactivado
    public void  UnsetLaser() {
        is_laser = false;
    }


    //Método que controla el número de enemigos
    public void DestroyEnemy() {
        totalEnemies = totalEnemies - 1 ;
    }
    //Método para registrar la victoria

     public void Victoria() {
        if(totalEnemies == 0 ) {
            InvokeRepeating("cargarMenuVictoria", 1.5f, 0f);
        }

    }

    //Metodo que registra el game over
    public void GameOver() {
        if(vidas <= 0) {
            cargarGameOver();
        }
    }

    //Metodo para cargar el panel de victoria

    public void cargarMenuVictoria() {
        menuVictoria.SetActive(true);
    }

    //Método que carga el panel de game over
    public void cargarGameOver() {
        gameOver.SetActive(true);
        Time.timeScale = 0;
    }
    //Método que lleva a la escena principal
    public void EscenaPrincipal() {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    
    //Método que carga la escena de nuevo
    public void Volver() {
        SceneManager.LoadScene(1);
         Time.timeScale = 1;
    }
}




       

