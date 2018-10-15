using UnityEngine;
using UnityEngine.SceneManagement;

public class TestUI : MonoBehaviour
{
    private RoundController roundController;

    private void Awake() {
        roundController = FindObjectOfType<RoundController>();
    }

    private void OnGUI() {
        if (GUI.Button(new Rect(10, 10, 250, 100), "Restart")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        var current = roundController.Current;
        if (current == null) {
            return;
        }

        var stats = current.GetComponent<Stats>();
        GUI.Label(new Rect(0, 60, 75, 20), $"Unit: {current.gameObject.name}");
        GUI.Label(new Rect(0, 80, 75, 20), $"HP: {stats[StatTypes.HP]} / {stats[StatTypes.MHP]}");
        GUI.Label(new Rect(0, 100, 75, 20), $"ATK: {stats[StatTypes.ATK]}");
        GUI.Label(new Rect(0, 120, 75, 20), $"DEF: {stats[StatTypes.DEF]}");
    }
}