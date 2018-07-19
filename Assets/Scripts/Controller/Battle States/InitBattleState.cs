using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InitBattleState : BattleState
{
    public override void Enter() {
        base.Enter();
        StartCoroutine(Init());
    }

    private IEnumerator Init() {
        var data = new List<Vector3>();
        for (int x = 0; x < 8; x++) {
            for (int y = 0; y < 5; y++) {
                data.Add(new Vector3(x, y));
            }
        }
        levelData = ScriptableObject.CreateInstance<LevelData>();
        levelData.tiles = data;

        board.Load(levelData);
        SpawnTestUnits();
        yield return null;
        owner.ChangeState<SelectUnitState>();
    }

    private void SpawnTestUnits() {
        for (int i = 0; i < 3; ++i) {
            GameObject instance = Instantiate(owner.heroPrefab, roundController.transform) as GameObject;
            Point p = new Point((int)levelData.tiles[i].x, (int)levelData.tiles[i].y);
            Unit unit = instance.GetComponent<Unit>();
            unit.Place(board.GetTile(p));
            unit.Match();
            Movement m = instance.AddComponent<WalkMovement>();
            m.range = 3;

            unit.turn = new Turn(unit);
            roundController.AddUnit(unit);
        }

        for (int i = 0; i < 2; ++i) {
            GameObject instance = Instantiate(owner.enemyPrefab, roundController.transform) as GameObject;
            Point p = new Point((int)levelData.tiles[i + 10].x, (int)levelData.tiles[i + 10].y);
            Unit unit = instance.GetComponent<Unit>();
            unit.Place(board.GetTile(p));
            unit.Match();
            Movement m = instance.AddComponent<WalkMovement>();
            m.range = 2;

            roundController.AddUnit(unit);
        }
    }
}