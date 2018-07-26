public class EnemyAbilityTarget : AbilityTarget
{
    private Alliances alliance;

    public override bool IsTarget(Tile tile) {
        if (tile == null || tile.content == null)
            return false;

        Alliances other = tile.content.GetComponentInChildren<Unit>().alliance;
        return other == alliance.GetOpposing();
    }

    private void Start() {
        alliance = GetComponentInParent<Unit>().alliance;
    }
}