using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static byte player;
    public static byte difficulty;
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject singelPlayerPanel;
    [SerializeField] GameObject multiPlayerPanel;
    [SerializeField] GameObject aboutPanel;
    [SerializeField] GameObject quitPanel;
    [SerializeField] GameObject loadingPng;
    [SerializeField] AudioSource buttonSound;
    [SerializeField] GameObject panelLight;
    private bool isQuitPanelActivate;

    private void Start()
    {
        BackButton();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isQuitPanelActivate) BackButton();
            else ActiveQuitPanel();
        }
    }
     

    public void SinglePlayerButton()
    {
        buttonSound.Play();
        player = 1;

        menuPanel.SetActive(false);
        singelPlayerPanel.SetActive(true);
        multiPlayerPanel.SetActive(false);
        aboutPanel.SetActive(false);
        quitPanel.SetActive(false);
        panelLight.SetActive(true);
    }

    public void EasyButton()
    {
        panelLight.SetActive(false);
        loadingPng.SetActive(true);
        singelPlayerPanel.SetActive(false);
       
        buttonSound.Play();
        difficulty = 1;
        SceneManager.LoadScene(1);
    }

    public void MediumButton()
    {
        panelLight.SetActive(false);
        loadingPng.SetActive(true);
        singelPlayerPanel.SetActive(false);

        buttonSound.Play();
        difficulty = 2;
        SceneManager.LoadScene(1);
    }

    public void HardButton()
    {
        panelLight.SetActive(false);
        loadingPng.SetActive(true);
        singelPlayerPanel.SetActive(false);

        buttonSound.Play();
        difficulty = 3;
        SceneManager.LoadScene(1);
    }
      
    

    public void MultiPlayerPlayerButton()
    {
        buttonSound.Play();

        menuPanel.SetActive(false);
        singelPlayerPanel.SetActive(false);
        multiPlayerPanel.SetActive(true);
        aboutPanel.SetActive(false);
        quitPanel.SetActive(false);
        panelLight.SetActive(true);
    }

    public void TwoPlayerButton()
    {
        panelLight.SetActive(false);
        loadingPng.SetActive(true);
        multiPlayerPanel.SetActive(false);

        buttonSound.Play();
        player = 2;
        SceneManager.LoadScene(1);
    }

    public void ThreePlayerButton()
    {
        panelLight.SetActive(false);
        loadingPng.SetActive(true);
        multiPlayerPanel.SetActive(false);

        buttonSound.Play();
        player = 3;
        SceneManager.LoadScene(1);
    }

    public void FourPlayerButton()
    {
        panelLight.SetActive(false);
        loadingPng.SetActive(true);
        multiPlayerPanel.SetActive(false);

        buttonSound.Play();
        player = 4;
        SceneManager.LoadScene(1);
    }



    public void QuitButton()
    {
        buttonSound.Play();
        Application.Quit();
    }

    public void ActiveQuitPanel()
    {
        isQuitPanelActivate = true;

        menuPanel.SetActive(false);
        singelPlayerPanel.SetActive(false);
        multiPlayerPanel.SetActive(false);
        aboutPanel.SetActive(false);
        quitPanel.SetActive(true);
        panelLight.SetActive(false);
    }


    public void AboutPanel()
    {
        menuPanel.SetActive(false);
        singelPlayerPanel.SetActive(false);
        multiPlayerPanel.SetActive(false);
        aboutPanel.SetActive(true);
        quitPanel.SetActive(false);
        panelLight.SetActive(false);
    }

    public void MoreGamesButton()
    {
        buttonSound.Play();
        Application.OpenURL("https://play.google.com/store/apps/developer?id=DK_Software");
    }
   
    public void LinkedInButton()
    {
        buttonSound.Play();
        Application.OpenURL("https://www.linkedin.com/in/dhiraj-karangale-464ab91bb");
    }

    public void YoutubeButton()
    {
        buttonSound.Play();
        Application.OpenURL("https://www.youtube.com/channel/UC_Dnn-QqlnrdYpKXycyzJDA");
    }


    public void BackButton()
    {
        isQuitPanelActivate = false;
        buttonSound.Play();

        menuPanel.SetActive(true);
        singelPlayerPanel.SetActive(false);
        multiPlayerPanel.SetActive(false);
        aboutPanel.SetActive(false);
        quitPanel.SetActive(false);
        panelLight.SetActive(true);
    }
}
