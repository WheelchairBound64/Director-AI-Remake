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
    private int advanceSpawnerNum;
    public float advanceSpawnerTime;
    public float startDelay;
    public float delayRespawn;
    public float spawnSpeed;
    // Start is called before the first frame update
    void Start()
    {
        spawn.GetComponent<MeshRenderer>().enabled = false;
        advanceSpawnerNum = 0;
        counter = enemyCount;
        num = 1;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && num == 1)
        {
            num -= 1;
            StartSpawner();
            StartCoroutine(AdvanceSpawns());
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
        advanceSpawnerNum++;
        StartCoroutine(RestartSpawner());
    }

    IEnumerator RestartSpawner()
    {
        yield return new WaitForSeconds(delayRespawn);
        num++;
        StopCoroutine(RestartSpawner());
    }

    IEnumerator AdvanceSpawns()
    {
        while (advanceSpawnerNum == 0)
        {
            yield return new WaitForSeconds(advanceSpawnerTime);
            enemyCount *= 2;
            delayRespawn /= 2;
            spawnSpeed /= 2;
        }
    }
}
