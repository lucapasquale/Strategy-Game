using UnityEngine;
using System.Collections;

public class Status : MonoBehaviour
{
    public const string AddedNotification = "Status.AddedNotification";
    public const string RemovedNotification = "Status.RemovedNotification";


    public T Add<T>() where T : StatusEffect {
        T oldEffect = GetComponentInChildren<T>();
        if (oldEffect) {
            oldEffect.Remove();
            Destroy(oldEffect.gameObject);
        }

        T effect = gameObject.AddChildComponent<T>();
        this.PostNotification(AddedNotification, effect);
        return effect;
    }
}
