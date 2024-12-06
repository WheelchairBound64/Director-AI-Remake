using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Key : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] GameObject playerKey;
    [SerializeField] int dist;
    Player myplayer = null;
    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space") && (myplayer != null))
        {

            playerKey.SetActive(false);
            myplayer.WalkingKey(false);
            // myplayer.SetSpeed(3);
            myplayer.DropKey(false);
            //myplayer.ShootGun(true);


        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Hit something");
        // Slow down speedS
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            myplayer = player;
            player.speed = player.maxSpeed / 2;
            player.normalSpeed = player.speed;
            player.WalkingKey(true);
            gameObject.SetActive(false) ;

            Debug.Log(player.speed);
        }
    }
    private void Dropkey()
    {
        Vector3 NewKeypos = myplayer.transform.position + (myplayer.transform.forward * dist);
        this.transform.position = NewKeypos;
        gameObject.SetActive (false);
    }
}
