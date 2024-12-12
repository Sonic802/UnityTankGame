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
        //�������̹��ͷ��ת��,��������ڴ�����   
        Cursor.lockState = CursorLockMode.Confined;

        //����һ�ΰ�ť�������ʲô
        btnBegin.clickEvent += () =>
        {
            //�л���Ϸ����
            SceneManager.LoadScene("GameScene");
        };

        btnSetting.clickEvent += () =>
        {
            //���������
            SettingPanel.Instance.ShowMe();
            HideMe();
        };

        btnQuit.clickEvent += () =>
        {
            //�˳���Ϸ
            Application.Quit();
        };

        btnRank.clickEvent += () =>
        {
            //�����а����
            
            RankPanel.Instance.ShowMe();
            HideMe() ;
        };
    }
}
