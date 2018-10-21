using System.Collections.Generic;
using UnityEngine;

public class BattleController : StateMachine
{
    public Board board;
    public CameraRig cameraRig;
    public LevelData levelData;
    public GameObject heroPrefab;
    public GameObject enemyPrefab;

    public RoundController roundController;
    public PartyController partyController;

    public AreaHighlightManager areaHighlightManager;
    public SelectionManager selectionManager;
    public RangeManager rangeManager;

    private void Start() {
        ChangeState<InitBattleState>();
    }
}