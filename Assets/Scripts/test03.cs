using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class test03 : MonoBehaviour
{
    [DllImport("user32.dll", EntryPoint = "keybd_event")]
    public static extern void Keybd_event(
        byte bvk,//虚拟键值 ESC键对应的是27
        byte bScan,//0
        int dwFlags,//0为按下，1按住，2释放
        int dwExtraInfo//0
    );


    public Button[] m_Button;

    private void Awake()
    {
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

    public void Bind_Event(int _Num)
    {
        Debug.Log(_Num);
        switch (_Num)
        {
            case 0:
                Keybd_event(49, 0, 0, 0);
                break;
            case 1:
                Keybd_event(50, 0, 0, 0);
                break;
            case 2:
                Keybd_event(51, 0, 0, 0);
                break;
            case 3:
                Keybd_event(52, 0, 0, 0);
                break;
            case 4:
                Keybd_event(53, 0, 0, 0);
                break;
            default:
                break;
        }
    }
}
