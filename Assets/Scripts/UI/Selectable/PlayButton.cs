using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : Selectable3D
{
    protected override void OnMouseDown() {
        base.OnMouseDown();
        GameManager.Instance.UpdateGameState(GameManager.GameState.Cutscene);
    }
}
