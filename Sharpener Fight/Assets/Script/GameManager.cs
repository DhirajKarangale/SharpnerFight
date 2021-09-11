using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static List<string> PlayersName = new List<string>();
    public static List<GameObject> instanstiatedPlayers = new List<GameObject>();
    public static string currentTurn;
    public static byte turn;

    [SerializeField] Transform[] spwanPoint;
    [SerializeField] GameObject[] players;

    [SerializeField] GameObject gameScreen;
    [SerializeField] GameObject pauseScreen;
    
    [SerializeField] AudioSource buttonSound;

    private float timer;

    [Header("Text")]
    [SerializeField] Text playerText;
    [SerializeField] Text singlePlayerText;
    [SerializeField] Text singlePlayertextInGameOver;
    [SerializeField] Text difficultyText;
    [SerializeField] Text difficuiltyTextInGameOver;
    [SerializeField] Text pushForwardButtonText;
    [SerializeField] Text pullBackButtonText;
    [SerializeField] GameObject timerText;

    [Header("Button")]
    [SerializeField] Button pushForwardButton;
    [SerializeField] Button pullBackButton;
    private int pushForward;

    private bool isPause,isSharpnersInstanstiate;
        

    private void Start()
    {
        PlayersName.Clear();
        instanstiatedPlayers.Clear();
        isPause = false;
        isSharpnersInstanstiate = false;
        timer = 10;
        turn = 0;
        InstanstiatePlayer();
        Stats();
        ControlInitialSetup();
    }

    private void Update()
    {
        if (PlayersName.Count == 1) GameOver.isGameOver = true;
        if(!GameOver.isGameOver && Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause) ResumeButton();
            else PauseButton();
        }

        if (isSharpnersInstanstiate && !GameOver.isGameOver) TurnCalculator();
    }

    private void TurnCalculator()
    {
        if (Menu.player == 1)
        {
            if ((currentTurn == PlayersName[1]) && !Sharpner.isSharpnerFire)
            {
                timerText.SetActive(true);
                playerText.gameObject.SetActive(true);
                playerText.color = Color.red;
                playerText.text = "Player 1";
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                    timerText.GetComponent<Text>().text = timer.ToString();
                }
                else
                {
                    timer = 10;
                    currentTurn = PlayersName[0];
                }
            }
            else
            {
                timerText.SetActive(false);
                playerText.gameObject.SetActive(false);
                timerText.SetActive(false);
                timer = 10;
            }
        }
        else 
        {
            if(!Sharpner.isSharpnerFire)
            {
                timerText.SetActive(true);
                playerText.gameObject.SetActive(true);
                currentTurn = PlayersName[turn];
                if (currentTurn == "Player1") playerText.color = Color.red;
                else if(currentTurn == "Player2") playerText.color = Color.blue;
                else if(currentTurn == "Player3") playerText.color = Color.green;
                else if(currentTurn == "Player4") playerText.color = Color.yellow;

                if (timer > 0)
                {
                    playerText.text = currentTurn;
                    timer -= Time.deltaTime;
                    timerText.GetComponent<Text>().text = timer.ToString();

                }
                else
                {
                    timer = 10;
                    if (turn < (PlayersName.Count-1)) turn++;
                    else turn = 0;
                    currentTurn = PlayersName[turn];
                }
              DesableSharpner();
            }
            else
            {
                timer = 10;
                timerText.SetActive(false);
                playerText.gameObject.SetActive(false);
            }
        }
    }
       
    private void InstanstiatePlayer()
    {
        if (Menu.player == 1)
        {
            Instantiate(players[0], spwanPoint[1].position, spwanPoint[1].rotation); //Instantiate AI
            instanstiatedPlayers.Add(Instantiate(players[1], spwanPoint[0].position, spwanPoint[0].rotation)); //Instantiate Player
            PlayersName.Add(players[0].name);
            PlayersName.Add(players[1].name);
            currentTurn = PlayersName[1];
            isSharpnersInstanstiate = true;
        }
        else
        {
            for(int i = 1; i <= Menu.player; i++)
            {
               instanstiatedPlayers.Add(Instantiate(players[i], spwanPoint[i-1].position, spwanPoint[i-1].rotation));
               PlayersName.Add(players[i].name);
            }
            turn = 0;
            currentTurn = PlayersName[turn];
            isSharpnersInstanstiate = true;
        }
    }

    public void DesableSharpner()
    {
        for (int i = 0; i < PlayersName.Count; i++)
        {
            if (currentTurn == PlayersName[i])
            {
                instanstiatedPlayers[i].gameObject.GetComponent<Sharpner>().enabled = true;
                instanstiatedPlayers[i].gameObject.GetComponent<Sharpner>().lightObj.SetActive(true);
            }
            else
            {
                instanstiatedPlayers[i].gameObject.GetComponent<Sharpner>().enabled = false;
                instanstiatedPlayers[i].gameObject.GetComponent<Sharpner>().lightObj.SetActive(false);
            }
        }
    }

    private void Stats()
    {
        if (Menu.player == 1)
        {
            TextAndColor(singlePlayerText, Color.white, "Single Player");
            TextAndColor(singlePlayertextInGameOver, Color.white, "Single Player");
            difficultyText.gameObject.SetActive(true);
            difficuiltyTextInGameOver.gameObject.SetActive(true);

            if (Menu.difficulty == 1)
            {
                TextAndColor(difficultyText, Color.green, "Easy");
                TextAndColor(difficuiltyTextInGameOver, Color.green, "Easy");
            }
            if (Menu.difficulty == 2)
            {
                TextAndColor(difficultyText, Color.yellow, "Medium");
                TextAndColor(difficuiltyTextInGameOver, Color.yellow, "Medium");
            }
            if (Menu.difficulty == 3)
            {
                TextAndColor(difficultyText, Color.red, "Hard");
                TextAndColor(difficuiltyTextInGameOver, Color.red, "Hard");
            }
        }
        else if(Menu.player == 2)
        {
            difficultyText.gameObject.SetActive(false);
            difficuiltyTextInGameOver.gameObject.SetActive(false);

            TextAndColor(singlePlayerText, Color.blue, "Two Player");
            TextAndColor(singlePlayertextInGameOver, Color.blue, "Two Player");

        }
        else if (Menu.player == 3)
        {
            difficultyText.gameObject.SetActive(false);
            difficuiltyTextInGameOver.gameObject.SetActive(false);

            TextAndColor(singlePlayerText, Color.green, "Three Player");
            TextAndColor(singlePlayertextInGameOver, Color.green, "Three Player");

        }
        else if (Menu.player == 4)
        {
            difficultyText.gameObject.SetActive(false);
            difficuiltyTextInGameOver.gameObject.SetActive(false);

            TextAndColor(singlePlayerText, Color.yellow, "Four Player");
            TextAndColor(singlePlayertextInGameOver, Color.yellow, "Four Player");

        }
    }

    private void TextAndColor(Text text,Color color,string textValue)
    {
        text.color = color;
        text.text = textValue;
    }
 
    public void PauseButton()
    {
        buttonSound.Play();
        BGMusic.instance.bgMusic.volume = 0.06f;
        isPause = true;
        pauseScreen.SetActive(true);
        gameScreen.SetActive(false);
        Time.timeScale = 0;
    }

    public void ResumeButton()
    {
        buttonSound.Play();
        BGMusic.instance.bgMusic.volume = 0.2f;
        isPause = false;
        pauseScreen.SetActive(false);
        gameScreen.SetActive(true);
        Time.timeScale = 1;
    }


    private void ControlInitialSetup()
    {
        if (PlayerPrefs.GetInt("PushForward", 0) == 0) PushForwardButton(false);
        else PullBackButton(false);
    }

    public void PushForwardButton(bool isButtonSoundPlay)
    {
        if (isButtonSoundPlay) buttonSound.Play();

        Controlbutton(pushForwardButton, false, pushForwardButtonText, Color.yellow, "Selected");
        Controlbutton(pullBackButton, true, pullBackButtonText, Color.white, "Select");

        PlayerPrefs.SetInt("PushForward", 0);
        PlayerPrefs.Save();
    }

    public void PullBackButton(bool isButtonSoundPlay)
    {
        if (isButtonSoundPlay) buttonSound.Play();

        Controlbutton(pushForwardButton, true, pushForwardButtonText, Color.white, "Select");
        Controlbutton(pullBackButton, false, pullBackButtonText, Color.yellow, "Selected");

        PlayerPrefs.SetInt("PushForward", 1);
        PlayerPrefs.Save();
    }

    private void Controlbutton(Button button, bool isInteractable, Text buttonText, Color textColor, string textWord)
    {
        button.interactable = isInteractable;
        buttonText.color = textColor;
        buttonText.text = textWord;
    }
}
