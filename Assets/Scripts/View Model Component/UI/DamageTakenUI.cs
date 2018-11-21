using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageTakenUI : MonoBehaviour
{
    public GameObject prefab;


    private void OnEnable() {
        this.AddObserver(HealthChanged, Stats.DidChangeNotification(StatTypes.HP));
    }

    private void OnDisable() {
        this.RemoveObserver(HealthChanged, Stats.DidChangeNotification(StatTypes.HP));
    }

    private void HealthChanged(object sender, object args) {
        Stats s = sender as Stats;
        int? oldValue = args as int?;

        var diff = s[StatTypes.HP] - oldValue.Value;
        if (diff >= 0) {
            return;
        }

        GameObject instance = Instantiate(prefab, s.transform);
        instance.GetComponentInChildren<Text>().text = diff.ToString();

        //StartCoroutine(Show(instance));
    }

    private IEnumerator Show(GameObject instance) {
        Vector3 startPoint = instance.transform.position;
        Vector3 endPoint = startPoint + new Vector3(0.5f, 0.75f);

        Tweener tweener = instance.transform.MoveTo(endPoint, 1f, EasingEquations.EaseInBounce);
        while (tweener) {
            yield return null;
        }

        Destroy(instance);
    }
}
