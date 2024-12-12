using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGUITexture : CustomGUIControl
{
    //Í¼Æ¬Ëõ·ÅÄ£Ê½
    public ScaleMode scaleMode = ScaleMode.StretchToFill;

    protected override void StyleOffDraw()
    {
        GUI.DrawTexture(GUIPos.Pos, content.image,scaleMode);
    }

    protected override void StyleOnDraw()
    {
        GUI.DrawTexture(GUIPos.Pos, content.image,scaleMode);
    }
}
