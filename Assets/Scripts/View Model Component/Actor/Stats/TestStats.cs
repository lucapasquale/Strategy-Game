using UnityEngine;

public class TestStats : MonoBehaviour
{
    public int HP;
    public int ATK;
    public int DEF;

    private void Start() {
        var stats = GetComponent<Stats>();

        stats[StatTypes.MHP] = HP;
        stats[StatTypes.HP] = HP;

        stats[StatTypes.ATK] = ATK;
        stats[StatTypes.DEF] = DEF;
    }
}