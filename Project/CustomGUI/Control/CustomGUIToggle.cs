using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomGUIToggle : CustomGUIControl
{
    public bool isSel;
    private bool oldSel;

    public event UnityAction<bool> isPressed;

    protected override void StyleOffDraw()
    {
        isSel = GUI.Toggle(GUIPos.Pos, isSel, content);
        //只有false和true相互变化时，才需要外部调用函数，没有必要一直告诉别人同一个值
        if(oldSel!=isSel)
        {
            isPressed?.Invoke(isSel);
            oldSel = isSel;
        }
        
    }

    protected override void StyleOnDraw()
    {
        isSel = GUI.Toggle(GUIPos.Pos, isSel, content,style);

        if (oldSel != isSel)
        {
            isPressed?.Invoke(isSel);
            oldSel = isSel;
        }
    }
}
