﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BattleState : State
{
    public LevelData levelData;
    protected BattleController owner;
    public Board board { get { return owner.board; } }
    public CameraRig cameraRig { get { return owner.cameraRig; } }

    //public LevelData levelData { get { return owner.levelData; } }
    public Point pos { get { return owner.pos; } set { owner.pos = value; } }

    public AbilityMenuPanelController abilityMenuPanelController { get { return owner.abilityMenuPanelController; } }
    public Turn turn { get { return owner.turn; } }
    public List<Unit> units { get { return owner.units; } }
    public Transform tileSelectionIndicator { get { return owner.tileSelectionIndicator; } }

    protected virtual void Awake() {
        owner = GetComponent<BattleController>();
    }

    protected override void AddListeners() {
        InputController.moveEvent += OnMove;
        InputController.fireEvent += OnFire;
    }

    protected override void RemoveListeners() {
        InputController.moveEvent -= OnMove;
        InputController.fireEvent -= OnFire;
    }

    protected virtual void OnMove(object sender, InfoEventArgs<Point> e) {
    }

    protected virtual void OnFire(object sender, InfoEventArgs<int> e) {
    }

    protected virtual void SelectTile(Point p) {
        if (pos == p || !board.tiles.ContainsKey(p))
            return;

        pos = p;
        tileSelectionIndicator.localPosition = board.tiles[p].center;
    }
}