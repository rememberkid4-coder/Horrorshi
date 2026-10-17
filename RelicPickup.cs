using UnityEngine;
namespace NightGrove { public class RelicPickup : MonoBehaviour { public float spinSpeed = 70; void Update() { transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime, Space.World); } void OnTriggerEnter(Collider other) { if (!other.GetComponentInParent<ArmSwingLocomotion>()) return; GameManager.Instance?.CollectRelic(); Destroy(gameObject); } } }
