using RPG.Attributes;
using RPG.Core;
using RPG.Saving;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] Transform player;
        [SerializeField] float maxSpeed = 6f;

        Animator animator;
        NavMeshAgent navMeshAgent;
        Health health;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        private void Start()
        {
            
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

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionSchedular>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        [Serializable]
        struct MoverSaveData
        {
            internal SerializableVector3 position;
            internal SerializableVector3 rotation;
        }

        public object CaptureState()
        {
            //1. yontem //cift slaslar//
            //Dictionary<string, object> data = new Dictionary<string, object>();
            //data["position"] = new SerializableVector3(transform.position);
            //data["rotation"] = new SerializableVector3(transform.eulerAngles); 
            //return data;

            //2.yontem ////4lu slashlar////
            ////MoverSaveData data;
            ////data.position = new SerializableVector3(transform.position);
            ////data.rotation = new SerializableVector3(transform.eulerAngles);
            ////return data;

            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            //Dictionary<string, object> data = (Dictionary<string, object>)state;
            //SerializableVector3 position = (SerializableVector3)state;

            ////MoverSaveData data = (MoverSaveData)state;
            SerializableVector3 position = (SerializableVector3)state;
            navMeshAgent.enabled = false;

            transform.position = position.ToVector();

            //transform.position = ((SerializableVector3)data["position"]).ToVector();
            //transform.eulerAngles = ((SerializableVector3)data["rotation"]).ToVector();

            ////transform.position = data.position.ToVector();
            ////transform.eulerAngles = data.rotation.ToVector();
            navMeshAgent.enabled = true;
            GetComponent<ActionSchedular>().CancelCurrentAction();//sonradan ekledim
        }
    }
}