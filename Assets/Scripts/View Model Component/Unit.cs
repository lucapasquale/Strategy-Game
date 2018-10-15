using UnityEngine;

public class Unit : MonoBehaviour
{
    public Alliances alliance;
    public Turn turn;
    public Tile Tile { get; protected set; }

    [SerializeField] private SpriteRenderer sprite;

    public void Place(Tile target) {
        if (Tile != null && Tile.content == gameObject)
            Tile.content = null;

        Tile = target;
        if (target != null)
            target.content = gameObject;
    }

    public void Match() {
        transform.localPosition = Tile.Center;
    }

    public void Paint(Color color) {
        sprite.color = color;
    }
}