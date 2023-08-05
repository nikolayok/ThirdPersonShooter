using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Transform _player;
    [SerializeField] private LayerMask _whatIsGround, _whatIsPlayer;

    // Patroling
    [SerializeField] private Vector3 _walkPoint;
    private bool _walkPointSet;
    [SerializeField] private float _walkPointRange;

    // Attacking
    [SerializeField] private float _timeBetweenAttacks;
    private bool _isAlreadyAttacked;

    // States
    [SerializeField] private float _sightRange, _attackRange;
    [SerializeField] private bool _playerInSightRange, _playerInAttackRange;

    [SerializeField] private GameObject _projectile;

    [SerializeField] private Transform _barrel;

    private void Awake() 
    {
        _player = GameObject.FindWithTag("Player").transform;
        _agent = GetComponent <NavMeshAgent>();
    }

    private void Update()
    {
        // Check for sight and attack range
        _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _whatIsPlayer);
        _playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, _whatIsPlayer);

        if ( !_playerInSightRange && !_playerInAttackRange)
        {
            Patroling();
        }
        if ( _playerInSightRange && !_playerInAttackRange)
        {
            ChasePlayer();
        }
        if ( _playerInSightRange && _playerInAttackRange)
        {
            AttackPlayer();
        }
    }

    private void Patroling()
    {
        if ( !_walkPointSet) 
        {
            SearchWalkPoint();
        }

        if (_walkPointSet)
        {
            _agent.SetDestination(_walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - _walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            _walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomZ = Random.Range(-_walkPointRange, _walkPointRange);
        float randomX = Random.Range(-_walkPointRange, _walkPointRange);

        _walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(_walkPoint, -transform.up, 2f, _whatIsGround))
        {
            _walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        _agent.SetDestination(_player.position);
    }

    private void AttackPlayer()
    {
        // make sure enemy doesn't move
        _agent.SetDestination(transform.position);
        transform.LookAt(_player);

        if ( !_isAlreadyAttacked)
        {
            // Attack code here
            Rigidbody rb = Instantiate(_projectile, _barrel.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce (transform.forward * 8f, ForceMode.Impulse);
            rb.AddForce (transform.up * 6f, ForceMode.Impulse);
            //

            _isAlreadyAttacked = true;
            Invoke(nameof(ResetAttack), _timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        _isAlreadyAttacked = false;
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _sightRange);
    }
}
