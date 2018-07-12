using UnityEngine;
using System.Collections;

public class BattleController : StateMachine
{
    public Board board;
    public CameraRig cameraRig;
    public LevelData levelData;
    public Point pos;
    public Transform tileSelectionIndicator;

    private void Start() {
        ChangeState<InitBattleState>();
    }
}