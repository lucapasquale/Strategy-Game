using UnityEngine;
using System.Collections;

public class BattleController : StateMachine
{
    public Board board;
    public CameraRig cameraRig;
    public LevelData levelData;
    public Point pos;
    public Transform tileSelectionIndicator;
    public GameObject heroPrefab;
    public Unit currentUnit;
    public Tile currentTile { get { return board.GetTile(pos); } }

    private void Start() {
        ChangeState<InitBattleState>();
    }
}