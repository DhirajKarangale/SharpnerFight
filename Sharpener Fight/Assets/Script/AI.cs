using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] Transform directionLine;
    [SerializeField] Transform forcePoint;
    [SerializeField] AudioSource slideSound;
    public GameObject lightObj;
    public Rigidbody2D rigidBody;
    public ParticleSystem slidePS;
    private float scale;
    public static bool playerturnFromAI;

    private void Start()
    {
        playerturnFromAI = true;
    }

    private void Update()
    {
        if (GameOver.isGameOver) rigidBody.velocity = Vector2.zero;

        if ((Menu.player == 1) && (GameManager.currentTurn == GameManager.PlayersName[0]) && !GameOver.isGameOver)
        {
            AIController(); 
        }
    }

    private void AIController()
    {
        lightObj.SetActive(true);

        Vector3 dir = Difficuilty() - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

        directionLine.localScale = new Vector3(scale * 0.3f, scale * 0.3f, scale * 0.3f);

        Invoke("AddForceComSharpner", 0.5f);
    }

    private Vector3 Difficuilty()
    {
        switch (Menu.difficulty)
        {
            case 1:
                float dis = Vector2.Distance(transform.position, GameManager.instanstiatedPlayers[0].transform.position);
                if (dis >= 5)
                {
                    scale = Random.Range(2.3f, 3.8f);
                    return new Vector3(Random.Range(GameManager.instanstiatedPlayers[0].transform.position.x - 7, GameManager.instanstiatedPlayers[0].transform.position.x + 7), GameManager.instanstiatedPlayers[0].transform.position.y, GameManager.instanstiatedPlayers[0].transform.position.z);
                }
                else if (dis >= 2)
                {
                    scale = Random.Range(2, 3);
                    return new Vector3(Random.Range(GameManager.instanstiatedPlayers[0].transform.position.x - 2.5f, GameManager.instanstiatedPlayers[0].transform.position.x + 2.5f), GameManager.instanstiatedPlayers[0].transform.position.y, GameManager.instanstiatedPlayers[0].transform.position.z);
                }
                else
                {
                    scale = 2.2f;
                    return GameManager.instanstiatedPlayers[0].transform.position;
                }

            case 2:
                scale = Random.Range(1.6f, 4.1f);
                return GameManager.instanstiatedPlayers[0].transform.position;

            default:
                scale = 4.5f;
                return GameManager.instanstiatedPlayers[0].transform.position;
        }
    }

    private void AddForceComSharpner()
    {
        lightObj.SetActive(false);
        GameManager.currentTurn = "None";
        slideSound.Play();
        slidePS.Play();
        rigidBody.AddForce(new Vector3(forcePoint.position.x - transform.position.x, forcePoint.position.y - transform.position.y, 0) * scale * 40);
        directionLine.localScale = Vector3.zero;
        Invoke("ActivateTurn1", 1.5f);
    }

    private void ActivateTurn1()
    {
        GameManager.currentTurn = GameManager.PlayersName[1];
        playerturnFromAI = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        slidePS.Stop();
    }
}
