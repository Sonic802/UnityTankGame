using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对齐方式枚举
/// </summary>
public enum E_Alignment_Type
{
    Up,Down, Left, Right,Center,
    Left_Up, Left_Down, Right_Up, Right_Down,
}

/// <summary>
/// 该类是用于表示位置 计算位置 相关信息的，不需要继承Mono
/// 自定义的类必须要加上System.Serializable才能在Inspector面板中显示！
/// </summary>
/// 

[System.Serializable]
public class CustomGUIPos 
{
    //主要处理控件位置相关内容
    //分辨率自适应的相关计算

    //该最终位置信息用于返回给外部 用于绘制控件
    private Rect rPos = new Rect(0,0,100,100);

    //屏幕对齐方式
    public E_Alignment_Type screen_Alignment_Type = E_Alignment_Type.Center;
    //控件中心对齐方式
    public E_Alignment_Type control_Center_Alignment_Type = E_Alignment_Type.Center;
    //偏移位置
    public Vector2 pos;
    //外部设置的控件宽高
    public float width = 100;
    public float height = 50;

    //用于计算的 中心点 成员变量
    private Vector2 centerPos;

    //计算控件中心点 偏移的方法
    private void CalCenterPos()
    {
        switch (control_Center_Alignment_Type)
        {
            case E_Alignment_Type.Up:
                centerPos.x = -width / 2;
                centerPos.y = 0;
                break;
            case E_Alignment_Type.Down:
                centerPos.x = -width / 2;
                centerPos.y = -height;
                break;
            case E_Alignment_Type.Left:
                centerPos.x = 0;
                centerPos.y = -height / 2;
                break;
            case E_Alignment_Type.Right:
                centerPos.x = -width;
                centerPos.y = -height / 2;
                break;
            case E_Alignment_Type.Center:
                centerPos.x = -width / 2;
                centerPos.y = -height / 2;
                break;
            case E_Alignment_Type.Left_Up:
                centerPos.x = 0;
                centerPos.y = 0;
                break;
            case E_Alignment_Type.Left_Down:
                centerPos.x = 0;
                centerPos.y = -height;
                break;
            case E_Alignment_Type.Right_Up:
                centerPos.x = -width;
                centerPos.y = 0;
                break;
            case E_Alignment_Type.Right_Down:
                centerPos.x = -width;
                centerPos.y = -height;
                break;
            default:
                break;
        }
    }


    
    //计算最终相对位置的坐标
    private void CalrealPos()
    {
        switch (screen_Alignment_Type)
        {
            case E_Alignment_Type.Up:
                rPos.x = Screen.width / 2 +centerPos.x + pos.x;
                rPos.y =0+centerPos.y+pos.y;
                break;
            case E_Alignment_Type.Down:
                rPos.x = Screen.width / 2 + centerPos.x + pos.x;
                rPos.y = Screen.height + centerPos.y + pos.y;
                break;
            case E_Alignment_Type.Left:
                rPos.x = centerPos.x + pos.x;
                rPos.y = Screen.height/2 + centerPos.y + pos.y;
                break;
            case E_Alignment_Type.Right:
                rPos.x = Screen.width  +centerPos.x + pos.x;
                rPos.y = Screen.height / 2 + centerPos.y + pos.y;
                break;
            case E_Alignment_Type.Center:
                rPos.x = Screen.width / 2 +centerPos.x + pos.x;
                rPos.y = Screen.height / 2 + centerPos.y + pos.y;
                break;
            case E_Alignment_Type.Left_Up:
                rPos.x =centerPos.x + pos.x;
                rPos.y =centerPos.y + pos.y;
                break;
            case E_Alignment_Type.Left_Down:
                rPos.x = centerPos.x + pos.x;
                rPos.y = Screen.height + centerPos.y + pos.y;
                break;
            case E_Alignment_Type.Right_Up:
                rPos.x = Screen.width  + centerPos.x + pos.x;
                rPos.y = centerPos.y + pos.y;
                break;
            case E_Alignment_Type.Right_Down:
                rPos.x = Screen.width+ centerPos.x + pos.x;
                rPos.y = Screen.height+ centerPos.y + pos.y;
                break;
            default:
                break;
        }
    }


    public Rect Pos
    {
        get
        {
            //进行计算
            //计算中心点偏移
            CalCenterPos();

            //计算最终位置
            CalrealPos();

            //宽高直接赋值,返回给外部，别人直接使用来绘制控件
            rPos.width = width; 
            rPos.height = height;

            return rPos;
        }
    }


}
