using System.Collections;
using System.Collections.Generic;
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
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Hit door");
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            if (player.speed <= 2)
            {
                Destroy(this.gameObject);
            }
        }
    }
} 
