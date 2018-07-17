using UnityEngine;
using System;
using System.Collections;

public class InputController : MonoBehaviour
{
    private Repeater _hor = new Repeater("Horizontal");
    private Repeater _ver = new Repeater("Vertical");
    private string[] _buttons = new string[] { "Jump", "Fire2" };

    public static event EventHandler<InfoEventArgs<Point>> moveEvent;

    public static event EventHandler<InfoEventArgs<int>> fireEvent;

    public static event EventHandler<InfoEventArgs<Point>> touchEvent;

    private void Update() {
        UpdateTouch();
        UpdateAxis();
        UpdateButtons();
    }

    private void UpdateTouch() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 touchPos = new Vector2(wp.x, wp.y);

            var collider = Physics2D.OverlapPoint(touchPos);
            if (collider) {
                var tile = collider.GetComponent<Tile>();
                if (tile) {
                    touchEvent?.Invoke(this, new InfoEventArgs<Point>(tile.pos));
                }
            }
        }
    }

    private void UpdateAxis() {
        int x = _hor.Update();
        int y = _ver.Update();

        if (x != 0 || y != 0) {
            moveEvent?.Invoke(this, new InfoEventArgs<Point>(new Point(x, y)));
        }
    }

    private void UpdateButtons() {
        for (int i = 0; i < _buttons.Length; ++i) {
            if (Input.GetButtonUp(_buttons[i])) {
                fireEvent?.Invoke(this, new InfoEventArgs<int>(i));
            }
        }
    }
}

internal class Repeater
{
    private const float threshold = 0.5f;
    private const float rate = 0.25f;
    private float _next;
    private bool _hold;
    private string _axis;

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