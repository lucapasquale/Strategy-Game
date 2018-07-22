using UnityEngine;

public class BattleController : StateMachine
{
    public Board board;
    public CameraRig cameraRig;
    public LevelData levelData;
    public GameObject heroPrefab;
    public GameObject enemyPrefab;

    public Point pos;
    public Transform tileSelectionIndicator;
    public RoundController roundController;
    public SelectionController selectionController;

    private void Start() {
        ChangeState<InitBattleState>();
    }
}