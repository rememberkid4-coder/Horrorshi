using UnityEngine;
namespace NightGrove { public class ExtractionGate : MonoBehaviour { void OnTriggerEnter(Collider other) { if (other.GetComponentInParent<ArmSwingLocomotion>()) { if (GameManager.Instance && GameManager.Instance.CanExtract) GameManager.Instance.Escape(); else UIHud.ShowMessage(GameManager.Instance ? GameManager.Instance.ObjectiveText : "Find the relics", 2f); } } } }
