using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpForce = 10.0f;
    public float jumpDelay = 1.0f;

    private Rigidbody rb;
    private bool isGrounded;
    private float lastJumpTime;

    public Transform cameraTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 movement = forward * vertical + right * horizontal;
        transform.position += movement * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && Time.time > lastJumpTime + jumpDelay)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            lastJumpTime = Time.time;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rbdy = collision.gameObject.GetComponent<Rigidbody>();

            //Stop Moving/Translating
            rbdy.velocity = Vector3.zero;

            //Stop rotating
            rbdy.angularVelocity = Vector3.zero;
        }
    }
}
