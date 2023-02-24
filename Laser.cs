using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script para manejar los ajustes del Láser */

public class Laser : MonoBehaviour
{   
    Rigidbody2D rb;
    private Transform _origin_shoot;
    public GameObject shoot;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        //Código con los ajustes del disparo
        int speed = 10;
        rb.velocity = new Vector2(0, speed);
    }
//Método para registrar el impacto con los enemigos
    void OnCollisionEnter2D( Collision2D collision) {
        if(collision.gameObject.tag == "Enemy") {
            Destroy(gameObject);
            FindObjectOfType<SettingsNave>().UnsetLaser();
        }
    }
//Método para desactivar el láser si sale del rango de la cámara
    void OnTriggerEnter2D( Collider2D col) {
            Destroy(gameObject); 
            FindObjectOfType<SettingsNave>().UnsetLaser();
    }


}
