using UnityEngine;

public class Unit : MonoBehaviour
{
    public Directions dir;
    public Alliances alliance;
    public Turn turn;
    public Tile tile { get; protected set; }

    public void Place(Tile target) {
        if (tile != null && tile.content == gameObject)
            tile.content = null;

        tile = target;
        if (target != null)
            target.content = gameObject;
    }

    public void Match() {
        transform.localPosition = tile.center;
        transform.localEulerAngles = dir.ToEuler();
    }
}