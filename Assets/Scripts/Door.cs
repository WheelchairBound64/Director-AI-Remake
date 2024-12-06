using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Hit door");
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            if (player.speed <= 2)
            {
                Destroy(this.gameObject);
                player.WalkingKey(false);
                player.speed = player.maxSpeed;
                player.normalSpeed = player.maxSpeed;
            }
        }
    }
} 
