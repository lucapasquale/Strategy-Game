using UnityEngine;

public abstract class BattleState : State
{
    //public LevelData levelData { get { return owner.levelData; } }
    public LevelData levelData;

    protected BattleController owner;

    public Board Board { get { return owner.board; } }
    public CameraRig CameraRig { get { return owner.cameraRig; } }

    public RoundController RoundController { get { return owner.roundController; } }
    public PartyController PartyController {  get { return owner.partyController; } }

    public SelectionManager SelectionManager {  get { return owner.selectionManager; } }
    public AreaHighlightManager AreaHighlightManager { get { return owner.areaHighlightManager; } }
    public RangeManager RangeManager { get { return owner.rangeManager; } }


    protected virtual void Awake() {
        owner = GetComponent<BattleController>();
    }

    protected override void AddListeners() {
        InputController.TouchEvent += OnTouch;
    }

    protected override void RemoveListeners() {
        InputController.TouchEvent -= OnTouch;
    }

    protected virtual void OnTouch(object sender, InfoEventArgs<Point> e) {
    }
}