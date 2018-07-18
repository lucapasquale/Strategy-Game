using System.Collections;
using System.Collections.Generic;

public class AbilityTargetState : BattleState
{
    //private List<Tile> tiles;
    //private AbilityRange ar;

    //private int currentSelection = 0;

    //public override void Enter() {
    //    base.Enter();
    //    ar = currentTurn.ability.GetComponent<AbilityRange>();
    //    SelectTiles();
    //    SelectTile(tiles[currentSelection].pos);
    //    //statPanelController.ShowPrimary(turn.actor.gameObject);
    //    //if (ar.directionOriented)
    //    //    RefreshSecondaryStatPanel(pos);
    //}

    //public override void Exit() {
    //    base.Exit();
    //    board.DeSelectTiles(tiles);
    //    //statPanelController.HidePrimary();
    //    //statPanelController.HideSecondary();
    //}

    ////protected override void OnMove(object sender, InfoEventArgs<Point> e) {
    ////    if (ar.directionOriented) {
    ////        ChangeDirection(e.info);
    ////    }
    ////    else {
    ////        currentSelection = ++currentSelection % tiles.Count;
    ////        SelectTile(tiles[currentSelection].pos);
    ////        //RefreshSecondaryStatPanel(pos);
    ////    }
    ////}

    ////protected override void OnFire(object sender, InfoEventArgs<int> e) {
    ////    if (e.info == 0) {
    ////        turn.hasUnitActed = true;
    ////        if (turn.hasUnitMoved)
    ////            turn.lockMove = true;
    ////    }

    ////    owner.ChangeState<CommandSelectionState>();
    ////}

    //private void SelectTiles() {
    //    tiles = ar.GetTilesInRange(board);
    //    board.SelectTiles(tiles);
    //}

    //private void ChangeDirection(Point p) {
    //    Directions dir = p.GetDirection();
    //    if (currentTurn.actor.dir != dir) {
    //        board.DeSelectTiles(tiles);
    //        currentTurn.actor.dir = dir;
    //        currentTurn.actor.Match();
    //        SelectTiles();
    //    }
    //}
}