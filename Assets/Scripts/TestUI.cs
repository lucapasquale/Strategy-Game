using UnityEngine;
using UnityEngine.SceneManagement;

public class TestUI : MonoBehaviour
{
    private void OnGUI() {
        if (GUI.Button(new Rect(800, 10, 220, 100), "Restart")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}