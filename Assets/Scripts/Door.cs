using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit door");
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            if (player.speed <= 3)
            {
                Destroy(this.gameObject);
            }
        }
    }
} 
