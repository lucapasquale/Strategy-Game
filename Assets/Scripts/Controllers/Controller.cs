using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    protected BattleController owner;

    protected virtual void Awake() {
        owner = GetComponentInParent<BattleController>();
    }
}
