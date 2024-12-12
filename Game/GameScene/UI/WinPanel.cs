using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : BasePanel<WinPanel>
{
    //关联控件
    public CustomGUIInput inputInfo;
    public CustomGUIButton btnConfirm;

    private void Start()
    {
        btnConfirm.clickEvent += () =>
        {
            //取消暂停
            Time.timeScale = 1.0f;

            //把数据记录到排行榜
            GameDataMgr.Instance.AddRankInfo(inputInfo.content.text, GamePanel.Instance.nowScore,
                GamePanel.Instance.nowTime);
            //回到开始界面
            SceneManager.LoadScene("BeginScene");

        };

        HideMe();
    }
}
