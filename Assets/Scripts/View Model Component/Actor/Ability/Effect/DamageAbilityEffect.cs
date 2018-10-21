using UnityEngine;

public class DamageAbilityEffect : AbilityEffect
{
    protected const int minDamage = 1;
    protected const int maxDamage = 999;


    public override int Predict(Tile target) {
        Unit attacker = GetComponentInParent<Unit>();
        Unit defender = target.content.GetComponent<Unit>();

        // Get the attackers base attack stat considering
        // mission items, support check, status check, and equipment, etc
        int attack = attacker.GetComponentInParent<Stats>()[StatTypes.ATK];
        attack = GetStat(attacker, defender, GetAttackNotification, attack);

        // Get the targets base defense stat considering
        // mission items, support check, status check, and equipment, etc
        int defense = defender.GetComponentInParent<Stats>()[StatTypes.DEF];
        defense = GetStat(attacker, defender, GetDefenseNotification, defense);

        // Calculate base damage
        int damage = attack - (defense / 2);

        // Tweak the damage based on a variety of other checks like
        // Elemental damage, Critical Hits, Damage multipliers, etc.
        damage = GetStat(attacker, defender, TweakDamageNotification, damage);

        damage = Mathf.Clamp(damage, minDamage, maxDamage);
        return -damage;
    }

    protected override int OnApply(Tile target) {
        Unit defender = target.content.GetComponent<Unit>();
        Stats s = defender.GetComponent<Stats>();

        int value = Predict(target);
        s[StatTypes.HP] += value;

        return value;
    }
}