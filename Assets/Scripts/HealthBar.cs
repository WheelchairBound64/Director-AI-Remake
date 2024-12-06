using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] Stat current;
    [SerializeField] Stat max;
    [SerializeField] Image bar;
    private bool isTouchingEnemy = false;

    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = (float)current.amount / max.amount;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<EnemyAI>())
        {
            isTouchingEnemy = true;
            StartCoroutine(StartDamage());
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.GetComponent<EnemyAI>())
        {
            isTouchingEnemy = false;
            StopCoroutine(StartDamage());
        }
    }
    IEnumerator StartDamage()
    {
        yield return new WaitForSeconds(.5f);
        while(current.amount > 0 && isTouchingEnemy == true)
        {
            current.amount -= 1;
            yield return new WaitForSeconds(1f);
        }

    }
}
