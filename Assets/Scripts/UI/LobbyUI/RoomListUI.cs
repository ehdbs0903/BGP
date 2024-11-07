using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Collections.Generic;

public class RoomListUI : MonoBehaviourPunCallbacks
{
    // �� ��� ��ũ�� ���� ������ �г�
    public Transform roomListContent;
    // �������� ������ �� �׸� ������
    public GameObject roomItemPrefab;

    // �� ��� ����
    private Dictionary<string, GameObject> roomListItems = new Dictionary<string, GameObject>();

    // Photon �ݹ�: �� ����� ������Ʈ�� �� ȣ���
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo room in roomList)
        {
            Debug.Log("���� ������ ���");
            // ���� ������ ���
            if (room.RemovedFromList)
            {
                if (roomListItems.ContainsKey(room.Name))
                {
                    Destroy(roomListItems[room.Name]);
                    roomListItems.Remove(room.Name);
                }
            }
            else
            {
                Debug.Log("���ο� ���̰ų� ������Ʈ�� ��");
                // ���ο� ���̰ų� ������Ʈ�� ��
                if (!roomListItems.ContainsKey(room.Name))
                {
                    GameObject newRoomItem = Instantiate(roomItemPrefab, roomListContent);
                    roomListItems[room.Name] = newRoomItem;

                    // �� ���� ������Ʈ
                    newRoomItem.transform.Find("RoomName").GetComponent<TMP_Text>().text = room.Name;
                    newRoomItem.transform.Find("GameType").GetComponent<TMP_Text>().text = room.CustomProperties["GameType"].ToString();
                    newRoomItem.transform.Find("PlayerCount").GetComponent<TMP_Text>().text = room.PlayerCount + "/" + room.MaxPlayers;

                    // Ŭ�� �� �濡 �����ϴ� �̺�Ʈ �߰�
                    newRoomItem.GetComponent<Button>().onClick.AddListener(() => JoinRoom(room.Name));
                }
                else
                {
                    // ���� ���� ���� ������Ʈ
                    Debug.Log("���� ���� ���� ������Ʈ");
                    GameObject existingRoomItem = roomListItems[room.Name];
                    existingRoomItem.transform.Find("PlayerCount").GetComponent<TMP_Text>().text = room.PlayerCount + "/" + room.MaxPlayers;
                }
            }
        }
    }

    // �濡 �����ϴ� �Լ�
    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("�濡 �����߽��ϴ�.");
        SceneManager.LoadScene("RoomScene");
    }
}
