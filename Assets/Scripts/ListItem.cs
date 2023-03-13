using UnityEngine;
using Photon.Realtime;
using TMPro;

public class ListItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _textName;
    [SerializeField] private TMP_Text _textPlayersCount;

    public void SetInfo(RoomInfo info)
    {
        _textName.text = info.Name;
        _textPlayersCount.text = info.PlayerCount + "/" + info.MaxPlayers;
    }
}
