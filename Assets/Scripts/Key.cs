using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Key : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Hit something");
        // Slow down speedS
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.speed = player.speed / 2;
            player.WalkingKey(true);
        }    
        //if(player)
        //switch animation
        //slow down player
        //key onlocks door

       // if (other.getComponent<Player>() != null)
         //   (other.getComponent<Player>().SetSpeed(other.5f);
    }
}
