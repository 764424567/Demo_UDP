using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class test01 : MonoBehaviour
{
    public Button[] m_Button;
    string strMsg = "";
    byte[] byteSendingArray;
    Socket socketClient;
    IPEndPoint iep;

    // Use this for initialization
    void Start()
    {
        //定义发送字节区
        byteSendingArray = new byte[1024];
        //定义网络地址
        iep = new IPEndPoint(IPAddress.Parse("192.168.15.201"), 7408);
        //创建socket
        socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        socketClient.Bind (new IPEndPoint (IPAddress.Any, 8410));
        m_Button[0].onClick.AddListener(delegate
        {
            Bind_Event(0);
        });
        m_Button[1].onClick.AddListener(delegate
        {
            Bind_Event(1);
        });
        m_Button[2].onClick.AddListener(delegate
        {
            Bind_Event(2);
        });
        m_Button[3].onClick.AddListener(delegate
        {
            Bind_Event(3);
        });
        m_Button[4].onClick.AddListener(delegate
        {
            Bind_Event(4);
        });
    }

    [DllImport("user32.dll", EntryPoint = "keybd_event")]
    public static extern void Keybd_event(
        byte bvk,//虚拟键值 ESC键对应的是27
        byte bScan,//0
        int dwFlags,//0为按下，1按住，2释放
        int dwExtraInfo//0
    );

    public void Bind_Event(int i)
    {
        switch (i)
        {
            case 0:
                strMsg = "55 aa 00 00 14 01 00 01 ff ff ff ff 00 00 00 01 00 00 0f a0 00 00 c3 50 00 00 c3 50 00 00 c3 50 00 00 c3 50 00 00 c3 50 00 00 c3 50 12 34 56 78 ab cd";
                //字节转换
                byteSendingArray = new byte[1024];
                byteSendingArray = Encoding.Default.GetBytes(strMsg);
                //发送
                socketClient.SendTo(byteSendingArray,byteSendingArray.Length,SocketFlags.None, iep);
                break;
            case 1:
                strMsg = "55 aa 00 00 14 01 00 01 ff ff ff ff 00 00 00 01 00 00 00 96 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 12 34 56 78 ab cd";
                //字节转换
                byteSendingArray = new byte[1024];
                byteSendingArray = Encoding.Default.GetBytes(strMsg);
                //发送
                socketClient.SendTo(byteSendingArray, byteSendingArray.Length, SocketFlags.None, iep);
                break;
            case 2:
                strMsg = "55 aa 00 00 12 01 00 02 ff ff ff ff 00 00 00 01 00 00";
                //字节转换
                byteSendingArray = new byte[1024];
                byteSendingArray = Encoding.Default.GetBytes(strMsg);
                //发送
                socketClient.SendTo(byteSendingArray, byteSendingArray.Length, SocketFlags.None, iep);
                break;
            case 3:
                Keybd_event(49, 0, 0, 0);
                break;
            case 4:
                Keybd_event(50, 0, 0, 0);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {


    }

}
