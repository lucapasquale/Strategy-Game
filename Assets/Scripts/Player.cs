using UnityEngine;

public class Player : MonoBehaviour
{
    private ActorStats stats;

    private void Start() {
        stats = GetComponent<ActorStats>();

        this.AddObserver(OnInputReceived, InputController.MoveNotification);
    }

    private void OnInputReceived(object sender, object args) {
        var input = (Point)args;

        var currentPos = transform.position;
        transform.position = new Vector3(Mathf.Floor(currentPos.x + input.x), Mathf.Floor(currentPos.y + input.y));
    }
}