using UnityEngine;

public class CancelRotation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentRotation = transform.localEulerAngles;

        transform.localEulerAngles = new Vector3(0, 0, 0);
    }
}
