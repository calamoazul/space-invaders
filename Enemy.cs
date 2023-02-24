using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{

   private float speed_enemy = 7.5f; //Establecer velocidad base de las naves
   private float limite_izquierda = -15f; //Limite izquierdo para el desplazamiento de las naves
   private float limite_derecha = 16f; //Límite derecho para el desplazamiento de las naves
   private GameObject _rumbo; //Variable para marcar el rumbo
   private AudioSource explosion;
   public int optionColor;
   private SpriteRenderer color_enemy;
   public float tiempo_espera = 0.05f;

    // Start is called before the first frame update
    void Start()
    {  
        explosion = GetComponent<AudioSource>();
        optionColor = Random.Range(1,6);
        color_enemy = GetComponent<SpriteRenderer>();
        //Cambiar de color las naves
        switch(optionColor) {
            case 1 :
            color_enemy.color = Color.blue;
            break;
            case 2 : 
            color_enemy.color = Color.red;
            break;
            case 3 :
            color_enemy.color = Color.green;
            break;
            case 4 :
            color_enemy.color = Color.yellow;
            break;
            case 5 :
            color_enemy.color = Color.grey;
            break;
            case 6 :
            color_enemy.color = Color.white;
            break;
            }

            
         Cambio_rumbo(); 
    }

    void FixedUpdate() {
        MovementEnemies();
    }
    //Método para que la nave cambie de rumbo durante su desplazamiento
    private void Cambio_rumbo() {

        //Marcar rumbo de naves enemigas a la izquierda
        if (_rumbo == null) {
            _rumbo = new GameObject("Rumbo");
            _rumbo.transform.position = new Vector2(limite_izquierda, transform.position.y);
            return;
        }

        //Marcar cambio de rumbo a la derecha y aumento de velocidad

        if(_rumbo.transform.position.x == limite_izquierda) {
            _rumbo.transform.position = new Vector2 (limite_derecha, transform.position.y - 1);
            speed_enemy = speed_enemy + 2;
        }

        //Cambio de rumbo de nuevo a la izquierda y aumento de velocidad

        else if (_rumbo.transform.position.x == limite_derecha) {
            _rumbo.transform.position = new Vector2 (limite_izquierda, transform.position.y - 1);
            speed_enemy = speed_enemy + 2;
        }
        
    }
    //Método que establece el movimiento de las naves y su comportamiento

    void MovementEnemies() { 
       
        if(Vector2.Distance(transform.position, _rumbo.transform.position) > 0.5f) {
            Vector2 direction = _rumbo.transform.position - transform.position;
            transform.Translate(direction.normalized * speed_enemy * Time.deltaTime);
        }
            else{
            Cambio_rumbo();
        
        }
        }

    //Método que registra la colisión de los enemigos con el jugador
    void OnCollisionEnter2D( Collision2D collision) {
        if(collision.collider.tag == "Laser") {
            Destroy(gameObject); // Destrucción del enemigo
            FindObjectOfType<Puntacion>().enemigoDerrotado(); //Aumento de puntuación al derrotar enemigo
            FindObjectOfType<SettingsNave>().DestroyEnemy(); //Registro del número de enemigos derrotados
            FindObjectOfType<ControllerSoundExplosion>().SoundExplosion();
        }
    }

     void OnTriggerEnter2D(Collider2D col) //Volver al inicio si las naves llegan al fin de pantalla
    {
        
            FindObjectOfType<SettingsNave>().Volver();

}
}