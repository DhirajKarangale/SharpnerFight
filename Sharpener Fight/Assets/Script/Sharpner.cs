using UnityEngine;

public class Sharpner : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] Transform sharpnerBG;
    [SerializeField] Transform forcePoint;
    public ParticleSystem slidePS;
    [SerializeField] AudioSource slideSound;
    public GameObject lightObj;
    [SerializeField] Rigidbody2D rigidBody;

    public static bool isSharpnerFire;
    private bool isSharpnerTouch;
    private float scale;

    [Header("CollisionEffect")]
    [SerializeField] GameObject collisionEffect;
    public bool isCollisionEffectAllow;

    [Header("Camera Shake")]
    private Vector3 cameraInitialPosition;
    private float shakeMagnetude = 0.04f, shakeTime = 0.2f;
    private Camera mainCamera;

    private void Start()
    {
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        isSharpnerFire = false;
        isSharpnerTouch = false;
        mainCamera = GameObject.FindObjectOfType<Camera>();
        isCollisionEffectAllow = true;
        sharpnerBG.localScale = Vector2.zero;
    }

    private void Update()
    {
        if (GameOver.isGameOver)
        {
            rigidBody.velocity = Vector2.zero;
            lightObj.SetActive(false);
        }

        if (AI.playerturnFromAI && !GameOver.isGameOver) lightObj.SetActive(true);

        if (!GameOver.isGameOver && Input.GetMouseButton(0)) SharpenerController();
        else if (isSharpnerTouch && !GameOver.isGameOver && Input.GetMouseButtonUp(0)) SharpenerThrow();
    }

    private void SharpenerController()
    {
        RaycastHit2D raycastHit;
        raycastHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
        if (raycastHit.collider)
        {
            if ((raycastHit.transform.name == (GameManager.currentTurn + "(Clone)"))) isSharpnerTouch = true;

            if (isSharpnerTouch)
            {
                GameOver.isSharpnerHitEnd = false;
                isSharpnerFire = true;
                isSharpnerTouch = true;

                var dir = raycastHit.point - new Vector2(transform.position.x, transform.position.y);
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                int pushForward = PlayerPrefs.GetInt("PushForward", 0);
                if (pushForward == 0) transform.rotation = Quaternion.AngleAxis(angle - 270, Vector3.forward);
                else transform.rotation = Quaternion.AngleAxis(angle + 270, Vector3.forward);

                scale = Vector2.Distance(transform.position, raycastHit.point) * 0.5f;
                scale = Mathf.Clamp(scale, 0.5f, 3);
                sharpnerBG.localScale = new Vector3(scale * 0.5f, scale * 0.5f, scale * 0.5f);
            }
        }
    }

    private void SharpenerThrow()
    {
        AI.playerturnFromAI = false;
        lightObj.SetActive(false);
        isSharpnerTouch = false;
        GameManager.currentTurn = "None";
        slidePS.Play();
        slideSound.Play();

        rigidBody.AddForce(new Vector3(forcePoint.position.x - transform.position.x, forcePoint.position.y - transform.position.y, 0) * 39 * scale);
        sharpnerBG.localScale = Vector3.zero;
        Invoke("ActivateNextPlayer", 2f);
    }

    private void ActivateNextPlayer()
    {
        if (Menu.player == 1) GameManager.currentTurn = GameManager.PlayersName[0];
        else
        {
            if (!GameOver.isSharpnerHitEnd)
            {
                if (GameManager.turn < (GameManager.PlayersName.Count - 1)) GameManager.turn++;
                else GameManager.turn = 0;
                GameManager.currentTurn = GameManager.PlayersName[GameManager.turn];
            }
        }
        isSharpnerFire = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Sharpner") && isCollisionEffectAllow)
        {
            slidePS.Stop();
            isCollisionEffectAllow = false;
            Destroy(Instantiate(collisionEffect, collision.GetContact(0).point, Quaternion.identity), 0.4f);
            ShakeIt();
           // Invoke("AllowCollisionEffect", 1);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isCollisionEffectAllow = true;
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