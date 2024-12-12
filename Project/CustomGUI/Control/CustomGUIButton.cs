using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomGUIButton : CustomGUIControl
{
    //提供给外部 用于响应 按钮点击的事件 只要在外部给与了响应函数 那就回执行
    public event UnityAction clickEvent;

    protected override void StyleOffDraw()
    {
        if (GUI.Button(GUIPos.Pos, content))
        {
            clickEvent?.Invoke();
        }
    }

    protected override void StyleOnDraw()
    {
        if (GUI.Button(GUIPos.Pos, content, style))
        {
            clickEvent?.Invoke();
        }
    }
}
