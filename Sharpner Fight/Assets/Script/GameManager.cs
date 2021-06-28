using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioSource dyeSound;
    [SerializeField] AudioSource buttonSound;
    [SerializeField] AudioSource bgMusic;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject gameScreen;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] Text winPlayerText;
    public static bool isGameOver;
    private bool isPause;

    private void Start()
    {
       if(bgMusic != null) bgMusic.Play();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            if (isPause) ResumeButton();
            else PauseButton();
        }

        if ((bgMusic != null) && isGameOver) bgMusic.Stop();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Sharpner") && !isGameOver)
        {
            dyeSound.Play();
            gameScreen.SetActive(false);
            Invoke("SetWinScreenActive", 1);
            isGameOver = true;
            collision.gameObject.transform.localScale = collision.gameObject.transform.localScale / 2;
            if (collision.gameObject.transform.name == "Red")
            {
                if (Menu.player == 1)
                {
                    winPlayerText.color = Color.blue;
                    winPlayerText.text = "Com Wins";
                }
                else
                {
                    winPlayerText.color = Color.blue;
                    winPlayerText.text = "Player 2 Wins";
                }
            }
            if (collision.gameObject.transform.name == "Blue")
            {
                winPlayerText.color = Color.red;
                winPlayerText.text = "Player 1 Wins";
            }
        }
    }

    private void SetWinScreenActive()
    {
        winScreen.SetActive(true);
    }

    public void RestartButton()
    {
        ResumeButton();
        buttonSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuButton()
    {
        ResumeButton();
        buttonSound.Play();
        SceneManager.LoadScene(0);
    }

    public void PauseButton()
    {
        isPause = true;
        if (bgMusic != null) bgMusic.Pause();
        pauseScreen.SetActive(true);
        gameScreen.SetActive(false);
        Time.timeScale = 0;
    }

    public void ResumeButton()
    {
        if (bgMusic != null) bgMusic.Play();
        isPause = false;
        pauseScreen.SetActive(false);
        gameScreen.SetActive(true);
        Time.timeScale = 1;
    }
}
