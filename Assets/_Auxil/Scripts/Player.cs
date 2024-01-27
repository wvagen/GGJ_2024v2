using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody myRig;
    [SerializeField]
    private float moveSpeed;

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
        //transform.Translate(Vector2.right * Time.deltaTime * moveX * moveSpeed);
    }
}
