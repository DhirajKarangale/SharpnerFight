using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static byte player;
    public static byte difficulty;
    public static AudioSource bgMusic;
    [SerializeField] GameObject difficultyPanel;
    [SerializeField] GameObject menuPanel;
    [SerializeField] AudioSource buttonSound;

    private void Awake()
    {
        bgMusic = GetComponent<AudioSource>();
        bgMusic.Play();
        DontDestroyOnLoad(bgMusic);
    }

    public void SinglePlayerButton()
    {
        buttonSound.Play();
        player = 1;
        difficultyPanel.SetActive(true);
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

    public void EasyButton()
    {
        menuPanel.SetActive(false);
        difficultyPanel.SetActive(false);
        buttonSound.Play();
        difficulty = 1;
        SceneManager.LoadScene(1);
    }

    public void MediumButton()
    {
        menuPanel.SetActive(false);
        difficultyPanel.SetActive(false);
        buttonSound.Play();
        difficulty = 2;
        SceneManager.LoadScene(1);
    }

    public void HardButton()
    {
        menuPanel.SetActive(false);
        difficultyPanel.SetActive(false);
        buttonSound.Play();
        difficulty = 3;
        SceneManager.LoadScene(1);
    }

    public void BackDifficultyButton()
    {
        buttonSound.Play();
        difficultyPanel.SetActive(false);
    }
}

