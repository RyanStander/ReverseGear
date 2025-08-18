using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using UnityEngine.AI;
using EventType = Events.EventType;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class WeepingAngelAI : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float stopDistance = 2f;
        [SerializeField] private float activeDistance = 25f;
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float catchUpSpeedBonus = 1;
        [SerializeField] private List<AnimationClip> idlePoses;
        [SerializeField] private Animation monsterAnimation;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private AudioSource audioSource;
        private float nextPoseChangeTime = 0f;
        private float poseInterval = 0.5f;

        private Coroutine catchUpCoroutine;
        private float currentSpeed;

        private bool disableFunctionality;

        private void OnValidate()
        {
            if (agent == null)
            {
                agent = GetComponent<NavMeshAgent>();
                agent.speed = moveSpeed;
            }

            if (target == null && Camera.main != null)
                target = Camera.main.transform;

            if (monsterAnimation == null)
                monsterAnimation = GetComponent<Animation>();

            if (audioSource == null)
                audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            EventManager.currentManager.Subscribe(EventType.PlayerCaught, OnPlayerCaught);
        }

        private void OnDisable()
        {
            EventManager.currentManager.Unsubscribe(EventType.PlayerCaught, OnPlayerCaught);
        }

        private void Start()
        {
            agent.speed = moveSpeed;
        }

        private void Update()
        {
            if (disableFunctionality)
                return;

            float distance = Vector3.Distance(transform.position, target.position);

            if (distance < stopDistance)
            {
                EventManager.currentManager.AddEvent(new PlayerCaughtEvent());
                return;
            }

            if (distance > activeDistance)
            {
                catchUpCoroutine ??= StartCoroutine(CatchUp());
            }
            else
            {
                if (catchUpCoroutine != null)
                {
                    StopCoroutine(catchUpCoroutine);
                    catchUpCoroutine = null;
                    currentSpeed = moveSpeed;
                    agent.speed = currentSpeed;
                }
            }

            if (!IsVisibleToPlayer())
                TryChangePose();

            Move(distance);
        }

        private bool IsVisibleToPlayer()
        {
            Vector3 directionToPlayer = (target.position - transform.position).normalized;
            float dot = Vector3.Dot(target.forward, directionToPlayer);
            return dot < 0;
        }

        private void Move(float distance)
        {
            if (distance > activeDistance || !IsVisibleToPlayer())
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

        private IEnumerator CatchUp()
        {
            while (true)
            {
                if (currentSpeed <= activeDistance / 2)
                    currentSpeed = Mathf.Lerp(currentSpeed, currentSpeed + catchUpSpeedBonus, Time.deltaTime);
                currentSpeed = Mathf.Clamp(currentSpeed, moveSpeed, activeDistance / 2);
                agent.speed = currentSpeed;
                yield return null;
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

        private void OnPlayerCaught(EventData eventData)
        {
            if (!eventData.IsEventOfType(out PlayerCaughtEvent caughtEvent)) return;

            disableFunctionality = true;
            agent.ResetPath();
            audioSource.Stop();
            gameObject.SetActive(false);
        }
    }
}
