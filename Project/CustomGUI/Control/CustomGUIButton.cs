using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomGUIButton : CustomGUIControl
{
    //�ṩ���ⲿ ������Ӧ ��ť������¼� ֻҪ���ⲿ��������Ӧ���� �Ǿͻ�ִ��
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
