using System.Collections;

public class WalkMovement : Movement
{
    public override IEnumerator Traverse(Tile tile) {
        unit.Place(tile);

        yield return StartCoroutine(Walk(tile));
    }

    protected override bool ExpandSearch(Tile from, Tile to) {
        // Skip if the tile is occupied by an enemy
        if (to.content != null)
            return false;

        return base.ExpandSearch(from, to);
    }

    private IEnumerator Walk(Tile target) {
        Tweener tweener = transform.MoveTo(target.Center, 0.2f, EasingEquations.Linear);
        while (tweener != null)
            yield return null;
    }
}