using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // ���� ���� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void PlayGame()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    // ���� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void Option() {
        SceneManager.LoadScene("OptionScene");
    }

    // �����ϱ� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void QuitGame()
    {
        Application.Quit();  // ���� ������ ���� (����� ���ӿ����� �۵�)
    }
}
