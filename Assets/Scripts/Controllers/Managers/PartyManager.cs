using UnityEngine;
using System.Collections.Generic;

public class PartyManager : Controller
{
    private List<Unit> units = new List<Unit>();


    public List<Unit> GetUnits(Alliances side) {
        return units.FindAll(u => u.alliance == side);
    }


    private void OnEnable() {
        this.AddObserver(UnitSpawned, InitBattleState.UnitSpawnedNotification);
        this.AddObserver(UnitDied, Health.UnitDiedNotification);
    }

    private void OnDisable() {
        this.RemoveObserver(UnitSpawned, InitBattleState.UnitSpawnedNotification);
        this.RemoveObserver(UnitDied, Health.UnitDiedNotification);
    }

    private void UnitSpawned(object sender, object args) {
        Unit unit = args as Unit;
        units.Add(unit);
    }

    private void UnitDied(object sender, object args) {
        Unit unit = args as Unit;
        units.Remove(unit);
    }
}