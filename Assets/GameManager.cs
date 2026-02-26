using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Assign")]
    public GameObject foodPrefab;      
    public Transform player;
    [Header("Food Reset")]
    public Transform foodContainer;

    [Header("Options")]
    public Transform foodParent;      
    private List<Vector3> initialFoodPositions = new List<Vector3>();
    private List<Quaternion> initialFoodRotations = new List<Quaternion>();

    private Vector3 playerStartPos;
    private Quaternion playerStartRot;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {

        if (player == null)
        {
            var pgo = GameObject.FindGameObjectWithTag("Player");
            if (pgo != null) player = pgo.transform;
        }

        if (player != null)
        {
            playerStartPos = player.position;
            playerStartRot = player.rotation;
        }

        initialFoodPositions.Clear();
        initialFoodRotations.Clear();

        var foods = FindObjectsOfType<WormFood>();
        foreach (var f in foods)
        {
            initialFoodPositions.Add(f.transform.position);
            initialFoodRotations.Add(f.transform.rotation);
        }
    }

  
    public void HandlePlayerCaught()
    {
      
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.ResetScore();
        foreach (var e in FindObjectsOfType<EnemySpawnReset>())
        {
            e.ResetToSpawn();
        }
        foreach (Transform food in foodContainer)
        {
            food.gameObject.SetActive(true);

      
            WormFood wf = food.GetComponent<WormFood>();
            if (wf != null)
            {
                wf.SendMessage("ResetFood", SendMessageOptions.DontRequireReceiver);
            }
        }


        if (UIManager.Instance != null && UIManager.Instance.startMenuPanel != null)
        {
            UIManager.Instance.startMenuPanel.SetActive(true);
        }
        Time.timeScale = 0f;

    
        RespawnAllFood();

        
        if (player != null)
        {
            player.position = playerStartPos;
            player.rotation = playerStartRot;

            var rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }

    private void RespawnAllFood()
    {
        if (foodPrefab == null)
        {
            Debug.LogWarning("GameManager: foodPrefab is not assigned. Cannot respawn food.");
            return;
        }

        
        var existing = FindObjectsOfType<WormFood>();
        foreach (var e in existing)
        {
            Destroy(e.gameObject);
        }


        for (int i = 0; i < initialFoodPositions.Count; i++)
        {
            Vector3 pos = initialFoodPositions[i];
            Quaternion rot = initialFoodRotations[i];
            GameObject go = Instantiate(foodPrefab, pos, rot);
            if (foodParent != null) go.transform.SetParent(foodParent, true);
        }
    }
}
