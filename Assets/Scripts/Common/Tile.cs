using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject content;
    public Point pos;
    public Vector3 center { get { return pos.ToVector3(); } }

    public void Load(Vector3 v) {
        Load(new Point((int)v.x, (int)v.y));
    }

    public void Load(Point p) {
        pos = p;
        Match();
    }

    private void Match() {
        transform.localPosition = new Vector3(pos.x, pos.y);
        transform.localScale = Vector3.one;
    }
}