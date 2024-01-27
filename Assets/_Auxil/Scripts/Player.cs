using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody myRig;
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Animator myAnim;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        float moveX = -Input.GetAxis("Horizontal");
        myRig.velocity = transform.right * moveX * moveSpeed;
        Vector2 moveDirection = new Vector2(moveX, 0);
        float magnitude = Mathf.Clamp01(moveDirection.magnitude);
        transform.localScale = moveX > 0 ? Vector3.one : new Vector3(-1, 1, 1);

        myAnim.SetFloat("Blend", magnitude);

        //transform.Translate(Vector2.right * Time.deltaTime * moveX * moveSpeed);
    }
}
