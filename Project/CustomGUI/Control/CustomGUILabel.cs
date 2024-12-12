using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGUILabel : CustomGUIControl
{
    protected override void StyleOffDraw()
    {
        GUI.Label(GUIPos.Pos, content);
    }

    protected override void StyleOnDraw()
    {
        GUI.Label(GUIPos.Pos, content, style);
    }
}
