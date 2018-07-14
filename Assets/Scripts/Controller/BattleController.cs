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
    public AbilityMenuPanelController abilityMenuPanelController;
    public Turn turn = new Turn();
    public List<Unit> units = new List<Unit>();
    public Tile currentTile { get { return board.GetTile(pos); } }

    private void Start() {
        ChangeState<InitBattleState>();
    }
}