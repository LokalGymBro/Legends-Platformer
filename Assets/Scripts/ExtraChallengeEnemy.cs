﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraChallengeEnemy : MonoBehaviour
{
    
    public Stats enemyStats;

    [Tooltip("The transform to which the enemy will pace back and forth to.")]
    public Transform[] patrolPoints;

    private int currentPatrolPoint = 0; // change float (1.0f) to int

    /// <summary>
    /// Contains tunable parameters to tweak the enemy's movement.
    /// </summary>
    [System.Serializable]
    public struct Stats
    {
        [Header("Enemy Settings")]

        [Tooltip("How fast the enemy moves.")]
        public float speed; //remove default value

        [Tooltip("Whether the enemy should move or not")]
        public bool move;

    }

    void Update()
    {
        
        //check if the enemy is allowed to move
        if (enemyStats.move == true)
        {
            Vector3 moveToPoint = patrolPoints[currentPatrolPoint].position;
            transform.position = Vector3.MoveTowards(transform.position, moveToPoint, enemyStats.speed * Time.deltaTime);
           
            if (Vector3.Distance(transform.position, moveToPoint) < 0.01f)
            {
                currentPatrolPoint++;
                
                if (currentPatrolPoint >= patrolPoints.Length)
                {
                    currentPatrolPoint = 0;
                }
            
            }
        }
        
    }
}
