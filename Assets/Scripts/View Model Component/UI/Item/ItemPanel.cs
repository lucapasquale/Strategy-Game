using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    public GameObject item;
    public Text nameLabel;

    public GameObject featurePanelPrefab;
    public Transform featuresParent;


    private void Start() {
        nameLabel.text = item.name;

        foreach (var feature in item.GetComponents<Feature>()) {
            var instance = Instantiate(featurePanelPrefab, featuresParent);
            var featurePanel = instance.GetComponent<FeaturePanel>();

            featurePanel.feature = feature;
            featurePanel.Show();
        }
    }
}