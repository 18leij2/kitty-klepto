using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : Selectable3D
{
    protected override void OnMouseDown()
    {
        base.OnMouseDown();
        Application.Quit();
    }
}
