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

    [Range(0.01f, 1.0f)]
    public float shakeDuration = 0.3f;

    [Range(0, 1.0f)]
    public float shakeAmount = 0.7f;

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

            StartCoroutine(ShakeCamera());
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    IEnumerator ShakeCamera()
    {
        float elapsed = 0f;

        Vector3 originalCamPos = cameraTransform.localPosition;

        while (elapsed < shakeDuration)
        {
            elapsed += Time.deltaTime;

            float percentComplete = elapsed / shakeDuration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= shakeAmount * damper;
            y *= shakeAmount * damper;

            cameraTransform.localPosition = new Vector3(x, y, originalCamPos.z);

            yield return null;
        }

        cameraTransform.localPosition = originalCamPos;
    }

}
