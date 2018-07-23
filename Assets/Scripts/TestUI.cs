using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TestUI : MonoBehaviour
{
    private RoundController roundController;

    private void Awake() {
        roundController = FindObjectOfType<RoundController>();
    }

    private void OnGUI() {
        GUI.Label(new Rect(10, 10, 75, 20), $"Turno: {roundController.actingSide.ToString()}");

        if (GUI.Button(new Rect(10, 40, 75, 50), "Restart")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}