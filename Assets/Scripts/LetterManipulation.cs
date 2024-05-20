using System.Diagnostics.Tracing;
using UnityEngine;

public class LetterManipulation : MonoBehaviour
{
    public float timeBeforeFall;
    public Vector3 endScale;
    public float stepSize;
    float transitionSpeed = 0.25f;
    float startTime;
    Rigidbody rb;
    Collider prefabCollider;
    private float previousY;
    private int floorCounter;
    void Awake()
    {
        startTime = Time.time;
        rb = GetComponent<Rigidbody>();
        prefabCollider = rb.GetComponent<Collider>();
        previousY = transform.position.y;
    }

    void Update()
    {
        float elapsedTime = Time.time - startTime;

        if (elapsedTime >= timeBeforeFall || Input.GetKeyDown(KeyCode.Space)) {
            if (rb.isKinematic)
                rb.isKinematic = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "BuildingFloor" && floorCounter < 3)
        {
            floorCounter++;
            if (floorCounter >= 3)
                prefabCollider.material = null;
        }    
        transform.localScale = Vector3.Max(transform.localScale - new Vector3(stepSize,stepSize,0), endScale);
    }
}
