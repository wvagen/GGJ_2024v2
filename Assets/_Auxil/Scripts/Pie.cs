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

    [SerializeField]
    private Collider[] cols;
    [SerializeField]
    private BoxCollider[] boxcols;

    bool isCollided = false;

    //void FixedUpdate()
    //{
    //    // Bit shift the index of the layer (8) to get a bit mask
    //    int layerMask = 1 << 8;

    //    // This would cast rays only against colliders in layer 8.
    //    // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
    //    layerMask = ~layerMask;

    //    RaycastHit hit;
    //    // Does the ray intersect any objects excluding the player layer
    //    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1, layerMask))
    //    {
    //        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
    //        Debug.Log("Did Hit " + hit.collider.name);
    //    }
    //    else
    //    {
    //        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
    //        Debug.Log("Did not Hit");
    //    }
    //}

    public void SetMe(SpawnablePie _man, float speed)
    {
        myRig.velocity = transform.forward * speed;
        this._man = _man;
    }

    void DisableColliders()
    {
        foreach (var item in cols)
        {
            item.enabled = false;
        }

        foreach (var item in boxcols)
        {
            item.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isCollided || SpawnablePie.isGameOver) return;

        _man.DecScore();
        SelfDestruction();
    }

    void SelfDestruction()
    {
        isCollided = true;
        DisableColliders();
        piGO.SetActive(false);
        pieDestruction.SetActive(true);
        StartCoroutine(DisableAfterWhile());
        Game_Over_2_AudioManager.audioManInstance.Play_Sfx("Splat");
        Destroy(Instantiate(pieParticle, transform.position, Quaternion.identity), 1.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCollided || SpawnablePie.isGameOver) return;

        Debug.Log(other.gameObject.tag);

        if (other.gameObject.tag == "head")
        {
            //Score ++;
            _man.IncScore();
        }

        else if (other.gameObject.tag == "player")
        {
            //Score ++;
            _man.DecScore();
        }

        SelfDestruction();
    }

    IEnumerator DisableAfterWhile()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject, 2);
    }
}
