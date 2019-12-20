using System;
using UnityEngine;

public class SetUserTime : MonoBehaviour
{
    //用户是否超过使用日期
    bool Islate = false;

    // Use this for initialization
    void Start()
    {
        //===（比如8月1日开始计算，到8月8日结束）
        //小于minTime 时间或者大于maxTime时间 ，将不可使用
        DateTime minTime = Convert.ToDateTime("2019-8-1 15:29:00");
        DateTime maxTime = Convert.ToDateTime("2019-8-8 15:29:00");
        if (minTime > DateTime.Now || DateTime.Now > maxTime)
        {
            //不在使用时间内，会直接退出程序
            Islate = true;
        }
        SetPlayUseNumber();
    }

    /// <summary>
    /// 设置用户使用次数
    /// </summary>
    void SetPlayUseNumber()
    {
        //异常捕捉，如果发生异常，比如闪退，限制改为false
        try
        {
            //限制使用时间，如果不在这个区间内,直接退出程序
            if (Islate)
            {
                Invoke("OnExit", 2);//延时退出，可在退出前显示提示消息
            }
        }
        catch
        {
            Islate = false;
        }
    }

    //出处程序
    private void OnExit()
    {
        Application.Quit();
    }
}