using System.Collections;

public class WalkMovement : Movement
{
    public override IEnumerator Traverse(Tile tile, float duration) {
        unit.Place(tile);

        yield return StartCoroutine(Walk(tile, duration));
    }

    protected override bool ExpandSearch(Tile from, Tile to) {
        // Skip if the tile is occupied by an enemy
        if (to.content != null)
            return false;

        return base.ExpandSearch(from, to);
    }

    private IEnumerator Walk(Tile target, float duration) {
        Tweener tweener = unit.transform.MoveTo(target.Center, duration, EasingEquations.Linear);
        while (tweener) {
            yield return null;
        }
    }
}