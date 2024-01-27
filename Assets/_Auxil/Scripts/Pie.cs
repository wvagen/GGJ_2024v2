using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pie : MonoBehaviour
{
    [SerializeField]
    private Rigidbody myRig;

    [SerializeField]
    private GameObject piGO,pieDestruction;

    private SpawnablePie _man;

    bool isCollided = false;

    public void SetMe(SpawnablePie _man, float speed)
    {
        myRig.velocity = transform.forward * speed;
        this._man = _man;
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
        if (isCollided) return;

        SelfDestruction();
    }

    void SelfDestruction()
    {
        isCollided = true;
        piGO.SetActive(false);
        pieDestruction.SetActive(true);
        StartCoroutine(DisableAfterWhile());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCollided) return;

        if (other.gameObject.tag == "head")
        {
            //Score ++;
            _man.IncScore();
        }

        SelfDestruction();
    }

    IEnumerator DisableAfterWhile()
    {
        yield return new WaitForSeconds(.5f);
        DisableColliders();
        Destroy(gameObject, 2);
    }
}
