using UnityEngine;

public class SegmentSlither : MonoBehaviour
{
    [Header("Slither Settings")]
    public float maxOffset = 0.5f;   
    public float slitherSpeed = 5f; 
    public Vector3 slitherDirection = Vector3.right; 

    private Vector3 basePosition;
    private Vector3 lastPosition;
    private float slitherTimer = 0f;

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
       
            slitherTimer += delta.magnitude * slitherSpeed;

         
            float offset = Mathf.Sin(slitherTimer) * maxOffset;

           
            transform.localPosition = basePosition + slitherDirection.normalized * offset;
        }
        else
        {
        
            transform.localPosition = Vector3.Lerp(transform.localPosition, basePosition, Time.deltaTime * 5f);
        }

        lastPosition = transform.position;
    }
}
