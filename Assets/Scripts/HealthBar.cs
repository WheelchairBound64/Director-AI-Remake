using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] Stat current;
    [SerializeField] Stat max;
    [SerializeField] Image bar;
    [SerializeField] GameObject enemy;

    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = (float)current.amount / max.amount;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyAI>())
        
        {
            current.amount -= 1;
        }
    }
    
}
