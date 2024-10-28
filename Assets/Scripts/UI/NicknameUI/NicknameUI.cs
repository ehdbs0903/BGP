using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class NicknameUI : MonoBehaviour
{
    public TMP_InputField nicknameInputField; // 닉네임 입력 필드

    // 확인 버튼 클릭 시 호출되는 함수
    public void OnConfirmButtonClicked()
    {
        string nickname = nicknameInputField.text;

        if (string.IsNullOrEmpty(nickname))
        {
            Debug.LogWarning("닉네임을 입력하세요.");
            return;
        }

        if (PhotonNetwork.IsConnected)
        {
            // Photon 네트워크에 플레이어 이름 설정
            PhotonNetwork.NickName = nickname;

            PhotonNetwork.JoinLobby();

            // 로비 씬으로 이동
            Debug.Log("로비에 입장했습니다.");
            SceneManager.LoadScene("LobbyScene");
        }
        else
        {
            Debug.LogError("Photon Network is not connected!");
        }
    }

    public void OnCancelButtonClicked()
    {
        SceneManager.LoadScene("MainScene");
    }
}
