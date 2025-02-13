using UnityEngine;

public class CancelMovement : MonoBehaviour
{
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
            
        }
    }
}
