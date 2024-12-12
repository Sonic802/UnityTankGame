using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_style_OnOff
{
    On,
    Off,
}


//Ҫ���ص�Ԥ������

public abstract class CustomGUIControl : MonoBehaviour
{
    //��ȡ�ؼ��Ĺ�ͬ����
    //1.λ����Ϣ    
    public CustomGUIPos GUIPos;
    //2.��ʾ����
    public GUIContent content;
    //3.�Զ�����ʽ
    public GUIStyle style;
    //�Զ�����ʽ�Ƿ����õĿ���
    public E_style_OnOff styleOnOff = E_style_OnOff.Off;

    //�ṩ���ⲿ ����GUI�ķ���
    public void DrawGUI()
    {
        switch (styleOnOff)
        {
            case E_style_OnOff.On:
                StyleOnDraw();
                break;
            case E_style_OnOff.Off:
                StyleOffDraw();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// �Զ�����ʽ����ʱ�Ļ��Ʒ���
    /// </summary>
    protected abstract void StyleOnDraw();

    /// <summary>
    /// �Զ�����ʽ�ر�ʱ�Ļ��Ʒ���
    /// </summary>
    protected abstract void StyleOffDraw();
}
