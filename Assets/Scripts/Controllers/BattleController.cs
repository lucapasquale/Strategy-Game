using System.Collections.Generic;
using UnityEngine;

public class BattleController : StateMachine
{
    public Board board;
    public CameraRig cameraRig;
    public LevelData levelData;
    public GameObject heroPrefab;
    public GameObject enemyPrefab;

    public List<Unit> units = new List<Unit>();

    public RoundController roundController;
    public RangeController rangeController;
    public AreaHighlightManager areaHighlightManager;

    private void Start() {
        ChangeState<InitBattleState>();
    }
}