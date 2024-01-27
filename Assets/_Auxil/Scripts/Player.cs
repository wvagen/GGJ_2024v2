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
    private float rotSpeed;

    [SerializeField]
    private Animator myAnim;

    [SerializeField]
    private Transform Hip;

    [SerializeField]
    private Camera mainCam;

    public float offsetValue;

    Vector3 hipInitRot;

    // Start is called before the first frame update
    void Start()
    {
        hipInitRot = Hip.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        LookTowards();
    }

    void GetInput()
    {
        float moveX = -Input.GetAxis("Horizontal");
        myRig.velocity = transform.right * moveX * moveSpeed;
        Vector2 moveDirection = new Vector2(moveX, 0);
        float magnitude = Mathf.Clamp01(moveDirection.magnitude);
        //transform.localScale = moveX > 0 ? Vector3.one : new Vector3(-1, 1, 1);
        //myAnim.speed = moveX > 0 ? 1 : -1;

        myAnim.SetFloat("Blend", magnitude);

        //transform.Translate(Vector2.right * Time.deltaTime * moveX * moveSpeed);
    }

    void LookTowards()
    {
        //Vector3 mousePosition = Input.mousePosition;

        //// Convert the mouse position to a point in the game world
        //Vector3 worldMousePosition = mainCam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));

        ////// Make the object look towards the mouse position
        //Hip.LookAt(worldMousePosition, Vector3.up);
        //Hip.localEulerAngles = new Vector3(hipInitRot.x, Hip.eulerAngles.y + offsetValue, hipInitRot.z);

        // Vector2 normolizedVec = (Hip.transform.position - worldMousePosition).normalized;
        //// Debug.Log(normolizedVec);
        // Debug.Log(Mathf.Acos(worldMousePosition.normalized.y) * Mathf.Rad2Deg);
        // Debug.Log(Mathf.Acos(normolizedVec.y) * Mathf.Rad2Deg);
        // Debug.DrawLine(Hip.position, worldMousePosition, Color.red);

        float moveY = -Input.GetAxis("Vertical");
        Hip.Rotate(Vector3.up * Time.deltaTime * rotSpeed * moveY);
    }
}
