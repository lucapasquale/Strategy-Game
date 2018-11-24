using UnityEngine;
using UnityEngine.UI;

public class FeaturePanel : MonoBehaviour
{
    public Feature feature;

    public Text titleLabel;
    public Text descriptionLabel;


    public void Show() {
        switch (feature.GetType().Name) {
            case "StatModifierFeature":
                ShowStatFeature();
                break;
            case "ApplyStatusFeature":
                ShowStatusFeature();
                break;
        }
    }

    private void ShowStatFeature() {
        StatModifierFeature feat = feature as StatModifierFeature;

        titleLabel.text = $"+{feat.amount} {feat.type}";
        descriptionLabel.text = "";
    }

    private void ShowStatusFeature() {
        ApplyStatusFeature feat = feature as ApplyStatusFeature;

        titleLabel.text = $"Applies {feat.effect} with {feat.intensity} intensity";
        descriptionLabel.text = $"Lasts {feat.duration} turns";
    }
}