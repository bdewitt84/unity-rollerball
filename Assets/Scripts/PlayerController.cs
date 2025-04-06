using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;            // horizontal movement
    private float movementY;            // horizontal movement
    private int count;                  // Number of collectibles retrieved
    private int jumpsLeft = 0;          // Number of jumps remaining before
                                        // touching the ground

    public float speed = 0.0f;          // horizontal movement speed
    public float jumpForce = 5.0f;      // Upward thrust when jumping
    public int maxJumps = 2;            // Number of jumps allowed before
                                        // touching the ground again
    public TextMeshProUGUI countText;   // Number of collectibles retrieved
    public GameObject winTextObject;    // Victory message to display when all
                                        // collectibles retrieved

    // Start is called once before the first execution of Update after the
    // MonoBehaviour is created
    void Start()
    {
        winTextObject.SetActive(false);
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
    }

    // Applies horizontal force to player by factor of 'speed'
    private void FixedUpdate() 
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    // Checks input and assigns horizontal movement values accordingly
    void OnMove (InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // Double-jump checks to see if any jumps are left. If so, applies force
    // upward based on public int jumpForce, and decrements jumpsLeft by one.
    // Otherwise, does nothing.
    void OnJump(InputValue jumpValue)
    {
        if (jumpsLeft > 0)
        {
            Vector3 jumpVec = new Vector3(0.0f, jumpForce, 0.0f);

            rb.AddForce(jumpVec, ForceMode.Impulse);

            jumpsLeft -= 1;
        }
    }

    // Updates the count text on the UI canvas when called. If the count
    // reaches 8, displays victory text.
    void SetCountText() 
    {
        countText.text =  "Count: " + count.ToString();

        if (count >= 8)
        {
            winTextObject.SetActive(true);
        }
    }

    // Checks whether the player has touched the ground. If so, resets
    // jumpsLeft to maxJumps.
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpsLeft = maxJumps;
        }
    }

    // Checks whether the player has touched a collectible. If so, increments
    // the counter, deactivates the collectible, and calls SetCountText.
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
