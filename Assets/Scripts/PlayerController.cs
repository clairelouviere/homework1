using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    public bool isGrounded;
    int jumps;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
      Vector2 movementVector = movementValue.Get<Vector2>();
      movementX = movementVector.x;
      movementY = movementVector.y;
    }

    void SetCountText()
    {
      countText.text = "Count: " + count.ToString();
      if(count >= 12)
      {
        winTextObject.SetActive(true);
      }
    }

    void FixedUpdate()
    {

      if(rb.transform.position.y <= 0.5f){
        isGrounded = true;
        jumps = 2;
      }
      if(Input.GetKeyDown("space") && (jumps > 0 || isGrounded)){
        Vector3 jump = new Vector3(0.0f, 300.0f, 0.0f);
        rb.AddForce(jump);
        isGrounded = false;
        jumps -= 1;
      }
      // y value is 0 for (x,y) plane movement
      Vector3 movement = new Vector3(movementX, 0.0f, movementY);
      rb.AddForce(movement * speed);
  }

    private void OnTriggerEnter(Collider other)
    {
      if (other.gameObject.CompareTag("PickUp"))
      {
        other.gameObject.SetActive(false);
        count = count + 1;

        SetCountText();
      }
    }
}
