using UnityEngine;
using System.Collections;

public class VictoryCondition : MonoBehaviour
{
    public Alliances Winner { get; protected set; }

    private BattleController bc;


    protected virtual void Awake() {
        bc = GetComponentInParent<BattleController>();
    }

    protected virtual void OnEnable() {
        this.AddObserver(CheckForGameOver, Health.UnitDiedNotification);
    }

    protected virtual void OnDisable() {
        this.RemoveObserver(CheckForGameOver, Health.UnitDiedNotification);
    }

    protected virtual void CheckForGameOver(object sender, object args) {
        Unit deadUnit = args as Unit;

        if (!PartyAlive(deadUnit.alliance)) {
            Winner = deadUnit.alliance.GetOpposing();
            print($"Winner: {Winner.ToString()}");
        }
    }

    protected virtual bool PartyAlive(Alliances type) {
        var availableUnits = bc.partyController.GetUnits(type);
        return availableUnits.Exists(u => u.isAlive);
    }
}
