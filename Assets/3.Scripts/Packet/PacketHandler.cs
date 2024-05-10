using UnityEngine;
using Google.Protobuf;
using ServerCore;
using Protocol;
using System;

class PacketHandler
{
    public static void S_ChatHandler(PacketSession session, IMessage packet)
    {
        S_CHAT chatPacket = packet as S_CHAT;
        ServerSession serverSession = session as ServerSession;

        Debug.Log(chatPacket.ToString());
    }

    public static void S_EnterGameHandler(PacketSession session, IMessage packet)
    {
        S_ENTER_GAME enterGamePacket = packet as S_ENTER_GAME;
        ServerSession serverSession = session as ServerSession;
    }

    public static void S_LeaveGameHandler(PacketSession session, IMessage packet)
    {
        S_LEAVE_GAME enterGamePacket = packet as S_LEAVE_GAME;
        ServerSession serverSession = session as ServerSession;
    }
}