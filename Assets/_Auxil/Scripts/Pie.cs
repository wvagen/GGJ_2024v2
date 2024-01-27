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

    void DisableColliders()
    {
        foreach (Collider col in GetComponentsInChildren<Collider>())
        {
            col.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        piGO.SetActive(false);
        pieDestruction.SetActive(true);
        StartCoroutine(DisableAfterWhile());
    }

    IEnumerator DisableAfterWhile()
    {
        yield return new WaitForSeconds(.5f);
        DisableColliders();
        Destroy(gameObject, 2);
    }
}
