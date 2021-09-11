using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] Button pushForwardButton;
    [SerializeField] Button pullBackButton;

    [Header("Text")]
    [SerializeField] Text pushForwardButtonText;
    [SerializeField] Text pullBackButtonText;

    [Header("Panel")]
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject singelPlayerPanel;
    [SerializeField] GameObject multiPlayerPanel;
    [SerializeField] GameObject aboutPanel;
    [SerializeField] GameObject changeControlPanel;
    [SerializeField] GameObject quitPanel;

    [Header("Other")]
    [SerializeField] GameObject loadingPng;
    [SerializeField] GameObject panelLight;
    [SerializeField] GameObject exitPanelLight;
    [SerializeField] AudioSource buttonSound;
    private bool isQuitPanelAllow;
    public static byte player;
    public static byte difficulty;

    private void Start()
    {
        ControlInitialSetup();
       
        BGMusic.instance.bgMusic.volume = 0.2f;

        BackButton(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isQuitPanelAllow) ActiveQuitPanel();
            else BackButton(true);
        }
    }
  

    public void SinglePlayerButton()
    {
        buttonSound.Play();
        player = 1;
        isQuitPanelAllow = false;

        menuPanel.SetActive(false);
        singelPlayerPanel.SetActive(true);
        multiPlayerPanel.SetActive(false);
        aboutPanel.SetActive(false);
        quitPanel.SetActive(false);
        panelLight.SetActive(true);
        exitPanelLight.SetActive(false);
        changeControlPanel.SetActive(false);
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
        isQuitPanelAllow = false;

        menuPanel.SetActive(false);
        singelPlayerPanel.SetActive(false);
        multiPlayerPanel.SetActive(true);
        aboutPanel.SetActive(false);
        quitPanel.SetActive(false);
        panelLight.SetActive(true);
        exitPanelLight.SetActive(false);
        changeControlPanel.SetActive(false);
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
        buttonSound.Play();
        isQuitPanelAllow = false;

        menuPanel.SetActive(false);
        singelPlayerPanel.SetActive(false);
        multiPlayerPanel.SetActive(false);
        aboutPanel.SetActive(false);
        quitPanel.SetActive(true);
        panelLight.SetActive(false);
        exitPanelLight.SetActive(true);
        changeControlPanel.SetActive(false);
    }


    public void AboutPanel()
    {
        buttonSound.Play();
        isQuitPanelAllow = false;
        menuPanel.SetActive(false);
        singelPlayerPanel.SetActive(false);
        multiPlayerPanel.SetActive(false);
        aboutPanel.SetActive(true);
        quitPanel.SetActive(false);
        panelLight.SetActive(false);
        exitPanelLight.SetActive(false);
        changeControlPanel.SetActive(false);
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


    public void BackButton(bool isButtonSoundPlay)
    {
        if (isButtonSoundPlay) buttonSound.Play();
        isQuitPanelAllow = true;

        menuPanel.SetActive(true);
        singelPlayerPanel.SetActive(false);
        multiPlayerPanel.SetActive(false);
        aboutPanel.SetActive(false);
        quitPanel.SetActive(false);
        panelLight.SetActive(true);
        exitPanelLight.SetActive(false);
        changeControlPanel.SetActive(false);
    }

    public void ChangeControlButton()
    {
        buttonSound.Play();
        menuPanel.SetActive(false);
        singelPlayerPanel.SetActive(false);
        multiPlayerPanel.SetActive(false);
        aboutPanel.SetActive(false);
        quitPanel.SetActive(false);
        panelLight.SetActive(false);
        exitPanelLight.SetActive(false);
        changeControlPanel.SetActive(true);
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

    private void ControlInitialSetup()
    {
        if (PlayerPrefs.GetInt("PushForward", 0) == 0) PushForwardButton(false);
        else PullBackButton(false);

        if (!PlayerPrefs.HasKey("PushForward")) ChangeControlButton();
    }

    private void Controlbutton(Button button, bool isInteractable, Text buttonText , Color textColor,string textWord)
    {
        button.interactable = isInteractable;
        buttonText.color = textColor;
        buttonText.text = textWord;
    }
}
