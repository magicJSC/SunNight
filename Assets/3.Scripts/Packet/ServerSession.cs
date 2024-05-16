using Google.Protobuf;
using Protocol;
using ServerCore;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class ServerSession : PacketSession
{
    public override void OnConnected(EndPoint endPoint)
    {
        if (endPoint == null)
            return;

        Debug.Log($"OnConnected : {endPoint}");
    }

    public override void OnDisconnected(EndPoint endPoint)
    {
        Debug.Log($"DisConnected : {endPoint}");
    }

    public override void OnRecvPacket(ArraySegment<byte> buffer)
    {
        S_CHAT pkt = new S_CHAT();
        pkt.MergeFrom(buffer.Array, buffer.Offset + 4, buffer.Count - 4);

        PacketHandler.SChatHandler(this, pkt);
    }

    public override void OnSend(int numOfBytes)
    {
        Debug.Log($"SendPacket : {numOfBytes}");
    }
}