using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ground : MonoBehaviour
{
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject gameScreen;
    [SerializeField] Text winPlayerText;
    string winPlayer;
    public static bool isGameOver;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Sharpner")
        {
            gameScreen.SetActive(false);
            Invoke("SetWinScreenActive", 1);
            isGameOver = true;
            collision.gameObject.transform.localScale = collision.gameObject.transform.localScale / 2;
            if(collision.gameObject.transform.name == "Red")
            {
                winPlayer = "Player 2 Wins";
                winPlayerText.color = Color.blue;
                winPlayerText.text = winPlayer;
            }
            if (collision.gameObject.transform.name == "Blue")
            {
                winPlayer = "Player 1 Wins";
                winPlayerText.color = Color.red;
                winPlayerText.text = winPlayer;
            }
        }
    }

    private void SetWinScreenActive()
    {
        winScreen.SetActive(true);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
