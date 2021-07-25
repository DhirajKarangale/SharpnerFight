using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] Transform directionLine;
    [SerializeField] Transform forcePoint;
    [SerializeField] AudioSource slideSound;
    private Rigidbody2D rigidBody;
    [SerializeField] Transform playerSharpner;
    private float scale;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameOver.isGameOver) rigidBody.velocity = Vector2.zero;

        if ((Menu.player == 1) && (GameManager.currentTurn == GameManager.PlayersName[0]) && !GameOver.isGameOver)
        {
            Vector3 rondomTargetPos;
            switch (Menu.difficulty)
            {
                case 1:
                    float dis = Vector2.Distance(transform.position, transform.position);
                    if (dis >= 5)
                    {
                        rondomTargetPos = new Vector3(playerSharpner.position.y, playerSharpner.position.z, Random.Range(playerSharpner.position.x - 200, playerSharpner.position.x + 200));
                        scale = Random.Range(1, 5f);
                    }
                    else if (dis >= 2)
                    {
                        rondomTargetPos = new Vector3(playerSharpner.position.y, playerSharpner.position.z, Random.Range(playerSharpner.position.x - 2, playerSharpner.position.x + 2));
                        scale = Random.Range(1, 3f);
                    }
                    else
                    {
                        scale = 2.5f;
                        rondomTargetPos = playerSharpner.position;
                    }
                    break;

                case 2:
                    rondomTargetPos = playerSharpner.position;
                    scale = Random.Range(1.2f, 4f);
                    break;

                default:
                    rondomTargetPos = playerSharpner.position;
                    scale = 4.5f;
                    break;
            }

            Vector3 dir = rondomTargetPos - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

            directionLine.localScale = new Vector3(scale * 0.3f, scale * 0.3f, scale * 0.3f);

            Invoke("AddForceComSharpner", 0.5f);

        }
    }

    private void AddForceComSharpner()
    {
        GameManager.currentTurn = "None";
        slideSound.Play();
        rigidBody.AddForce(new Vector3(forcePoint.position.x - transform.position.x, forcePoint.position.y - transform.position.y, 0) * scale * 40);
        directionLine.localScale = Vector3.zero;
        Invoke("ActivateTurn1", 1.5f);
    }

    private void ActivateTurn1()
    {
        GameManager.currentTurn = GameManager.PlayersName[1];
    }
}
