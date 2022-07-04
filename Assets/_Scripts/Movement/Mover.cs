using RPG.Core;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] Transform player;
        Animator animator;
        NavMeshAgent navMeshAgent;
        Health health;

        private void Start()
        {
            animator = GetComponent<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }
        private void Update() 
        {
            navMeshAgent.enabled = !health.IsDead();
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            //globalden locale ceviriyor.
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float forwardSpeed = localVelocity.z;
            animator.SetFloat("forwardSpeed", forwardSpeed);
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }
        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionSchedular>().StartAction(this);
            MoveTo(destination);
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }
    }
}