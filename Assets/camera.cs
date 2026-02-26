using UnityEngine;

public class CameraTiltOnSpace : MonoBehaviour
{
    public float upAngle = -60f;          // negative = look up
    public float boostedFarClip = 3000f;  // view distance when tilted

    private bool tilted = false;
    private float originalFarClip;
    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
        originalFarClip = cam.farClipPlane;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tilted = !tilted;

            if (tilted)
            {
                // Tilt camera up
                transform.rotation = Quaternion.Euler(upAngle, transform.eulerAngles.y, 0f);

                // Increase view distance
                cam.farClipPlane = boostedFarClip;
            }
            else
            {
                // Reset camera rotation
                transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);

                // Restore original view distance
                cam.farClipPlane = originalFarClip;
            }
        }
    }
}
