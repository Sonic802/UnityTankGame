using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPanel : BasePanel<RankPanel>
{
    public CustomGUIButton btnClose;

    //�ؼ����� �ô���ȥ���Ӷ��� Ȼ��õ���ű�
    //private List<CustomGUILabel> labRank = new List<CustomGUILabel>();
    public List<CustomGUILabel> labName = new List<CustomGUILabel>();
    public List<CustomGUILabel> labScore = new List<CustomGUILabel>();
    public List<CustomGUILabel> labTime = new List<CustomGUILabel>();

    private void Start()
    {
        for (int i = 1; i <=3; i++)
        {
            //labRank.Add(this.transform.Find("labRank" + i).GetComponent<CustomGUILabel>());
            labName.Add(this.transform.Find("labName" + i).GetComponent<CustomGUILabel>());
            labScore.Add(this.transform.Find("labScore" + i).GetComponent<CustomGUILabel>());
            labTime.Add(this.transform.Find("labTime" + i).GetComponent<CustomGUILabel>());
        }


        btnClose.clickEvent += () =>
        {
            BeginPanel.Instance.ShowMe();
            HideMe();
        };

        HideMe();

       
    }

    public override void ShowMe()
    {
        base.ShowMe();
        UpdatePanelInfo();
    }

    public void UpdatePanelInfo()
    {
        //�������а����� �������
        //��ȡGameDataMgr�е����а��б� ���ڴ˴�����

        List<RankInfo> list = GameDataMgr.Instance.rankData.list;

        //�����б�����������
        for (int i = 0; i < list.Count; i++)
        {
            //����
            labName[i].content.text = list[i].name;
            //����
            labScore[i].content.text = list[i].score.ToString();
            //ʱ�� ��λ��Ϊ ��
            //������ת��Ϊʱ �� ��
            //ǿת ֱ����ȥС��
            int time = (int)list[i].time;
            //���
            labTime[i].content.text = "";
            if(time/3600 > 0)
            {
                labTime[i].content.text += time / 3600 + "ʱ";
            }
            if(time%3600/60 > 0 || labTime[i].content.text!="")
            {
                labTime[i].content.text += time % 3600 / 60 + "��";
            }
            labTime[i].content.text += time % 60 + "��";
        }
    }
}
