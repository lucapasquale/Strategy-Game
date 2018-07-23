using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static event EventHandler<InfoEventArgs<Point>> TouchEvent;

    private void Update() {
        UpdateTouch();
    }

    private void UpdateTouch() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 touchPos = new Vector2(wp.x, wp.y);

            var collider = Physics2D.OverlapPoint(touchPos);
            if (collider) {
                var tile = collider.GetComponent<Tile>();
                if (tile) {
                    TouchEvent?.Invoke(this, new InfoEventArgs<Point>(tile.pos));
                }
            }
        }
    }
}