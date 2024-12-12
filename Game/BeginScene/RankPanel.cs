using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPanel : BasePanel<RankPanel>
{
    public CustomGUIButton btnClose;

    //控件过多 用代码去找子对象 然后得到其脚本
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
        //根据排行榜数据 更新面板
        //获取GameDataMgr中的排行榜列表 用于此处更新

        List<RankInfo> list = GameDataMgr.Instance.rankData.list;

        //根据列表更新面板数据
        for (int i = 0; i < list.Count; i++)
        {
            //名字
            labName[i].content.text = list[i].name;
            //分数
            labScore[i].content.text = list[i].score.ToString();
            //时间 单位定为 秒
            //把秒数转换为时 分 秒
            //强转 直接舍去小数
            int time = (int)list[i].time;
            //清空
            labTime[i].content.text = "";
            if(time/3600 > 0)
            {
                labTime[i].content.text += time / 3600 + "时";
            }
            if(time%3600/60 > 0 || labTime[i].content.text!="")
            {
                labTime[i].content.text += time % 3600 / 60 + "分";
            }
            labTime[i].content.text += time % 60 + "秒";
        }
    }
}
