using UnityEngine;
using Google.Protobuf;
using ServerCore;
using Protocol;
using System;

class PacketHandler
{
    public static void SChatHandler(PacketSession session, IMessage packet)
    {
        S_CHAT chatPacket = packet as S_CHAT;
        ServerSession serverSession = session as ServerSession;

        Debug.Log(chatPacket.Msg);
    }

    public static void SEnterGameHandler(PacketSession session, IMessage packet)
    {
        S_ENTER_GAME enterGamePacket = packet as S_ENTER_GAME;
        ServerSession serverSession = session as ServerSession;
    }

    public static void SLeaveGameHandler(PacketSession session, IMessage packet)
    {
        S_LEAVE_GAME enterGamePacket = packet as S_LEAVE_GAME;
        ServerSession serverSession = session as ServerSession;
    }
}