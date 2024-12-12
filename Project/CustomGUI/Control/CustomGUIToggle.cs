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
        //ֻ��false��true�໥�仯ʱ������Ҫ�ⲿ���ú�����û�б�Ҫһֱ���߱���ͬһ��ֵ
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
