using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float movementSpeed = 6f;
    [SerializeField] float jumpForce = 5f;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;

    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isDead)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);

            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isDead)
        {
            if (collision.gameObject.CompareTag("Enemy Side One") ||
                collision.gameObject.CompareTag("Enemy Side Two") ||
                collision.gameObject.CompareTag("Enemy Side Three") ||
                collision.gameObject.CompareTag("Enemy Side Four"))
            {
                Die();
            }
        }
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, .1f, ground);
    }

    void Die()
    {
        isDead = true;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

        // Disable player movement script
        GetComponent<PlayerMovement>().enabled = false;

        Invoke(nameof(ReloadLevel), 1.3f);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}