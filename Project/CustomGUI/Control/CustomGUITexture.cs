using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGUITexture : CustomGUIControl
{
    //ͼƬ����ģʽ
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
