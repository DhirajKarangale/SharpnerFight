using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static byte player;
    [SerializeField] AudioSource buttonSound;

    public void SinglePlayerButton()
    {
        buttonSound.Play();
        player = 1;
        SceneManager.LoadScene(1);
    }

    public void TwoPlayerButton()
    {
        buttonSound.Play();
        player = 2;
        SceneManager.LoadScene(1);
    }

    public void MoreGamesButton()
    {
        buttonSound.Play();
        Application.OpenURL("https://play.google.com/store/apps/developer?id=DK_Software");
    }

    public void QuitButton()
    {
        buttonSound.Play();
        Application.Quit();
    }
}

