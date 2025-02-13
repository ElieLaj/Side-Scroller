using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public GameManager gameManager;
    public float jumpForce = 10f;
    private bool isGrounded = true;
    private int jumpCount = 0;
    private int spamSpace = 0;
    private float lastSpacePressTime = 0f;
    private const float spamTimeLimit = 2f; // 2 secondes pour spammer 20 fois
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float currentTime = Time.time;

            // Vérifier si les 20 pressions ont été faites rapidement
            if (currentTime - lastSpacePressTime > spamTimeLimit)
            {
                spamSpace = 0; // Reset si trop lent
            }

            spamSpace++;
            lastSpacePressTime = currentTime;

            Debug.Log($"Spam: {spamSpace}");

            if (spamSpace >= 20 && jumpCount == 1)
            {
                Debug.Log("DOUBLE JUMP ACTIVATED!");
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpCount++;
                spamSpace = 0;
            }
            else if (isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
                jumpCount = 1;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        gameManager.HandleCollision(gameObject, collision);

        isGrounded = true;
        jumpCount = 0;
    }

    private void OnTriggerEnter(Collider collider)
    {
        gameManager.HandleTrigger(gameObject, collider);
    }
}
