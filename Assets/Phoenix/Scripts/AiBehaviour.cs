using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiBehaviour : MonoBehaviour
{
    public enum Tracking { Patrol, Chase}
    public Tracking trackType = Tracking.Patrol;

    public lights lighting;

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

    //Sight of Player
    public float sightRange;

    void Start()
    {
        lighting.GetComponent<lights>();
        Player = GameObject.Find("Player").transform;
        Monster = GetComponent<NavMeshAgent>();
        Monster.SetDestination(points[i].position);
    }

    void Update()
    {
        //Set boolean to true if player in sight
        bool playerInSight = Physics.CheckSphere(transform.position, sightRange, PlayerMask);
        
        if(lighting.monstermove == true)
        {
            if (playerInSight)
            {
                trackType = Tracking.Chase;
                Monster.speed = 10;
            }
            else
            {
                trackType = Tracking.Patrol;
                Monster.speed = 5;
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
        else
        {
            Monster.speed = 0;
        }

        //Assign enum to track or chase dependant on playerInSight

    }

    //Monster Patrol System
    private void Patrol()
    {
        if (Monster.remainingDistance < range)
        {
            i++;
            if(i == points.Length)
            {
                i = 0;
            }
            Monster.SetDestination(points[i].position);
        }

    }

    private void ChasePlayer()
    {
        //Chases Player once Monster has seen them
        Monster.SetDestination(Player.position);
    }
}