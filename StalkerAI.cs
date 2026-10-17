using UnityEngine;
using UnityEngine.AI;
namespace NightGrove {
    [RequireComponent(typeof(NavMeshAgent))]
    public class StalkerAI : MonoBehaviour {
        public Transform target; public float sightRange = 18, attackRange = 1.4f, damage = 35; public LayerMask sightMask = ~0; public AudioSource audioSource; public AudioClip alertClip;
        NavMeshAgent agent; float nextAttack; Vector3 home; enum State { Roam, Hunt, Search } State state;
        void Awake() { agent = GetComponent<NavMeshAgent>(); home = transform.position; }
        void Update() { if (!target) { var p = FindObjectOfType<ArmSwingLocomotion>(); if (p) target = p.transform; } if (!target) return; bool sees = CanSeePlayer(); if (sees) { state = State.Hunt; agent.SetDestination(target.position); FindObjectOfType<PlayerVitals>()?.AddFear(7 * Time.deltaTime); if (Vector3.Distance(transform.position,target.position) <= attackRange && Time.time > nextAttack) { nextAttack = Time.time + 1.2f; target.GetComponent<PlayerVitals>()?.Damage(damage); } } else if (state == State.Hunt && agent.remainingDistance < 1) { state = State.Search; agent.SetDestination(home); } else if (state == State.Roam && agent.remainingDistance < 1) { agent.SetDestination(home + Random.insideUnitSphere * 8); } }
        bool CanSeePlayer() { Vector3 eye = transform.position + Vector3.up * 1.5f; Vector3 dir = target.position + Vector3.up - eye; if (dir.magnitude > sightRange) return false; if (Physics.Raycast(eye, dir.normalized, out RaycastHit hit, sightRange, sightMask)) return hit.transform == target || hit.transform.IsChildOf(target); return false; }
    }
}
