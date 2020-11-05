using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winzone : MonoBehaviour
{
    // Start is called before the first frame update
    int totalPlayers;
    int curPlayersIn = 0;
    ParticleSystem particles;

    void Start()
    {
        particles = gameObject.GetComponent<ParticleSystem>();
        totalPlayers = GameObject.FindGameObjectsWithTag("Player").Length;
        Debug.Log(totalPlayers);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("INSIDE");
        if (collision.gameObject.tag == "Player")
        {
            curPlayersIn++;
            if(curPlayersIn == totalPlayers)
            {
                particles.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("OUTSIDE");
        if (collision.gameObject.tag == "Player")
        {
            curPlayersIn--;
        }
    }
}
