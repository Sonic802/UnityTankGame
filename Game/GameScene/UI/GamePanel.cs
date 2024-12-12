using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : BasePanel<GamePanel>
{
    //��ȡ�ؼ� ���������ϵĿؼ�����
    public CustomGUILabel labSocre;
    public CustomGUILabel labTime;
    public CustomGUIButton btnQuit;
    public CustomGUIButton btnSetting;

    public CustomGUITexture texHP;
    public float HPWidth = 300;

    //��¼��ǰ���� ϣ���ⲿʹ�� ����ϣ�����������ʾ
    [HideInInspector]
    public int nowScore = 0;

    [HideInInspector]
    public float nowTime = 0;
    private int time;


    // ���������ϵ� �ؼ������¼�
    void Start()
    {
        btnQuit.clickEvent += () =>
        {
            //������Ϸ�����棬Ҫ����ȷ����ȡ���İ�ť
            QuitPanel.Instance.ShowMe();

            //��ͣ
            Time.timeScale = 0;
        };

        btnSetting.clickEvent += () =>
        {
            //���������
            SettingPanel.Instance.ShowMe();

            //��ͣ
            Time.timeScale = 0;
        };

    }

    // Update is called once per frame
    void Update()
    {
        //ͨ��֡���ʱ���ۼӣ������Time.time��������ǽ�����Ϸ�����ڵ���ʱ��
        nowTime += Time.deltaTime;

        //��ת����ʱ���� ����Update��Ƶ��ȥ�������� ѡ�����ⲿ����
        time = (int)nowTime;
        labTime.content.text = "";
        if (time / 3600 > 0)
        {
            labTime.content.text += time / 3600 + "ʱ";
        }
        if (time % 3600 / 60 > 0 || labTime.content.text != "")
        {
            labTime.content.text += time % 3600 / 60 + "��";
        }
        labTime.content.text += time % 60 + "��";

        
    }

    /// <summary>
    /// �ṩ���ⲿ�ļӷַ���
    /// </summary>
    /// <param name="socre"></param>
    public void AddScore(int socre)
    {
        nowScore += socre;
        //�������
        labSocre.content.text =nowScore.ToString();

    }

    /// <summary>
    /// ����Ѫ���ķ���
    /// </summary>
    /// <param name="maxHP"></param>
    /// <param name="HP"></param>
    public void UpdateHP(int maxHP,int HP)
    {
        texHP.GUIPos.width = (float)HP / maxHP * HPWidth;
    }
}
