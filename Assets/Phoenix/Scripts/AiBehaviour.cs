using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiBehaviour : MonoBehaviour
{
    public NavMeshAgent Monster;

    public Transform Player;

    public LayerMask WhatisGround, WhatisPlayer;

    //Patrol
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //State
    public float sightRange;
    public bool playerInSight;

    private void Start()
    {
        Player = GameObject.Find("Player").transform;
        Monster = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //If Player is in Line of Sight
        playerInSight = Physics.CheckSphere(transform.position, sightRange, WhatisPlayer);

        //States that Monster can switch to
        if (!playerInSight) Patrol();
        if (playerInSight) ChasePlayer();
    }

    //Monster Patrol System
    private void Patrol()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) Monster.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //WalkPoint
        if (distanceToWalkPoint.magnitude < -1f) walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, WhatisGround)) walkPointSet = true;
    }

    private void ChasePlayer()
    {
        //Chases Player once Monster has seen them
        Monster.SetDestination(Player.position);
    }
}
