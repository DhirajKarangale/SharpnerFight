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
    [SerializeField] AudioSource bgMusic;


    private float timer;
    [SerializeField] Text playerText;
    [SerializeField] Text singlePlayerText;
    [SerializeField] Text difficultyText;
    [SerializeField] GameObject timerText;

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
    }

    private void Update()
    {
        if (PlayersName.Count == 1) GameOver.isGameOver = true;
        if(!GameOver.isGameOver && Input.GetKey(KeyCode.Escape))
        {
            if (isPause) ResumeButton();
            else PauseButton();
        }

        if (GameOver.isGameOver) bgMusic.Stop();

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
            }
            else
            {
                instanstiatedPlayers[i].gameObject.GetComponent<Sharpner>().enabled = false;
            }
        }
    }

    private void Stats()
    {
        if (Menu.player == 1)
        {
            difficultyText.gameObject.SetActive(true);
            singlePlayerText.text = "Single Player";
            if (Menu.difficulty == 1)
            {
                difficultyText.color = Color.green;
                difficultyText.text = "Easy";
            }
            if (Menu.difficulty == 2)
            {
                difficultyText.color = Color.yellow;
                difficultyText.text = "Medium";
            }
            if (Menu.difficulty == 3)
            {
                difficultyText.color = Color.red;
                difficultyText.text = "Hard";
            }
        }
        else if(Menu.player == 2)
        {
            difficultyText.gameObject.SetActive(false);
            singlePlayerText.color = Color.blue;
            singlePlayerText.text = "Two Player";
        }
        else if (Menu.player == 3)
        {
            difficultyText.gameObject.SetActive(false);
            singlePlayerText.color = Color.green;
            singlePlayerText.text = "Three Player";
        }
        else if (Menu.player == 4)
        {
            difficultyText.gameObject.SetActive(false);
            singlePlayerText.color = Color.yellow;
            singlePlayerText.text = "Four Player";
        }
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
