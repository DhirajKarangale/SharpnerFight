using UnityEngine;
using UnityEngine.UI;

public class SharnerController : MonoBehaviour
{
    [SerializeField] GameObject redSharpner,blueSharpner;
    [SerializeField] Transform redDirectionLine, blueDirectionLine;
    [SerializeField] Transform redForcePoint, blueForcePoint;
    private bool redSharpnerHit, blueSharpnerHit;
    private RaycastHit2D hit;
    private float scale;
    private byte turn;
    private float timer;
    [SerializeField] Text playerText;
    [SerializeField] Text timerText;


    private void Start()
    {
        Ground.isGameOver = false;
        turn = 1;
        timer = 10;
        redSharpnerHit = false;
        blueSharpnerHit = false;
        redDirectionLine.localScale = Vector3.zero;
        blueDirectionLine.localScale = Vector3.zero;
    }

    private void Update()
    {
        if((turn == 1) && !Ground.isGameOver)
        {
            playerText.color = Color.red;
            playerText.text = "Player 1";
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                timerText.text = timer.ToString();
            }
            else
            {
                timer = 10;
                turn = 2;
            }
        }
        if ((turn == 2) && !Ground.isGameOver)
        {
            playerText.color = Color.blue;
            playerText.text = "Player 2";
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                timerText.text = timer.ToString();
            }
            else
            {
                timer = 10;
                turn = 1;
            }
        }

        if (Ground.isGameOver)
        {
            blueSharpner.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            redSharpner.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        if (Input.GetMouseButton(0))
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
            if (hit.collider)
            {
                if ((hit.transform.name == "Blue") && (turn == 2) && !Ground.isGameOver)
                {
                    blueSharpnerHit = true;
                }

                if ((hit.transform.name == "Red") && (turn == 1) && !Ground.isGameOver)
                {
                    redSharpnerHit = true;
                }
               
                Debug.Log(hit.transform.name);
                if (redSharpnerHit)
                {
                    if(hit.point.x < redSharpner.transform.position.x) redSharpner.transform.localRotation = Quaternion.Euler(redDirectionLine.localRotation.x, redDirectionLine.localRotation.y, -hit.point.y * 20 - 90);
                    else if (hit.point.x > redSharpner.transform.position.x) redSharpner.transform.localRotation = Quaternion.Euler(redDirectionLine.localRotation.x, redDirectionLine.localRotation.y, hit.point.y * 20 + 90);

                    redDirectionLine.localRotation = Quaternion.Euler(redDirectionLine.localRotation.x, redDirectionLine.localRotation.y, -hit.point.y * 5);
                    scale = Vector2.Distance(redSharpner.transform.position, hit.point) * 1.5f;
                    scale = Mathf.Clamp(scale, 0, 5);
                    redDirectionLine.localScale = new Vector3(scale, scale, scale);
                }
                if(blueSharpnerHit)
                {
                    if (hit.point.x < blueSharpner.transform.position.x) blueSharpner.transform.localRotation = Quaternion.Euler(blueDirectionLine.localRotation.x, redDirectionLine.localRotation.y, -hit.point.y * 20 + 90);
                    else if (hit.point.x > blueSharpner.transform.position.x) blueSharpner.transform.localRotation = Quaternion.Euler(blueDirectionLine.localRotation.x, redDirectionLine.localRotation.y, hit.point.y * 20 - 90);

                    blueDirectionLine.localRotation = Quaternion.Euler(blueDirectionLine.localRotation.x, blueDirectionLine.localRotation.y, -hit.point.y * 5);
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
                redSharpner.GetComponent<Rigidbody2D>().AddForce(new Vector3(redForcePoint.position.x - redSharpner.transform.position.x, redForcePoint.position.y - redSharpner.transform.position.y, 0) * 75 * scale);
                redSharpnerHit = false;
                redDirectionLine.localScale = Vector3.zero;
                timer = 10;
                turn = 2;
            }
            if(turn == 2 && blueSharpnerHit)
            {
                blueSharpner.GetComponent<Rigidbody2D>().AddForce(new Vector3(blueForcePoint.position.x - blueSharpner.transform.position.x, blueForcePoint.position.y - blueSharpner.transform.position.y, 0) * 75 * scale);
                blueSharpnerHit = false;
                blueDirectionLine.localScale = Vector3.zero;
                timer = 10;
                turn = 1;
            }
        }
    }
}
