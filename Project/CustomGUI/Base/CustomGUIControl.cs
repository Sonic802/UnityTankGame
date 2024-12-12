using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_style_OnOff
{
    On,
    Off,
}


//要挂载到预设体上

public abstract class CustomGUIControl : MonoBehaviour
{
    //提取控件的共同表现
    //1.位置信息    
    public CustomGUIPos GUIPos;
    //2.显示内容
    public GUIContent content;
    //3.自定义样式
    public GUIStyle style;
    //自定义样式是否启用的开关
    public E_style_OnOff styleOnOff = E_style_OnOff.Off;

    //提供给外部 绘制GUI的方法
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
    /// 自定义样式开启时的绘制方法
    /// </summary>
    protected abstract void StyleOnDraw();

    /// <summary>
    /// 自定义样式关闭时的绘制方法
    /// </summary>
    protected abstract void StyleOffDraw();
}
