using UnityEngine;

public class BGMusic : MonoBehaviour
{
    public AudioSource bgMusic;

    public static BGMusic instance = null;
    public static BGMusic Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
