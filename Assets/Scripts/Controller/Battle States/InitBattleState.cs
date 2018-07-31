using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBattleState : BattleState
{
    public override void Enter() {
        base.Enter();
        StartCoroutine(Init());
    }

    private IEnumerator Init() {
        var data = new List<Vector3>();
        for (int x = 0; x < 16; x++) {
            for (int y = 0; y < 9; y++) {
                data.Add(new Vector3(x, y));
            }
        }
        levelData = ScriptableObject.CreateInstance<LevelData>();
        levelData.tiles = data;

        Board.Load(levelData);
        SpawnTestUnits();
        yield return null;
        owner.ChangeState<SelectUnitState>();
    }

    private void SpawnTestUnits() {
        for (int i = 0; i < 2; ++i) {
            GameObject instance = Instantiate(owner.heroPrefab, RoundController.transform) as GameObject;
            Point p = new Point(2, i * 3);
            Unit unit = instance.GetComponent<Unit>();
            unit.Place(Board.GetTile(p));
            unit.Match();

            unit.turn = new Turn(unit);
            RoundController.AddUnit(unit);
        }

        for (int i = 0; i < 1; ++i) {
            GameObject instance = Instantiate(owner.enemyPrefab, RoundController.transform) as GameObject;
            Point p = new Point(4, 4);
            Unit unit = instance.GetComponent<Unit>();
            unit.Place(Board.GetTile(p));
            unit.Match();

            RoundController.AddUnit(unit);
        }
    }
}