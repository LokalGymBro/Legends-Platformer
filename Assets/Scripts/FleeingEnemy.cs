using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeingEnemy : MonoBehaviour

{
    public Stats enemyStats;
    public Transform sight;
    public GameObject enemyExplosionParticles;

    private bool fleeing = false;
    private bool slipping = false;
    private GameObject player;
    private Patrulja patrolBehavior;

    [System.Serializable]
    public struct Stats
    {
        public float walkSpeed;
        public float rotateSpeed;
        public float chaseSpeed;
        public float fleeDistance; // Distance at which enemy starts fleeing
        public float explodeDist;
    }

    private void Start()
    {
        patrolBehavior = GetComponent<Patrulja>();
    }

    private void Update()
    {
        if (fleeing)
        {
            Flee();
            CheckSlipping();
            CheckExplode();
        }
        else
        {
            if (player != null && Vector3.Distance(transform.position, player.transform.position) < enemyStats.fleeDistance)
            {
                fleeing = true;
            }
            else
            {
                patrolBehavior.Move(enemyStats.walkSpeed);
            }
        }
    }

    private void Flee()
    {
        if (player != null)
        {
            Vector3 fleeDirection = transform.position - player.transform.position;
            Vector3 targetPosition = transform.position + fleeDirection.normalized * enemyStats.fleeDistance;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * enemyStats.chaseSpeed);
        }
    }

    private void CheckSlipping()
    {
        if (slipping)
        {
            transform.Translate(Vector3.back * 20 * Time.deltaTime, Space.World);
        }
    }

    private void CheckExplode()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < enemyStats.explodeDist)
        {
            StartCoroutine("Explode");
            fleeing = false; // Stop fleeing when exploding
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            fleeing = true; // Start fleeing when player enters trigger
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            fleeing = false; // Stop fleeing when player exits trigger
        }
    }

    private IEnumerator Explode()
    {
        GameObject particles = Instantiate(enemyExplosionParticles, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
        yield return new WaitForSeconds(1f);
        Destroy(transform.parent.gameObject);
    }
}

// help. cant get the enemy to chase the player when player is below max health, it just keeps fleeing :(
