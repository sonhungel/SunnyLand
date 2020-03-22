using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class DoorScript : MonoBehaviour

{
    public AudioSource audioBackground;
    public AudioClip audioClip;
    public GameManager gameManager;

  
    private AudioSource _sound;

    private float timeRemaining;
    private float timeDelay=3f;

    private bool flag = false;
    private void FixedUpdate()
    {
        if(flag==true)
        {
            timeRemaining += Time.deltaTime;
        }
        if (timeRemaining > timeDelay)
            gameManager.NextScene();
    }


    private void Awake()
    {
        _sound = GetComponent<AudioSource>();
        
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            gameManager.Victory();
            audioBackground.Pause();
            _sound.PlayOneShot(audioClip);

            flag = true;


        }
    }

   
}
