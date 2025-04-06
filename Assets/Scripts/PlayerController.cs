using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int count;
    private int jumpsLeft = 0;

    public float speed = 0.0f;
    public float jumpForce = 5.0f;
    public int maxJumps = 2;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        winTextObject.SetActive(false);
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
    }

    private void FixedUpdate() 
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    void OnMove (InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump(InputValue jumpValue)
    {
        if (jumpsLeft > 0)
        {
            Vector3 jumpVec = new Vector3(0.0f, jumpForce, 0.0f);

            rb.AddForce(jumpVec, ForceMode.Impulse);

            jumpsLeft -= 1;
        }
    }

    void SetCountText() 
    {
        countText.text =  "Count: " + count.ToString();

        if (count >= 8)
        {
            winTextObject.SetActive(true);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpsLeft = maxJumps;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            count = count + 1;
        }
        SetCountText();
    }

}
