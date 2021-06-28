using UnityEngine;
using UnityEngine.UI;

public class SharnerController : MonoBehaviour
{
    [SerializeField] GameObject redSharpner,blueSharpner;
    [SerializeField] Transform redDirectionLine, blueDirectionLine;
    [SerializeField] Transform redForcePoint, blueForcePoint;
    [SerializeField] AudioSource slideSound;
    private bool redSharpnerHit, blueSharpnerHit;
    private RaycastHit2D hit;
    private float scale;
    private byte turn;
    private float timer;
    [SerializeField] Text playerText;
    [SerializeField] Text debugText;
    [SerializeField] Text singlePlayerText;
    [SerializeField] Text difficultyText;
    [SerializeField] GameObject timerText;


    private void Start()
    {
        if(Menu.player == 1)
        {
            difficultyText.gameObject.SetActive(true);
            singlePlayerText.text = "Single Player";
            if (Menu.difficulty == 1) difficultyText.text = "Easy";
            if (Menu.difficulty == 2) difficultyText.text = "Medium";
            if (Menu.difficulty == 3) difficultyText.text = "Hard";
        }
        else
        {
            singlePlayerText.text = "Two Player";
            difficultyText.gameObject.SetActive(false);
        }
        GameManager.isGameOver = false;
        turn = 1;
        timer = 10;
        redSharpnerHit = false;
        blueSharpnerHit = false;
        redDirectionLine.localScale = Vector3.zero;
        blueDirectionLine.localScale = Vector3.zero;
    }

    private void Update()
    {
        debugText.text = Camera.main.orthographicSize.ToString();
        if((turn == 1) && !GameManager.isGameOver && !redSharpnerHit)
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
                timerText.SetActive(false);
                playerText.gameObject.SetActive(false);
                timer = 10;
                if (Menu.player == 1)
                {
                    timerText.SetActive(false);
                    playerText.color = Color.blue;
                    playerText.text = "Com"; turn = 100;
                }
                else turn = 2;
            }
        }
        if((turn == 2) && !GameManager.isGameOver && !blueSharpnerHit && (Menu.player == 2))
        {
            timerText.SetActive(true);
            playerText.gameObject.SetActive(true);
            playerText.color = Color.blue;
            playerText.text = "Player 2";
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                timerText.GetComponent<Text>().text = timer.ToString();
            }
            else
            {
                timer = 10;
                turn = 1;
            }
        }

        if (GameManager.isGameOver)
        {
            blueSharpner.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            redSharpner.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if ((turn == 100) && !GameManager.isGameOver)
        {
            blueSharpnerHit = false;
            Vector3 rondomTargetPos;

            if(Menu.difficulty == 1)
            {
                rondomTargetPos = new Vector3(Random.Range(redSharpner.transform.position.x - 3, redSharpner.transform.position.x + 3), redSharpner.transform.position.y, redSharpner.transform.position.z);
                scale = Random.Range(1, 2.8f);
            }
            else if (Menu.difficulty == 2)
            {
                rondomTargetPos = redSharpner.transform.position;
                scale = Random.Range(1.2f, 4f);
            }
            else
            {
                rondomTargetPos = redSharpner.transform.position;
                scale = 4.5f;
            }

            Vector3 dir = rondomTargetPos - blueSharpner.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            blueSharpner.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

            blueDirectionLine.localScale = new Vector3(scale * 10, scale * 10, scale * 10);

            Invoke("AddForceComSharpner", 0.5f);
        }
        if (Input.GetMouseButton(0) && !GameManager.isGameOver)
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
            if (hit.collider)
            {
                if ((hit.transform.name == "Red") && (turn == 1)) redSharpnerHit = true;
                else if ((hit.transform.name == "Blue") && (turn == 2) && (Menu.player == 2)) blueSharpnerHit = true;
                if(redSharpnerHit)
                {
                    timerText.SetActive(false);
                    playerText.gameObject.SetActive(false);

                    var dir = hit.point - new Vector2(redSharpner.transform.position.x,redSharpner.transform.position.y);
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    redSharpner.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

                    scale = Vector2.Distance(redSharpner.transform.position, hit.point) * 1.5f;
                    scale = Mathf.Clamp(scale, 0, 5);
                    redDirectionLine.localScale = new Vector3(scale, scale, scale);
                }
                if(blueSharpnerHit)
                {
                    timerText.SetActive(false);
                    playerText.gameObject.SetActive(false);

                    var dir = -hit.point + new Vector2(blueSharpner.transform.position.x, blueSharpner.transform.position.y);
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    blueSharpner.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

                    scale = Vector2.Distance(blueSharpner.transform.position, hit.point) * 1.5f;
                    scale = Mathf.Clamp(scale, 0, 5);
                    blueDirectionLine.localScale = new Vector3(scale * 10, scale * 10, scale * 10);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (turn == 1 && redSharpnerHit)
            {
                timer = 10;
                turn = 0;
                slideSound.Play();
                redSharpner.GetComponent<Rigidbody2D>().AddForce(new Vector3(redForcePoint.position.x - redSharpner.transform.position.x, redForcePoint.position.y - redSharpner.transform.position.y, 0) * 65 * scale);
                redSharpnerHit = false;
                redDirectionLine.localScale = Vector3.zero;

                if(Menu.player == 1) Invoke("ActivateTurn100", 2);
                else Invoke("ActivateTurn2", 1);
            }
            if (turn == 2 && blueSharpnerHit)
            {
                timer = 10;
                turn = 0;
                blueSharpnerHit = false;
                slideSound.Play();
                blueSharpner.GetComponent<Rigidbody2D>().AddForce(new Vector3(blueForcePoint.position.x - blueSharpner.transform.position.x, blueForcePoint.position.y - blueSharpner.transform.position.y, 0) * 65 * scale);
                blueDirectionLine.localScale = Vector3.zero;
                Invoke("ActivateTurn1", 1);
            }
        }
    }

    private void AddForceComSharpner()
    {
        turn = 0;
        slideSound.Play();
        blueSharpner.GetComponent<Rigidbody2D>().AddForce(new Vector3(blueForcePoint.position.x - blueSharpner.transform.position.x, blueForcePoint.position.y - blueSharpner.transform.position.y, 0) * scale * 3.8f);
        blueDirectionLine.localScale = Vector3.zero;
        Invoke("ActivateTurn1", 1);
    }

    private void ActivateTurn1()
    {
        turn = 1;
    }
    private void ActivateTurn2()
    {
        turn = 2;
    }
    private void ActivateTurn100()
    {
        turn = 100;
    }
}
