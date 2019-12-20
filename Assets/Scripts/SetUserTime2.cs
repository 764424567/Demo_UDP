using Microsoft.Win32;
using System;
using UnityEngine;

public class SetUserTime2 : MonoBehaviour
{
    //最大使用次数
    int MaxUsageCount = 3;
    //用户是否超过使用日期
    bool Islate = false;

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
        //创建键值对
        RegistryKey RootKey, RegKey;
        //项名为：HKEY_CURRENT_USER\Software
        RootKey = Registry.CurrentUser.OpenSubKey("SOFTWARE", true);
        //打开子项：HKEY_CURRENT_USER\Software\MyRegDataApp
        if ((RegKey = RootKey.OpenSubKey("TestToControlUseTime", true)) == null)
        {
            RootKey.CreateSubKey("TestToControlUseTime");               //不存在，则创建子项
            RegKey = RootKey.OpenSubKey("TestToControlUseTime", true);  //打开键值
            RegKey.SetValue("UseTime7", (object)MaxUsageCount);         //创建键值，存储最大可使用次数
            return;
        }
        //异常捕捉，如果出现程序异常，比如闪退，次数更新为开始设置的最大使用次数
        try
        {
            object usetime = RegKey.GetValue("UseTime7");        //读取键值，可使用次数
            print("还可以使用:" + usetime + "次");
            //使用次数减1
            int newtime = int.Parse(usetime.ToString()) - 1;
            if (newtime < 0 || Islate)
            {
                //到期退出程序
                RegKey.SetValue("UseTime7", (object)newtime);
                Invoke("OnExit", 2);//延时退出，可在退出前显示提示消息
            }
            else
            {
                RegKey.SetValue("UseTime7", (object)newtime);    //更新键值，可使用次数减1
            }
        }
        catch
        {
            RegKey.SetValue("UseTime7", (object)MaxUsageCount);
            Islate = false;
            print("更新使用次数");
        }
    }

    /// <summary>
    /// 退出程序
    /// </summary>
    private void OnExit()
    {
        Application.Quit();
    }
}