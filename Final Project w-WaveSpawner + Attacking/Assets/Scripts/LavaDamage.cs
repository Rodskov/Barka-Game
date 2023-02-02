using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDamage : MonoBehaviour
{
    [SerializeField] float time = 2f, damge = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerStats>())
        {
            PlayerStats currentHealth = other.GetComponent<PlayerStats>();
            StartCoroutine(TakeDamage(time, currentHealth));
        }
    }

    IEnumerator TakeDamage(float time, PlayerStats currentHealth)
    {
        currentHealth.TakeDamage(damge);
        GetComponent<BoxCollider>().isTrigger = false;
        yield return new WaitForSeconds(time);
        GetComponent<BoxCollider>().isTrigger = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
