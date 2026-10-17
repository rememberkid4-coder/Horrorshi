using UnityEngine;
using UnityEngine.SceneManagement;

namespace NightGrove {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance { get; private set; }
        [SerializeField] int requiredRelics = 3;
        public int RelicsCollected { get; private set; }
        public bool IsGameOver { get; private set; }
        public bool CanExtract => RelicsCollected >= requiredRelics;
        public string ObjectiveText => CanExtract ? "Reach the extraction gate" : $"Find relics: {RelicsCollected}/{requiredRelics}";
        public int RequiredRelics => requiredRelics;
        void Awake() { if (Instance != null && Instance != this) { Destroy(gameObject); return; } Instance = this; DontDestroyOnLoad(gameObject); }
        public void CollectRelic() { if (IsGameOver) return; RelicsCollected++; UIHud.Refresh(); }
        public void PlayerCaught() { if (IsGameOver) return; IsGameOver = true; UIHud.ShowMessage("THE GROVE REMEMBERS YOU", 4f); }
        public void Escape() { if (!CanExtract || IsGameOver) return; IsGameOver = true; UIHud.ShowMessage("YOU ESCAPED... FOR NOW", 6f); }
        public void Restart() { IsGameOver = false; RelicsCollected = 0; SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
    }
}
