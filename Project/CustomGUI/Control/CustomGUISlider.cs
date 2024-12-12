using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum E_Slider_Type
{
    Horizontal, 
    Vertical,
}

public class CustomGUISlider : CustomGUIControl
{
    public float minValue=0;
    public float maxValue=1;
    public float nowValue=0.5f;

    //水平还是竖直
    public E_Slider_Type type = E_Slider_Type.Horizontal;
    //小按钮的style
    public GUIStyle styleThumb;

    public event UnityAction<float> OnValueChanged;

    private float preValue;

    protected override void StyleOffDraw()
    {
        switch (type)
        {
            case E_Slider_Type.Horizontal:
                nowValue = GUI.HorizontalSlider(GUIPos.Pos, nowValue, minValue, maxValue);
                break;
            case E_Slider_Type.Vertical:
                nowValue = GUI.VerticalSlider(GUIPos.Pos, nowValue, minValue, maxValue);
                break;
            
        }
        if(preValue != nowValue)
        {
            OnValueChanged?.Invoke(nowValue);
            preValue = nowValue;
        }

    }

    protected override void StyleOnDraw()
    {
        switch (type)
        {
            case E_Slider_Type.Horizontal:
                nowValue = GUI.HorizontalSlider(GUIPos.Pos, nowValue, minValue, maxValue, style, styleThumb);
                break;
            case E_Slider_Type.Vertical:
                nowValue = GUI.VerticalSlider(GUIPos.Pos, nowValue, minValue, maxValue, style, styleThumb);
                break;

        }

        if (preValue != nowValue)
        {
            OnValueChanged?.Invoke(nowValue);
            preValue = nowValue;
        }
    }
}
