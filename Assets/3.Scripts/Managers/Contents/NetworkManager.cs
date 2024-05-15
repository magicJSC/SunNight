using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    ServerSession _session = new ServerSession();
    Connector _connector = new Connector();
    IPAddress ipAddr;

    public void Init()
    {
        string host = Dns.GetHostName();
        IPHostEntry ipHost = Dns.GetHostEntry(host);
        for (int i = 0; ipHost.AddressList.Length > i; i++)
        {
            ipAddr = ipHost.AddressList[i];
        }

        IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 7777);

        _connector.Connect(ipEndPoint,
            () => { return _session; });
    }
}
