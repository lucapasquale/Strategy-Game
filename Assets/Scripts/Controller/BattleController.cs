using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleController : StateMachine
{
    public Board board;
    public CameraRig cameraRig;
    public LevelData levelData;
    public Point pos;
    public Transform tileSelectionIndicator;
    public GameObject heroPrefab;
    public RoundController roundController;
    public Tile currentTile { get { return board.GetTile(pos); } }

    private void Start() {
        ChangeState<InitBattleState>();
    }
}