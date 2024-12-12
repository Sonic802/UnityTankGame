using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CustomGUIRoot : MonoBehaviour
{
    //���ڴ洢�Ӷ��� ���е�GUI�ؼ�������
    private CustomGUIControl[] allControls;
    
    void Start()
    {
        allControls = GetComponentsInChildren<CustomGUIControl>();
    }

    //�˴�ͳһ�����Ӷ���ؼ�������
    private void OnGUI()
    {
        
        allControls = GetComponentsInChildren<CustomGUIControl>();

        //����ÿһ���ؼ� ����ִ�л���
        for (int i = 0; i < allControls.Length; i++)
        {
            allControls[i].DrawGUI();
        }
    }
}
