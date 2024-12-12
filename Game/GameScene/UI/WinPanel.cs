using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : BasePanel<WinPanel>
{
    //�����ؼ�
    public CustomGUIInput inputInfo;
    public CustomGUIButton btnConfirm;

    private void Start()
    {
        btnConfirm.clickEvent += () =>
        {
            //ȡ����ͣ
            Time.timeScale = 1.0f;

            //�����ݼ�¼�����а�
            GameDataMgr.Instance.AddRankInfo(inputInfo.content.text, GamePanel.Instance.nowScore,
                GamePanel.Instance.nowTime);
            //�ص���ʼ����
            SceneManager.LoadScene("BeginScene");

        };

        HideMe();
    }
}
