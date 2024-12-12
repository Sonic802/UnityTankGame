using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginPanel : BasePanel<BeginPanel>
{
    public CustomGUIButton btnBegin;
    public CustomGUIButton btnSetting;
    public CustomGUIButton btnQuit;
    public CustomGUIButton btnRank;

    private void Start()
    {
        //方便控制坦克头部转向,锁定鼠标在窗口内   
        Cursor.lockState = CursorLockMode.Confined;

        //监听一次按钮点击后做什么
        btnBegin.clickEvent += () =>
        {
            //切换游戏场景
            SceneManager.LoadScene("GameScene");
        };

        btnSetting.clickEvent += () =>
        {
            //打开设置面板
            SettingPanel.Instance.ShowMe();
            HideMe();
        };

        btnQuit.clickEvent += () =>
        {
            //退出游戏
            Application.Quit();
        };

        btnRank.clickEvent += () =>
        {
            //打开排行榜面板
            
            RankPanel.Instance.ShowMe();
            HideMe() ;
        };
    }
}
