using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int num;
    private int playerNum;
    private Object player;
    // Start is called before the first frame update
    void Start()
    {
        num = 0;
    }

    // Update is called once per frame
    void Update()
    {
        player = FindObjectOfType(typeof(Player));
        if (player != null)
        {
            num++;
        }
    }
}
