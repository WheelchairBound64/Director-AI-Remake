using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject spawn;
    GameObject player;
    public int enemyCount;
    private int counter;
    private int num;
    public float startDelay;
    public float delayRespawn;
    public float spawnSpeed;
    // Start is called before the first frame update
    void Start()
    {
        spawn.GetComponent<MeshRenderer>().enabled = false;
        counter = enemyCount;
        num = 1;
        //StartCoroutine(SpawnSteve(enemy, spawn));
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && num == 1)
        {
            num -= 1;
            StartSpawner();
        }
    }

    public void StartSpawner()
    {
        StartCoroutine(SpawnSteve(enemy, spawn));
    }

    IEnumerator SpawnSteve(GameObject e, GameObject s)
    {
        yield return new WaitForSeconds(startDelay);
        while (counter != 0)
        {
            Instantiate(e, s.transform);
            yield return new WaitForSeconds(spawnSpeed);
            counter--;
        }
        StopCoroutine(SpawnSteve(enemy, spawn));
        counter = enemyCount;
        StartCoroutine(RestartSpawner());
    }

    IEnumerator RestartSpawner()
    {
        yield return new WaitForSeconds(delayRespawn);
        num++;
        StopCoroutine(RestartSpawner());
    }
}
