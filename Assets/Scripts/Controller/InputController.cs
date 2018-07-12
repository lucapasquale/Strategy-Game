using UnityEngine;

public class InputController : MonoBehaviour
{
    public const string MoveNotification = "InputManager.MoveNotification";

    private Repeater _hor = new Repeater("Horizontal");
    private Repeater _ver = new Repeater("Vertical");

    private void Update() {
        int x = _hor.Update();
        int y = _ver.Update();

        if (x != 0 || y != 0) {
            this.PostNotification(MoveNotification, new Point(x, y));
        }
    }
}

internal class Repeater
{
    private const float rate = 0.25f;
    private const float threshold = 0.5f;
    private string _axis;
    private bool _hold;
    private float _next;

    public Repeater(string axisName) {
        _axis = axisName;
    }

    public int Update() {
        int retValue = 0;
        int value = Mathf.RoundToInt(Input.GetAxisRaw(_axis));
        if (value != 0) {
            if (Time.time > _next) {
                retValue = value;
                _next = Time.time + (_hold ? rate : threshold);
                _hold = true;
            }
        }
        else {
            _hold = false;
            _next = 0;
        }
        return retValue;
    }
}