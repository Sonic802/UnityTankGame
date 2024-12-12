using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : BasePanel<GamePanel>
{
    //获取控件 关联场景上的控件对象
    public CustomGUILabel labSocre;
    public CustomGUILabel labTime;
    public CustomGUIButton btnQuit;
    public CustomGUIButton btnSetting;

    public CustomGUITexture texHP;
    public float HPWidth = 300;

    //记录当前分数 希望外部使用 但不希望在面板上显示
    [HideInInspector]
    public int nowScore = 0;

    [HideInInspector]
    public float nowTime = 0;
    private int time;


    // 监听界面上的 控件操作事件
    void Start()
    {
        btnQuit.clickEvent += () =>
        {
            //返回游戏主界面，要弹出确定或取消的按钮
            QuitPanel.Instance.ShowMe();

            //暂停
            Time.timeScale = 0;
        };

        btnSetting.clickEvent += () =>
        {
            //打开设置面板
            SettingPanel.Instance.ShowMe();

            //暂停
            Time.timeScale = 0;
        };

    }

    // Update is called once per frame
    void Update()
    {
        //通过帧间隔时间累加，如果用Time.time代表的则是进入游戏到现在的总时长
        nowTime += Time.deltaTime;

        //秒转换成时分秒 不在Update中频繁去声明变量 选择在外部声明
        time = (int)nowTime;
        labTime.content.text = "";
        if (time / 3600 > 0)
        {
            labTime.content.text += time / 3600 + "时";
        }
        if (time % 3600 / 60 > 0 || labTime.content.text != "")
        {
            labTime.content.text += time % 3600 / 60 + "分";
        }
        labTime.content.text += time % 60 + "秒";

        
    }

    /// <summary>
    /// 提供给外部的加分方法
    /// </summary>
    /// <param name="socre"></param>
    public void AddScore(int socre)
    {
        nowScore += socre;
        //更新面板
        labSocre.content.text =nowScore.ToString();

    }

    /// <summary>
    /// 更新血条的方法
    /// </summary>
    /// <param name="maxHP"></param>
    /// <param name="HP"></param>
    public void UpdateHP(int maxHP,int HP)
    {
        texHP.GUIPos.width = (float)HP / maxHP * HPWidth;
    }
}
