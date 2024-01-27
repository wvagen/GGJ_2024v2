using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pie : MonoBehaviour
{
    [SerializeField]
    private Rigidbody myRig;

    [SerializeField]
    private GameObject piGO,pieDestruction;

    private void Start()
    {
        Debug.Log("Pie: " + transform.position);
    }

    public void SetMe(float speed)
    {
        myRig.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        piGO.SetActive(false);
        pieDestruction.SetActive(true);
    }
}
