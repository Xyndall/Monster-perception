using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiBehaviour : MonoBehaviour
{
    public enum Tracking { Patrol, Chase}
    public Tracking trackType = Tracking.Patrol;


    //Monster
    public NavMeshAgent Monster;
    //Player
    public Transform Player;
    //Layer Recognize
    public LayerMask GroundMask, PlayerMask;

    //Patrol
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public Transform[] points;
    int current;

    public float range = 0.5f;
    int i = 0;

    //State
    public float sightRange;

    private void Start()
    {
        Player = GameObject.Find("Player").transform;
        Monster = GetComponent<NavMeshAgent>();
        Monster.SetDestination(points[i].position);
    }

    private void Update()
    {
        //Set boolean to true if player in sight
        bool playerInSight = Physics.CheckSphere(transform.position, sightRange, PlayerMask);
        
        //Assign enum to track or chase dependant on playerInSight
        if (playerInSight)
        {
            trackType = Tracking.Chase;
        }
        else
        {
            trackType = Tracking.Patrol;
        }

        //Run movement function based on enum trackType
        switch (trackType)
        {
            case Tracking.Patrol:
                Patrol();
                break;
            case Tracking.Chase:
                ChasePlayer();
                break;
        }
    }

    //Monster Patrol System
    private void Patrol()
    {
        if (Monster.remainingDistance < range)
        {
            if(i == 1)
            {
                i = 0;
                Monster.SetDestination(points[i].position);
            }
            else
            {
                i = 1;
                Monster.SetDestination(points[i].position);
            }
        }

    }

    private void ChasePlayer()
    {
        //Chases Player once Monster has seen them
        Monster.SetDestination(Player.position);
    }
}