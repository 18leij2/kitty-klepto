using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : Selectable3D
{
    protected override void OnMouseDown() {
        base.OnMouseDown();
        AudioManager.Instance.PlaySoundEffect(1);
        GameManager.Instance.UpdateGameState(GameManager.GameState.Cutscene);
    }
}
