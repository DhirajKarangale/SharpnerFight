using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static bool isGameOver, isSharpnerHitEnd;
    private bool isAdAllow;
    
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject gameScreen;
    [SerializeField] ParticleSystem celebrationPS;

    [SerializeField] Text winPlayerText;
    [SerializeField] Text playerEliminateText;

    [SerializeField] AudioSource dyeSound;
    [SerializeField] AudioSource buttonSound;

    private void Start()
    {
        celebrationPS.Stop();
        isAdAllow = true;
        isGameOver = false;
        isSharpnerHitEnd = false;
        SetSoundToNormal();
    }

    private void Update()
    {
        if (GameManager.PlayersName.Count == 1) GameOver1();
        else if (GameManager.PlayersName.Count == 0) GameOver0();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isGameOver)
        {
            collision.gameObject.transform.localScale = collision.gameObject.transform.localScale / 2;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

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
                    celebrationPS.Play();

                    TextSetter(winPlayerText, true, Color.red, "Player 1 Win");
                }
                else if(collision.transform.name == "Player1(Clone)")
                {
                    TextSetter(winPlayerText, true, Color.blue, "Com Win");
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
                            TextSetter(playerEliminateText, true, Color.red, "Player 1 Eliminated");
                        }
                        else if (collision.transform.name == "Player2(Clone)")
                        {
                            TextSetter(playerEliminateText, true, Color.blue, "Player 2 Eliminated");
                        }
                        else if (collision.transform.name == "Player3(Clone)")
                        {
                            TextSetter(playerEliminateText, true, Color.green, "Player 3 Eliminated");
                        }
                        else if (collision.transform.name == "Player4(Clone)")
                        {
                            TextSetter(playerEliminateText, true, Color.yellow, "Player 4 Eliminated");
                        }

                        if ((GameManager.turn > eleminatedPlayerindex) && (GameManager.turn == GameManager.PlayersName.Count)) GameManager.turn--;
                        else if ((GameManager.turn == GameManager.PlayersName.Count) || (eleminatedPlayerindex == GameManager.PlayersName.Count)) GameManager.turn = 0;
                        else if (GameManager.turn < eleminatedPlayerindex) GameManager.turn++;
                        else if ((GameManager.turn == eleminatedPlayerindex) || (GameManager.turn > eleminatedPlayerindex)) return;
                    }
                }
            }
        }
    }

    private void GameOver1()
    {
        celebrationPS.Play();

        BGMusic.instance.bgMusic.volume = 0.06f;

        GameManager.instanstiatedPlayers[0].GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        gameScreen.SetActive(false);
        Invoke("SetWinScreenActive", 1);

        if (GameManager.PlayersName[0] == "Player1") winPlayerText.color = Color.red;
        if (GameManager.PlayersName[0] == "Player2") winPlayerText.color = Color.blue;
        if (GameManager.PlayersName[0] == "Player3") winPlayerText.color = Color.green;
        if (GameManager.PlayersName[0] == "Player4") winPlayerText.color = Color.yellow;

        winPlayerText.text = GameManager.PlayersName[0].Insert(6, " ") + " Win";
    }

    private void GameOver0()
    {
        BGMusic.instance.bgMusic.volume = 0.06f;
        gameScreen.SetActive(false);
        Invoke("SetWinScreenActive", 1);
        winPlayerText.text = "Nobody Win this Match";
    }

    private void SetWinScreenActive()
    {
        gameOverScreen.SetActive(true);
        if (isAdAllow)
        {
            if (Random.Range(0, 7) == 3)
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
        celebrationPS.Stop();
        Time.timeScale = 1;
        buttonSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuButton()
    {
        celebrationPS.Stop();
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

    private void TextSetter(Text text, bool setTextActive,Color textColor,string textWord)
    {
        text.gameObject.SetActive(setTextActive);
        text.color = textColor;
        text.text = textWord;
        Invoke("DesableEliminatePlayerText", 1.5f);
    }
} 
