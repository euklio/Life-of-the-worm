using UnityEngine;

public class WormFood : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip eatSound;

    public float consumeDuration = 2f;
    public Texture[] consumeStages;

    private Renderer rend;
    private float timer;
    private bool eating;

    void Awake()
    {
        rend = GetComponent<Renderer>();


        if (consumeStages != null && consumeStages.Length > 0)
        {
            rend.material.mainTexture = consumeStages[0];
        }
    }

    void Update()
    {
        if (!eating) return;

        timer += Time.deltaTime;

        if (consumeStages == null || consumeStages.Length == 0)
        {
            if (timer >= consumeDuration)
                Destroy(gameObject);
                ScoreManager.Instance.AddScore(1);
            return;
        }

        float t = Mathf.Clamp01(timer / consumeDuration);

        int stage = Mathf.Min(
            Mathf.FloorToInt(t * consumeStages.Length),
            consumeStages.Length - 1
        );

        rend.material.mainTexture = consumeStages[stage];

        if (timer >= consumeDuration)
        {
            ScoreManager.Instance.AddScore(1);


            if (eatSound != null && muzika.Instance != null)
            {
                muzika.Instance.PlayOneShot(eatSound);
            }

            Destroy(gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Worm"))
        {
            eating = true;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Worm"))
        {
            eating = false;
        }
    }
    public void ResetFood()
    {
        timer = 0f;
        eating = false;

        if (consumeStages != null && consumeStages.Length > 0)
        {
            rend.material.mainTexture = consumeStages[0];
        }
    }
}
