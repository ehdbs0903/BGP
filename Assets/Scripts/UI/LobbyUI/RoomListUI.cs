using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Collections.Generic;

public class RoomListUI : MonoBehaviourPunCallbacks
{
    // 방 목록 스크롤 뷰의 콘텐츠 패널
    public Transform roomListContent;
    // 동적으로 생성될 방 항목 프리팹
    public GameObject roomItemPrefab;

    // 방 목록 저장
    private Dictionary<string, GameObject> roomListItems = new Dictionary<string, GameObject>();

    // Photon 콜백: 방 목록이 업데이트될 때 호출됨
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo room in roomList)
        {
            Debug.Log("방이 삭제된 경우");
            // 방이 삭제된 경우
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
                Debug.Log("새로운 방이거나 업데이트된 방");
                // 새로운 방이거나 업데이트된 방
                if (!roomListItems.ContainsKey(room.Name))
                {
                    GameObject newRoomItem = Instantiate(roomItemPrefab, roomListContent);
                    roomListItems[room.Name] = newRoomItem;

                    // 방 정보 업데이트
                    newRoomItem.transform.Find("RoomName").GetComponent<TMP_Text>().text = room.Name;
                    newRoomItem.transform.Find("GameType").GetComponent<TMP_Text>().text = room.CustomProperties["GameType"].ToString();
                    newRoomItem.transform.Find("PlayerCount").GetComponent<TMP_Text>().text = room.PlayerCount + "/" + room.MaxPlayers;

                    // 클릭 시 방에 입장하는 이벤트 추가
                    newRoomItem.GetComponent<Button>().onClick.AddListener(() => JoinRoom(room.Name));
                }
                else
                {
                    // 기존 방의 정보 업데이트
                    Debug.Log("기존 방의 정보 업데이트");
                    GameObject existingRoomItem = roomListItems[room.Name];
                    existingRoomItem.transform.Find("PlayerCount").GetComponent<TMP_Text>().text = room.PlayerCount + "/" + room.MaxPlayers;
                }
            }
        }
    }

    // 방에 입장하는 함수
    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("방에 입장했습니다.");
        SceneManager.LoadScene("RoomScene");
    }
}
