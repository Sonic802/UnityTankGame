using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomGUIInput : CustomGUIControl
{
    public event UnityAction<string> textChange;

    private string preString;

    protected override void StyleOffDraw()
    {
        content.text = GUI.TextField(GUIPos.Pos, content.text);
        if(preString != content.text)
        {
            textChange?.Invoke(content.text);
            preString = content.text;
        }
    }

    protected override void StyleOnDraw()
    {
        content.text = GUI.TextField(GUIPos.Pos, content.text,style);
        if (preString != content.text)
        {
            textChange?.Invoke(content.text);
            preString = content.text;
        }
    }
}
