using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnablePieUI : MonoBehaviour
{
    [SerializeField]
    private Image coreImg, zoneImg;

    [SerializeField]
    private GameObject zoneGO;

    [SerializeField]
    private Color dangerCol, greenCol;

    float closeSpeed;

    public void SetMe(float closeSpeed)
    {
        this.closeSpeed = closeSpeed;
        zoneImg.color = dangerCol;
        coreImg.color = dangerCol;
    }

    private void Update()
    {
        if (zoneGO.transform.localScale.x > 0)
        {
            zoneGO.transform.localScale -= Vector3.one * closeSpeed * Time.deltaTime;
        }

        if (zoneGO.transform.localScale.x <= 0.5f)
        {
            zoneImg.color = greenCol;
            coreImg.color = greenCol;
        }

    }


}
