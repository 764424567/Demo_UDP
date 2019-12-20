using Microsoft.Win32;
using UnityEngine;

public class SetUserTime1 : MonoBehaviour
{
    //最大使用次数
    int MaxUsageCount = 3;

    void Start()
    {
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
            if (newtime < 0)
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