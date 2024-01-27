using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pie : MonoBehaviour
{
    [SerializeField]
    private Rigidbody myRig;

    [SerializeField]
    private GameObject piGO,pieDestruction,pieParticle;

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
        if (isCollided || SpawnablePie.isGameOver) return;

        SelfDestruction();
    }

    void SelfDestruction()
    {
        isCollided = true;
        piGO.SetActive(false);
        pieDestruction.SetActive(true);
        StartCoroutine(DisableAfterWhile());
        Destroy(Instantiate(pieParticle, transform.position, Quaternion.identity), 1.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCollided || SpawnablePie.isGameOver) return;

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
