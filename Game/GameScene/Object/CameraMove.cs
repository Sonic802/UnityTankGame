using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //摄像机看向的目标
    public Transform targetPlayer;
    public float h =10;

    private Vector3 pos;

    //摄像机更新往往写在 LateUpdate
    void LateUpdate()
    {
        if (targetPlayer == null)
        {
            return;
        }

        pos.x = targetPlayer.position.x;
        pos.z = targetPlayer.position.z;
        //外部调整摄像机高度
        pos.y = h;
        this.transform.position = pos;
    }
}
