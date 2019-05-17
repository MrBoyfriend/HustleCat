using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCash : MonoBehaviour
{
    private ParticleSystem moneyBurst;
    public AudioClip cashGet;
    AudioSource playerAS;


    // Start is called before the first frame update
    void Start()
    {
        moneyBurst = gameObject.GetComponent<ParticleSystem>();
        playerAS = GameObject.Find("cb_base1").GetComponent<AudioSource>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") == true)
        {
            collision.SendMessage("PlayParticle");
            if (!playerAS.isPlaying)
            {
                playerAS.PlayOneShot(cashGet, 0.7f);
            }
            Destroy(gameObject);    
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
