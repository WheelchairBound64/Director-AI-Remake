using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject spawn;
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 10;
        StartCoroutine(SpawnSteve(enemy, spawn));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnSteve(GameObject e, GameObject s)
    {
        while (count != 0)
        {
            Instantiate(e, s.transform);
            yield return new WaitForSeconds(4.0f);
            count--;
        }
    }
}
