using UnityEngine;
using UnityEngine.Advertisements;

public class ADManager : MonoBehaviour
{
    public static ADManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Advertisement.Initialize("4266075", true);
        if (Random.Range(0, 4) == 2) Invoke("ShowInterstitialAd", 2f);
    }

    public void ShowInterstitialAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("Interstitial_Android");
        }
    }
}
