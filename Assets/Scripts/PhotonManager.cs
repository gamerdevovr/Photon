using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Collections.Generic;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private string _region;
    [SerializeField] private TMP_InputField _roomName;
    [SerializeField] private ListItem _itemPrefab;
    [SerializeField] private Transform _content;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.ConnectToRegion(_region);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Вы подключились к: " + PhotonNetwork.CloudRegion);
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Вы отключены от сервера!");
    }

    public void CreateRoomButton()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(_roomName.text, roomOptions, TypedLobby.Default);
        PhotonNetwork.LoadLevel("GameScena");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Создана комната, имя комнаты: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Не удалось создать комнату!");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            ListItem listItem = Instantiate(_itemPrefab, _content);
            if (listItem != null)
            {
                listItem.SetInfo(info);
            }
        }
    }
}
