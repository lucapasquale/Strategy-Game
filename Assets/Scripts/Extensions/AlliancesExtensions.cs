public static class AlliancesExtensions
{
    public static Alliances GetOpposing(this Alliances a) {
        if (a == Alliances.None) {
            return Alliances.None;
        }

        return a == Alliances.Ally ? Alliances.Enemy : Alliances.Ally;
    }
}