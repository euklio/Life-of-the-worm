using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Panels")]
    public GameObject startMenuPanel;

    private void Awake()
    {
      
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }


        if (Object.FindFirstObjectByType<EventSystem>() == null)
        {
            GameObject es = new GameObject("EventSystem");
            es.AddComponent<EventSystem>();
            es.AddComponent<StandaloneInputModule>();
        }
    }

    private void Start()
    {
    
        Time.timeScale = 0f;

       
        if (startMenuPanel != null)
            startMenuPanel.SetActive(true);
        else
            Debug.LogWarning("Start Menu Panel is not assigned in UIManager!");
    }

    public void OnStartButtonClicked()
    {
        foreach (var e in FindObjectsOfType<EnemyChase>())
            e.SendMessage("ResetCatch", SendMessageOptions.DontRequireReceiver);

        if (startMenuPanel != null)
            startMenuPanel.SetActive(false);

      
        Time.timeScale = 1f;
    }
}
