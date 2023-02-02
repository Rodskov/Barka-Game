using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float enemyHealth;
    public float enemyAttackValue;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void DamageToEnemy(float dmg)
    {
        enemyHealth -= dmg;

        if(enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
