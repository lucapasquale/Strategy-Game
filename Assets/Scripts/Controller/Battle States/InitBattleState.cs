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
        for (int x = 0; x < 5; x++) {
            for (int y = 0; y < 5; y++) {
                data.Add(new Vector3(x, y));
            }
        }

        levelData = ScriptableObject.CreateInstance<LevelData>();
        levelData.tiles = data;

        board.Load(levelData);
        Point p = new Point((int)levelData.tiles[0].x, (int)levelData.tiles[0].z);
        SelectTile(p);
        SpawnTestUnits();
        yield return null;
        owner.ChangeState<SelectUnitState>();
    }

    private void SpawnTestUnits() {
        System.Type[] components = new System.Type[] { typeof(WalkMovement) };
        for (int i = 0; i < 1; ++i) {
            GameObject instance = Instantiate(owner.heroPrefab) as GameObject;
            Point p = new Point((int)levelData.tiles[i].x, (int)levelData.tiles[i].z);
            Unit unit = instance.GetComponent<Unit>();
            unit.Place(board.GetTile(p));
            unit.Match();
            Movement m = instance.AddComponent(components[i]) as Movement;
            m.range = 5;
        }
    }
}