using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static bool isGameOver,isSharpnerHitEnd;
    
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject gameScreen;

    [SerializeField] Text winPlayerText;

    [SerializeField] AudioSource dyeSound;
    [SerializeField] AudioSource buttonSound;

    private void Start()
    {
        isGameOver = false;
        isSharpnerHitEnd = false;
    }

    private void Update()
    {
        if(GameManager.PlayersName.Count == 1)
        {
            gameScreen.SetActive(false);
            Invoke("SetWinScreenActive", 1);
            if (GameManager.PlayersName[0] == "Player1") winPlayerText.color = Color.red;
            if (GameManager.PlayersName[0] == "Player2") winPlayerText.color = Color.blue;
            if (GameManager.PlayersName[0] == "Player3") winPlayerText.color = Color.green;
            if (GameManager.PlayersName[0] == "Player4") winPlayerText.color = Color.gray;
            winPlayerText.text = GameManager.PlayersName[0] + " Win";
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isGameOver)
        {
            collision.gameObject.transform.localScale = collision.gameObject.transform.localScale / 2;
            if (Menu.player == 1)
            {
                isGameOver = true;
                dyeSound.Play();
                gameScreen.SetActive(false);
                Invoke("SetWinScreenActive", 1);
                if (collision.transform.name == "AI(Clone)")
                {
                    winPlayerText.color = Color.red;
                    winPlayerText.text = "Player 1 Win"; 
                }
                else if(collision.transform.name == "Player1(Clone)")
                {
                    winPlayerText.color = Color.blue;
                    winPlayerText.text = "Com Win";
                }
            }
            else
            {
                for (int i = 0; i < GameManager.PlayersName.Count; i++)
                {
                    if (collision.transform.name == GameManager.PlayersName[i]+"(Clone)")
                    {
                        isSharpnerHitEnd = true;
                        GameManager.PlayersName.Remove(GameManager.PlayersName[i]);
                        GameManager.instanstiatedPlayers.Remove(GameManager.instanstiatedPlayers[i]);
                        collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        collision.gameObject.GetComponent<Sharpner>().enabled = false;
                        Destroy(collision.gameObject,5f);
                        if ((GameManager.turn <= 0) || (GameManager.turn == GameManager.PlayersName.Count)) GameManager.turn = 0;
                    }
                }
            }
        }
    }

    private void SetWinScreenActive()
    {
        gameOverScreen.SetActive(true);
    }

    public void RestartButton()
    {
        Time.timeScale = 1;
        buttonSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuButton()
    {
        Time.timeScale = 1;
        buttonSound.Play();
        SceneManager.LoadScene(0);
    }
}
