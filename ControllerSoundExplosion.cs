using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSoundExplosion : MonoBehaviour
{
    private AudioSource explosion;
    // Start is called before the first frame update
    void Start()
    {
        explosion = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SoundExplosion() {
        explosion.Play();
    }
}
