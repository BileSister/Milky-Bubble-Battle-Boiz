﻿using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();

        // Now that we have the client's id, connect UDP
        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnMap(Packet _packet)
    {

        Vector3 _position = _packet.ReadVector3();

        GameManager.instance.SpawnMap(_position);
    }

    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.instance.SpawnPlayer(_id, _username, _position, _rotation);
    }

    public static void PlayerPosition(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        GameManager.players[_id].transform.position = _position;
    }

    public static void PlayerRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.players[_id].transform.rotation = _rotation;
    }

    public static void PlayerDisconnected(Packet _packet)
    {
        int _id = _packet.ReadInt();

        Destroy(GameManager.players[_id].gameObject);
        GameManager.players.Remove(_id);
    }

    public static void SpawnProjectile(Packet _packet)
    {
        int _projectileID = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        int _shotByPlayer = _packet.ReadInt();

        GameManager.instance.SpawnProjectile(_projectileID, _position);
    }

    public static void ProjectilePosition(Packet _packet)
    {
        int _projectileID = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        if(GameManager.projectiles.ContainsKey(_projectileID))
        {
            GameManager.projectiles[_projectileID].transform.position = _position;
        }
            
    }

    public static void PlatformPosition(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

       // GameManager.platforms[_id].transform.position = _position;
    }


}
