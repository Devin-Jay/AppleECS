using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class MainMenu : MonoBehaviour
{
    public void LoadEasyLevel()
    {
        GameManager.Instance.SetDifficulty(0);
        GameManager.Instance.LoadScene("Easy");
    }

    public void LoadMediumLevel()
    {
        GameManager.Instance.SetDifficulty(1);
        GameManager.Instance.LoadScene("Medium");
    }

    public void LoadHardLevel()
    {
        GameManager.Instance.SetDifficulty(2);
        GameManager.Instance.LoadScene("Hard");
    }
}
