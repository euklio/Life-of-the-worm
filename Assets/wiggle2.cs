using UnityEngine;

public class SegmentSlitherReverse : MonoBehaviour
{
    [Header("Slither Settings")]
    public float maxOffset = 0.5f;   // max distance left/right
    public float slitherSpeed = 5f;  // speed of side-to-side movement
    public Vector3 slitherDirection = Vector3.right; // sideways direction

    private Vector3 basePosition;
    private Vector3 lastPosition;

    // Start opposite direction by shifting sine phase
    private float slitherTimer = Mathf.PI;

    void Start()
    {
        basePosition = transform.localPosition;
        lastPosition = transform.position;
    }

    void Update()
    {
        Vector3 delta = transform.position - lastPosition;

        if (delta.magnitude > 0.001f)
        {
            // Advance slither based on movement distance
            slitherTimer += delta.magnitude * slitherSpeed;

            float offset = Mathf.Sin(slitherTimer) * maxOffset;

            transform.localPosition =
                basePosition + slitherDirection.normalized * offset;
        }
        else
        {
            // Return smoothly when not moving
            transform.localPosition =
                Vector3.Lerp(transform.localPosition, basePosition, Time.deltaTime * 5f);
        }

        lastPosition = transform.position;
    }
}
