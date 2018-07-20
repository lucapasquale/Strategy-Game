using UnityEngine;

public abstract class BattleState : State
{
    // TEMP
    public LevelData levelData;

    protected BattleController owner;

    //public LevelData levelData { get { return owner.levelData; } }
    public Board board { get { return owner.board; } }

    public CameraRig cameraRig { get { return owner.cameraRig; } }
    public Point pos { get { return owner.pos; } set { owner.pos = value; } }
    public RoundController roundController { get { return owner.roundController; } }
    public Transform tileSelectionIndicator { get { return owner.tileSelectionIndicator; } }

    protected virtual void Awake() {
        owner = GetComponent<BattleController>();
    }

    protected override void AddListeners() {
        InputController.touchEvent += OnTouch;
    }

    protected override void RemoveListeners() {
        InputController.touchEvent -= OnTouch;
    }

    protected virtual void OnTouch(object sender, InfoEventArgs<Point> e) {
    }

    protected virtual void SelectTile(Point p) {
        if (pos == p || !board.tiles.ContainsKey(p))
            return;

        pos = p;
        tileSelectionIndicator.localPosition = board.tiles[p].center;
    }

    protected virtual void ClearSelection() {
        tileSelectionIndicator.localPosition = new Vector3(-10, -10, 0);
    }
}