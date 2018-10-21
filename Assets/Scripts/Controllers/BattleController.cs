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

    public RangeManager rangeManager;
    public SelectionManager selectionManager;

    private void Start() {
        ChangeState<InitBattleState>();
    }
}