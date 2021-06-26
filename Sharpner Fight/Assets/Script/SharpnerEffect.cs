using UnityEngine;

public class SharpnerEffect : MonoBehaviour
{
    public static bool isSharpnerCollide;
    [Header("Camera Shake")]
    private Vector3 cameraInitialPosition;
    private float shakeMagnetude = 0.04f, shakeTime = 0.2f;
    [SerializeField] Camera mainCamera;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Sharpner")
        {
            isSharpnerCollide = true;
            ShakeIt();
        }
    }

    private void ShakeIt()
    {
        cameraInitialPosition = mainCamera.transform.position;
        InvokeRepeating("StartCameraShaking", 0f, 0.005f);
        Invoke("StopCameraShaking", shakeTime);
    }

    private void StartCameraShaking()
    {
        float cameraShakingOffsetX = UnityEngine.Random.value * shakeMagnetude * 2 - shakeMagnetude;
        float cameraShakingOffsetY = UnityEngine.Random.value * shakeMagnetude * 2 - shakeMagnetude;
        Vector3 cameraIntermadiatePosition = mainCamera.transform.position;
        cameraIntermadiatePosition.x += cameraShakingOffsetX;
        cameraIntermadiatePosition.y += cameraShakingOffsetY;
        mainCamera.transform.position = cameraIntermadiatePosition;
    }

    private void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");
        mainCamera.transform.position = cameraInitialPosition;
    }
}
