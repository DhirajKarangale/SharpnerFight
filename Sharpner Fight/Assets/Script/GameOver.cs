using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static bool isGameOver, isSharpnerHitEnd;
    private bool isAdAllow;
    
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject gameScreen;

    [SerializeField] Text winPlayerText;
    [SerializeField] Text playerEliminateText;

    [SerializeField] AudioSource dyeSound;
    [SerializeField] AudioSource buttonSound;

    private void Start()
    {
        isAdAllow = true;
        SetSoundToNormal();
        isGameOver = false;
        isSharpnerHitEnd = false;
    }

    private void Update()
    {
        if(GameManager.PlayersName.Count <= 1)
        {
            BGMusic.instance.bgMusic.volume = 0.06f;
            GameManager.instanstiatedPlayers[0].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameScreen.SetActive(false);
            Invoke("SetWinScreenActive", 1);
            if (GameManager.PlayersName[0] == "Player1") winPlayerText.color = Color.red;
            if (GameManager.PlayersName[0] == "Player2") winPlayerText.color = Color.blue;
            if (GameManager.PlayersName[0] == "Player3") winPlayerText.color = Color.green;
            if (GameManager.PlayersName[0] == "Player4") winPlayerText.color = Color.yellow;
            winPlayerText.text = GameManager.PlayersName[0].Insert(6," ") + " Win";
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isGameOver)
        {
            collision.gameObject.transform.localScale = collision.gameObject.transform.localScale / 2;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            if (collision.gameObject.GetComponent<Sharpner>() != null)
            {
                collision.gameObject.GetComponent<Sharpner>().lightObj.SetActive(false);
                collision.gameObject.GetComponent<Sharpner>().slidePS.Stop();
            }

            if (Menu.player == 1)
            {
                if (collision.gameObject.GetComponent<AI>() != null)
                {
                    collision.gameObject.GetComponent<AI>().slidePS.Stop();
                    collision.gameObject.GetComponent<AI>().lightObj.SetActive(false);
                }

                BGMusic.instance.bgMusic.volume = 0.06f;
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
                        BGMusic.instance.bgMusic.volume = 0.09f;
                        Invoke("SetSoundToNormal", 1.5f);

                        dyeSound.Play();
                        playerEliminateText.gameObject.SetActive(true);
                        isSharpnerHitEnd = true;
                       
                        int eleminatedPlayerindex = GameManager.PlayersName.IndexOf(GameManager.PlayersName[i]);
                        GameManager.PlayersName.Remove(GameManager.PlayersName[i]);
                        GameManager.instanstiatedPlayers.Remove(GameManager.instanstiatedPlayers[i]);

                        collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        collision.gameObject.GetComponent<Sharpner>().enabled = false;
                       
                        Destroy(collision.gameObject,5f);

                        if(collision.transform.name == "Player1(Clone)")
                        {
                            playerEliminateText.gameObject.SetActive(true);
                            playerEliminateText.color = Color.red;
                            playerEliminateText.text = "Player 1 Eliminated";
                            Invoke("DesableEliminatePlayerText", 1.5f);
                        }
                        else if (collision.transform.name == "Player2(Clone)")
                        {
                            playerEliminateText.gameObject.SetActive(true);
                            playerEliminateText.color = Color.blue;
                            playerEliminateText.text = "Player 2 Eliminated";
                            Invoke("DesableEliminatePlayerText", 1.5f);
                        }
                        else if (collision.transform.name == "Player3(Clone)")
                        {
                            playerEliminateText.gameObject.SetActive(true);
                            playerEliminateText.color = Color.green;
                            playerEliminateText.text = "Player 3 Eliminated";
                            Invoke("DesableEliminatePlayerText", 1.5f);
                        }
                        else if (collision.transform.name == "Player4(Clone)")
                        {
                            playerEliminateText.gameObject.SetActive(true);
                            playerEliminateText.color = Color.yellow;
                            playerEliminateText.text = "Player 4 Eliminated";
                            Invoke("DesableEliminatePlayerText", 1.5f);
                        }

                        if ((eleminatedPlayerindex - GameManager.turn) == -1) GameManager.turn--;
                        else if ((eleminatedPlayerindex > GameManager.turn) && (GameManager.turn < (GameManager.PlayersName.Count-1)) && (eleminatedPlayerindex != 0)) GameManager.turn++;
                        else if ((GameManager.turn <= 0) || (GameManager.turn == GameManager.PlayersName.Count) || ((eleminatedPlayerindex >= GameManager.PlayersName.Count))) GameManager.turn = 0;
                      
                    }
                }
            }
        }
    }

    private void SetWinScreenActive()
    {
        gameOverScreen.SetActive(true);
        if (isAdAllow)
        {
            if (Random.Range(0, 5) == 3)
            {
                isAdAllow = false;
                Invoke("ShowAd", 3);
            }
        }
    }

    private void DesableEliminatePlayerText()
    {
        playerEliminateText.gameObject.SetActive(false);
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

    private void SetSoundToNormal()
    {
        BGMusic.instance.bgMusic.volume = 0.2f;
    }

    private void ShowAd()
    {
        ADManager.instance.ShowInterstitialAd();
    }
} 
