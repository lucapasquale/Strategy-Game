using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnitUI : MonoBehaviour
{
    public Text label;
    public Image healthBar;

    private float maxWidth;
    private Unit owner;
    private Health health;

    private void Awake() {
        owner = GetComponentInParent<Unit>();
        health = owner.GetComponent<Health>();
        maxWidth = healthBar.rectTransform.sizeDelta.x;

        Match();
    }

    private void OnEnable() {
        this.AddObserver(OnHealthChange, Stats.DidChangeNotification(StatTypes.HP));
        this.AddObserver(OnHealthChange, Stats.DidChangeNotification(StatTypes.MHP));
    }

    private void OnDisable() {
        this.RemoveObserver(OnHealthChange, Stats.DidChangeNotification(StatTypes.HP));
        this.RemoveObserver(OnHealthChange, Stats.DidChangeNotification(StatTypes.MHP));
    }

    private void OnHealthChange(object sender, object args) {
        Match();
    }

    private void Match() {
        var healthPercent = (float)health.HP / health.MHP;

        label.text = health.HP.ToString();
        healthBar.rectTransform.sizeDelta = new Vector2(maxWidth * healthPercent, healthBar.rectTransform.sizeDelta.y);
    }
}