using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGUIToggleGroup : MonoBehaviour
{
    public CustomGUIToggle[] toggles;

    //记录上次为true的toggle 
    private CustomGUIToggle preTrueToggle;
    void Start()
    {
        if (toggles.Length == 0)
            return;

        //通过遍历 来为多个toggle 添加 监听事件函数
        //函数中的处理：一个为true时，另外两个变成false
        for (int i = 0; i < toggles.Length; i++)
        {
            CustomGUIToggle toggle = toggles[i];
            toggle.isPressed += (value) =>
            { 
                
                if (value==true)
                {   //另外两个变成false
                    for (int j = 0; j < toggles.Length; j++)
                    {
                        //闭包 toggle是上一个函数中声明的变量，改变了它的生命周期   
                        if (toggles[j] !=toggle)
                        {
                            toggles[j].isSel = false;
                        }
                    }
                    preTrueToggle = toggle;
                }
                //来判断 当前变成false的toggle是不是上一次为true
                //如果是，那么不允许将其改成false
                else if(toggle == preTrueToggle)
                { 
                    toggle.isSel = true;
                }
            };
        }
    }

}
