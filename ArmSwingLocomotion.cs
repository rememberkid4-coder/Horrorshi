using UnityEngine;
using UnityEngine.XR;

namespace NightGrove {
    [RequireComponent(typeof(CharacterController))]
    public class ArmSwingLocomotion : MonoBehaviour {
        public Transform head; public Transform leftHand; public Transform rightHand;
        public float movementScale = 1.8f, maxSpeed = 4.5f, gravity = -18f, floorOffset = 0.05f;
        public float stamina = 1f, staminaDrain = .22f, staminaRecovery = .16f;
        CharacterController controller; Vector3 previousL, previousR; Vector3 velocity;
        void Start() { controller = GetComponent<CharacterController>(); previousL = leftHand ? leftHand.position : transform.position; previousR = rightHand ? rightHand.position : transform.position; }
        void Update() {
            Vector3 deltaL = previousL - (leftHand ? leftHand.position : previousL); Vector3 deltaR = previousR - (rightHand ? rightHand.position : previousR); previousL = leftHand ? leftHand.position : previousL; previousR = rightHand ? rightHand.position : previousR;
            Vector3 planar = Vector3.ProjectOnPlane((deltaL + deltaR) * movementScale / Mathf.Max(Time.deltaTime, .001f), Vector3.up);
            planar = Vector3.ClampMagnitude(planar, maxSpeed);
            bool sprinting = planar.magnitude > maxSpeed * .68f && stamina > 0f;
            if (sprinting) stamina = Mathf.Max(0, stamina - staminaDrain * Time.deltaTime); else stamina = Mathf.Min(1, stamina + staminaRecovery * Time.deltaTime);
            if (!sprinting && stamina <= 0.01f) planar *= .35f;
            Vector3 forward = head ? Vector3.ProjectOnPlane(head.forward, Vector3.up).normalized : transform.forward;
            Vector3 right = head ? Vector3.ProjectOnPlane(head.right, Vector3.up).normalized : transform.right;
            Vector3 move = right * planar.x + forward * planar.z;
            if (controller.isGrounded && velocity.y < 0) velocity.y = -2f; velocity.y += gravity * Time.deltaTime; move += velocity;
            controller.Move(move * Time.deltaTime); if (head) transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
