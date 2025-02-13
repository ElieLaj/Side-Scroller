using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    public float rotationSpeed = 100f; // Vitesse de rotation ajustable
    public Vector3 rotationAxis = new Vector3(0, 1, 0); // Axe de rotation (Y par défaut)

    void Update()
    {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}
