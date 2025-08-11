using System;
using System.Collections.Generic;
using Events;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class WeepingAngelAI : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float stopDistance = 2f;
        [SerializeField] private float activeDistance = 25f;
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private List<AnimationClip> idlePoses;
        [SerializeField] private Animation monsterAnimation;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private AudioSource audioSource;

        private bool isVisibleToPlayer = false;
        private float nextPoseChangeTime = 0f;
        private float poseInterval = 0.5f;

        private void OnValidate()
        {
            if (agent == null)
            {
                agent = GetComponent<NavMeshAgent>();
                agent.speed = moveSpeed;
            }

            if (target == null)
                target = Camera.main.transform;

            if (monsterAnimation == null)
                monsterAnimation = GetComponent<Animation>();

            if (audioSource == null)
                audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance < stopDistance)
            {
                EventManager.currentManager.AddEvent(new PlayerCaughtEvent());
                enabled = false;
                return;
            }

            Vector3 directionToPlayer = (target.position - transform.position).normalized;
            float dot = Vector3.Dot(target.forward, directionToPlayer);
            isVisibleToPlayer = dot < 0;

            if (!isVisibleToPlayer)
                TryChangePose();

            if (distance > activeDistance || !isVisibleToPlayer)
            {
                agent.SetDestination(target.position);
                if (!audioSource.isPlaying)
                    audioSource.Play();
            }
            else
            {
                agent.ResetPath();
                audioSource.Stop();
            }
        }

        private void TryChangePose()
        {
            if (Time.time > nextPoseChangeTime && idlePoses.Count > 0)
            {
                int randIndex = Random.Range(0, idlePoses.Count);
                monsterAnimation.clip = idlePoses[randIndex];
                monsterAnimation.Play();
                nextPoseChangeTime = Time.time + poseInterval;
            }
        }
    }
}
