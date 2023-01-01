using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesActions : MonoBehaviour
{
    public float speed = 20.0f;
    public float minDist = 1f;
    public Transform target;
    private Animator monAnim;
    bool monAtk;
    public PlayerStats playerHealth;
    public float health;
    public int enemydamage = 2;

    // Use this for initialization
    void Start()
    {
        monAnim = gameObject.GetComponent<Animator>();

        playerHealth = GameObject.Find("Player").GetComponent<PlayerStats>();
        health = playerHealth.currentHealth;

        // if no target specified, assume the player
        if (target == null)
        {
            if (GameObject.FindWithTag("Player") != null)
            {
                target = GameObject.FindWithTag("Player").GetComponent<Transform>();
            }
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;
        // face the target
        transform.LookAt(target);
        //get the distance between the chaser and the target
        float distance = Vector3.Distance(transform.position, target.position);


        //so long as the chaser is farther away than the minimum distance, move towards it at rate speed.
        if (distance > minDist)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
            monAnim.SetBool("MonAttack", false);
        }
        else
        {
            monAnim.SetBool("MonAttack", true);
        }
    }
    // Set the target of the chaser
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
//            health.TakeDamage(enemydamage);
        }
    }
}
