using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public GameObject itemEffect;
    public AudioClip audioClip;

    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void ItemEffect()
    {
        if(itemEffect!=null)
        {
            if (audioClip != null)
            {
                audioSource.PlayOneShot(audioClip);
            }
            
            itemEffect = Instantiate(itemEffect, transform.position, Quaternion.identity);
            Destroy(itemEffect, .5f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ItemEffect();
            Destroy(gameObject,.4f);
        }
    }
   
}
