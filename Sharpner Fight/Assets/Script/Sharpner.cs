using UnityEngine;

public class Sharpner : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] Transform directionLine;
    [SerializeField] Transform forcePoint;
    [SerializeField] AudioSource slideSound;

    public static bool isSharpnerFire;
    private bool isSharpnerTouch;
    private Rigidbody2D rigidBody;
    private RaycastHit2D raycastHit;
    private float scale;

    [Header("CollisionEffect")]
    [SerializeField] AudioSource collideSound;
    [SerializeField] GameObject collisionEffect;
    private bool collisionEffectAllow;

    [Header("Camera Shake")]
    private Vector3 cameraInitialPosition;
    private float shakeMagnetude = 0.04f, shakeTime = 0.2f;
    private Camera mainCamera;

    private void Start()
    {
        transform.localScale = new Vector3(1, 1, 1);
        isSharpnerFire = false;
        isSharpnerTouch = false;
        mainCamera = GameObject.FindObjectOfType<Camera>();
        rigidBody = GetComponent<Rigidbody2D>();
        collisionEffectAllow = true;
        directionLine.localScale = Vector2.zero;
    }

    private void Update()
    {
        if (GameOver.isGameOver)
        {
            rigidBody.velocity = Vector2.zero;
        }

        if (!GameOver.isGameOver && Input.GetMouseButton(0))
        {
            raycastHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
            if (raycastHit.collider)
            {
                if ((raycastHit.transform.name == (GameManager.currentTurn + "(Clone)"))) isSharpnerTouch = true;
               
                if (isSharpnerTouch)
                {
                    isSharpnerFire = true;
                    isSharpnerTouch = true;
                    var dir = raycastHit.point - new Vector2(transform.position.x, transform.position.y);
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle + 270, Vector3.forward);

                    scale = Vector2.Distance(transform.position, raycastHit.point) * 0.5f;
                    scale = Mathf.Clamp(scale, 0, 3);
                    directionLine.localScale = new Vector3(scale * 0.5f, scale * 0.5f, scale * 0.5f);
                }
            }
        }
        else if (isSharpnerTouch && !GameOver.isGameOver && Input.GetMouseButtonUp(0))
        {
            isSharpnerTouch = false;
            GameManager.currentTurn = "None";
            slideSound.Play();
            rigidBody.AddForce(new Vector3(forcePoint.position.x - transform.position.x, forcePoint.position.y - transform.position.y, 0) * 35 * scale);
            directionLine.localScale = Vector3.zero;
            Invoke("ActivateNextPlayer", 2f);
        }
    }

    private void ActivateNextPlayer()
    {
        if (Menu.player == 1) GameManager.currentTurn = GameManager.PlayersName[0];
        else
        {
            if (GameManager.turn < (GameManager.PlayersName.Count - 1)) GameManager.turn++;
            else GameManager.turn = 0;
            GameManager.currentTurn = GameManager.PlayersName[GameManager.turn];
        }
        isSharpnerFire = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Sharpner") && collisionEffectAllow)
        {
            collisionEffectAllow = false;
            Destroy(Instantiate(collisionEffect, collision.GetContact(0).point, Quaternion.identity), 0.4f);
            if (collideSound.isPlaying) collideSound.Stop();
            collideSound.Play();
            ShakeIt();
            Invoke("AllowCollisionEffect", 1);
        }
    }

    private void AllowCollisionEffect()
    {
        collisionEffectAllow = true;
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
