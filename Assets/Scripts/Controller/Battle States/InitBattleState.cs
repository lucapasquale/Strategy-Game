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
        yield return null;
        owner.ChangeState<MoveTargetState>();
    }
}