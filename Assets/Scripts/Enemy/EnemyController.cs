using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float stoppingDistance = 3;
    private float timeOfLastAttack = 0;
    private bool hasStopped = false;
    private NavMeshAgent agent = null;
    private EnemyStats stats = null;
    private Transform target;
    
    private void Start()
    {
        GetReferences();
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        agent.SetDestination(target.position);
        RotateToTarget();

        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        if(distanceToTarget <= agent.stoppingDistance)
        {
            // if(!hasStopped)
            // {
            //     hasStopped = true;
            //     timeOfLastAttack = Time.time;
            // }
            if(Time.time >= timeOfLastAttack + stats.attackSpeed)
            {
                timeOfLastAttack = Time.time;
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                AttackToTarget(targetStats);
            }
            // else
            // {
            //     if(hasStopped)
            //     {
            //         hasStopped = false;
            //     }
            // }
            
        }
    }

    private void RotateToTarget()
    {
        //transform.LookAt(target);

        Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation;
    }
    
    private void AttackToTarget(CharacterStats statsToDamage)
    {
        stats.DealDamage(statsToDamage);
    }

    private void GetReferences()
    {
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<EnemyStats>();
        target = PlayerMotor.instance;
    }
}
