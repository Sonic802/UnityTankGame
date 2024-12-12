using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���뷽ʽö��
/// </summary>
public enum E_Alignment_Type
{
    Up,Down, Left, Right,Center,
    Left_Up, Left_Down, Right_Up, Right_Down,
}

/// <summary>
/// ���������ڱ�ʾλ�� ����λ�� �����Ϣ�ģ�����Ҫ�̳�Mono
/// �Զ���������Ҫ����System.Serializable������Inspector�������ʾ��
/// </summary>
/// 

[System.Serializable]
public class CustomGUIPos 
{
    //��Ҫ����ؼ�λ���������
    //�ֱ�������Ӧ����ؼ���

    //������λ����Ϣ���ڷ��ظ��ⲿ ���ڻ��ƿؼ�
    private Rect rPos = new Rect(0,0,100,100);

    //��Ļ���뷽ʽ
    public E_Alignment_Type screen_Alignment_Type = E_Alignment_Type.Center;
    //�ؼ����Ķ��뷽ʽ
    public E_Alignment_Type control_Center_Alignment_Type = E_Alignment_Type.Center;
    //ƫ��λ��
    public Vector2 pos;
    //�ⲿ���õĿؼ����
    public float width = 100;
    public float height = 50;

    //���ڼ���� ���ĵ� ��Ա����
    private Vector2 centerPos;

    //����ؼ����ĵ� ƫ�Ƶķ���
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


    
    //�����������λ�õ�����
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
            //���м���
            //�������ĵ�ƫ��
            CalCenterPos();

            //��������λ��
            CalrealPos();

            //���ֱ�Ӹ�ֵ,���ظ��ⲿ������ֱ��ʹ�������ƿؼ�
            rPos.width = width; 
            rPos.height = height;

            return rPos;
        }
    }


}
