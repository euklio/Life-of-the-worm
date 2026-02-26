using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    private bool hasCaughtPlayer = false;
    public Transform player;  
    public float speed = 3f;
    public float catchDistance = 4f;  

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;  
    }

    void Update()
    {
        if (player == null) return;


        transform.LookAt(player);
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        if (!hasCaughtPlayer && Vector3.Distance(transform.position, player.position) < catchDistance)
        {
            hasCaughtPlayer = true;

            if (GameManager.Instance != null)
                GameManager.Instance.HandlePlayerCaught();
            else
                Debug.Log("Caught the player!");
        }

    }
    void ResetCatch()
    {
        hasCaughtPlayer = false;
    }
}