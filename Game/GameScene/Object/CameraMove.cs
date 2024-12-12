using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //����������Ŀ��
    public Transform targetPlayer;
    public float h =10;

    private Vector3 pos;

    //�������������д�� LateUpdate
    void LateUpdate()
    {
        if (targetPlayer == null)
        {
            return;
        }

        pos.x = targetPlayer.position.x;
        pos.z = targetPlayer.position.z;
        //�ⲿ����������߶�
        pos.y = h;
        this.transform.position = pos;
    }
}
