using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGUIToggleGroup : MonoBehaviour
{
    public CustomGUIToggle[] toggles;

    //��¼�ϴ�Ϊtrue��toggle 
    private CustomGUIToggle preTrueToggle;
    void Start()
    {
        if (toggles.Length == 0)
            return;

        //ͨ������ ��Ϊ���toggle ��� �����¼�����
        //�����еĴ���һ��Ϊtrueʱ�������������false
        for (int i = 0; i < toggles.Length; i++)
        {
            CustomGUIToggle toggle = toggles[i];
            toggle.isPressed += (value) =>
            { 
                
                if (value==true)
                {   //�����������false
                    for (int j = 0; j < toggles.Length; j++)
                    {
                        //�հ� toggle����һ�������������ı������ı���������������   
                        if (toggles[j] !=toggle)
                        {
                            toggles[j].isSel = false;
                        }
                    }
                    preTrueToggle = toggle;
                }
                //���ж� ��ǰ���false��toggle�ǲ�����һ��Ϊtrue
                //����ǣ���ô��������ĳ�false
                else if(toggle == preTrueToggle)
                { 
                    toggle.isSel = true;
                }
            };
        }
    }

}
