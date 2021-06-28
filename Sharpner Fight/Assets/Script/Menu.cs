using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static byte player;
    public static byte difficulty;
    [SerializeField] GameObject difficultyPanel;
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject quitPanel;
    [SerializeField] AudioSource buttonSound;
    private bool isQuitPanelActivate;

   
    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            if (isQuitPanelActivate) DesableQuitPanel();
            else ActiveQuitPanel();
        }
    }
     

    public void SinglePlayerButton()
    {
        buttonSound.Play();
        player = 1;
        difficultyPanel.SetActive(true);
    }

    public void TwoPlayerButton()
    {
        menuPanel.SetActive(false);
        difficultyPanel.SetActive(false);
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

    public void ActiveQuitPanel()
    {
        isQuitPanelActivate = true;
        quitPanel.SetActive(true);
    }

    public void DesableQuitPanel()
    {
        isQuitPanelActivate = false;
        quitPanel.SetActive(false);
    }
}

