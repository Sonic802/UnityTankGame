using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CustomGUIRoot : MonoBehaviour
{
    //用于存储子对象 所有的GUI控件的容器
    private CustomGUIControl[] allControls;
    
    void Start()
    {
        allControls = GetComponentsInChildren<CustomGUIControl>();
    }

    //此处统一绘制子对象控件的内容
    private void OnGUI()
    {
        
        allControls = GetComponentsInChildren<CustomGUIControl>();

        //遍历每一个控件 让其执行绘制
        for (int i = 0; i < allControls.Length; i++)
        {
            allControls[i].DrawGUI();
        }
    }
}
