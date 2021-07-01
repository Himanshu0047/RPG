using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Saving;

namespace RPG.Movements
{
    public class Mover : MonoBehaviour, ISaveable
    {
        NavMeshAgent agent;
        Animator animator;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            UpdateAnimator();
        }

        public void MoveTo(Vector3 destination)
        {
            agent.isStopped = false;
            agent.destination = destination;
        }

        public void Stop()
        {
            agent.isStopped = true;
        }

        void UpdateAnimator()
        {
            // Convert from global to local velocity because animator parameters are for local character and
            // using global coordinates may not play the animations. This animator only plays animations for local
            // positive forward and local positive forward may be global negative backward.
            Vector3 velocity = agent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);

            animator.SetFloat("ForwardSpeed", localVelocity.z);
        }

        public object CaptureState()
        {
            // Can also use Struct
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["position"] = new SerializableVector3(transform.position);
            data["rotation"] = new SerializableVector3(transform.eulerAngles);
            return data;
        }

        public void RestoreState(object state)
        {
            Dictionary<string, object> data = (Dictionary<string, object>)state;
            GetComponent<NavMeshAgent>().Warp(((SerializableVector3)data["position"]).ToVector());
            transform.eulerAngles = ((SerializableVector3)data["rotation"]).ToVector();
        }
    }
}
