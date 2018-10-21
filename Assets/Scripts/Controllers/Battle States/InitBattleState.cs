using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBattleState : BattleState
{
    public const string UnitSpawnedNotification = "InitBattleState.UnitSpawnedNotification";


    public override void Enter() {
        base.Enter();
        StartCoroutine(Init());
    }

    private IEnumerator Init() {
        levelData = ScriptableObject.CreateInstance<LevelData>();

        var tilesPostitions = new List<Vector3>();
        for (int x = 0; x < 8; x++) {
            for (int y = 0; y < 12; y++) {
                tilesPostitions.Add(new Vector3(x, y));
            }
        }
        levelData.tiles = tilesPostitions;
        Board.Load(levelData);

        SpawnTestUnits();
        RoundController.StartGame();

        yield return null;
        owner.ChangeState<SelectUnitState>();
    }

    private void SpawnTestUnits() {
        for (int i = 0; i < 2; ++i) {
            GameObject instance = Instantiate(owner.heroPrefab, PartyController.transform) as GameObject;
            Point p = new Point(2, i * 3);

            Unit unit = instance.GetComponent<Unit>();
            unit.nickname = instance.name;

            unit.Place(Board.GetTile(p));
            unit.Match();

            unit.turn = new Turn(unit);
            this.PostNotification(UnitSpawnedNotification, unit);
        }

        for (int i = 0; i < 1; ++i) {
            GameObject instance = Instantiate(owner.enemyPrefab, PartyController.transform) as GameObject;
            Point p = new Point(4, 4);

            Unit unit = instance.GetComponent<Unit>();
            unit.nickname = instance.name;

            unit.Place(Board.GetTile(p));
            unit.Match();

            unit.turn = new Turn(unit);
            this.PostNotification(UnitSpawnedNotification, unit);
        }
    }
}