using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnablePie : MonoBehaviour
{
    [SerializeField]
    private GameObject _Pie;

    public float forceSpeed;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("InvokePies", 0, 2);
    }

    void InvokePies()
    {
        Pie pieScript = Instantiate(_Pie).GetComponent<Pie>();
        pieScript.SetMe(forceSpeed);
    }
}
