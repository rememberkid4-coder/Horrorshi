using UnityEngine;
namespace NightGrove {
    public class PlayerVitals : MonoBehaviour {
        public float maxHealth = 100, health = 100, fear = 0; public float fearRecovery = 4;
        public bool IsDead => health <= 0;
        void Update() { fear = Mathf.MoveTowards(fear, 0, fearRecovery * Time.deltaTime); }
        public void Damage(float amount) { health -= amount; fear = Mathf.Clamp(fear + amount * .7f, 0, 100); UIHud.Refresh(); if (IsDead) GameManager.Instance?.PlayerCaught(); }
        public void AddFear(float amount) { fear = Mathf.Clamp(fear + amount, 0, 100); }
    }
}
