using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Movements;

namespace RPG.Controllers
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseRange = 5f;
        [SerializeField] float suspicionTime = 3f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointWaitingTime = 3f;
        [SerializeField] float patrolSpeed = 2f;
        [SerializeField] float playerFoundSpeed = 4.5f;

        GameObject player;
        Fighter fighter;
        Health health;
        Mover mover;

        Vector3 guardPosition;
        int currentWaypointIndex = 0;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;


        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();

            guardPosition = transform.position;
        }

        private void Update()
        {
            if(health.IsDead()) { return; }

            AIBehaviour();
        }

        void AIBehaviour()
        {
            // Faster than Vector3.Distance()
            if (((transform.position - player.transform.position).sqrMagnitude <= chaseRange * chaseRange) && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0f;
                AttackBehaviour();
            }
            else if(timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        void AttackBehaviour()
        {
            GetComponent<NavMeshAgent>().speed = playerFoundSpeed;

            fighter.SetTarget(player);
        }

        void SuspicionBehaviour()
        {
            fighter.Stop();
        }

        void PatrolBehaviour()
        {
            GetComponent<NavMeshAgent>().speed = patrolSpeed;

            Vector3 nextPosition = guardPosition;
            if(patrolPath != null)
            {
                if (AtWaypoint())
                {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }

            if (timeSinceArrivedAtWaypoint >= waypointWaitingTime)
            {
                mover.MoveTo(nextPosition);
            }
        }

        bool AtWaypoint()
        {
            float distanceToWaypoint = (transform.position - GetCurrentWaypoint()).sqrMagnitude;
            return distanceToWaypoint <= waypointTolerance * waypointTolerance;
        }

        void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypointPosition(currentWaypointIndex);
        }

        // Called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, chaseRange);
        }
    }
}
