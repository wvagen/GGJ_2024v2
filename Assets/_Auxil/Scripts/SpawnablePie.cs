using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnablePie : MonoBehaviour
{
    [SerializeField]
    private GameObject _Pie;

    public float forceSpeed;
    public float collapseSpeed;

    [SerializeField]
    private GameObject _PieUI;

    [SerializeField]
    private GameObject _RealWorldCanvas;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnPies());
    }

    IEnumerator SpawnPies()
    {
        while (true)
        {
            SpawnablePieUI spawnablePieUI;
            spawnablePieUI = Instantiate(_PieUI, _RealWorldCanvas.transform).GetComponent<SpawnablePieUI>();
            spawnablePieUI.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(Random.Range(-650f, 650f), 0, 0);
            spawnablePieUI.SetMe(collapseSpeed);

            yield return new WaitForSeconds(1);
            Pie pieScript = Instantiate(_Pie).GetComponent<Pie>();
            Vector3 pieSrciptPos = pieScript.transform.position;
            //pieSrciptPos.x = spawnablePieUI.transform.position.x;
            pieScript.transform.position = spawnablePieUI.transform.position;
            pieScript.transform.position += transform.forward * 5;
            pieScript.SetMe(forceSpeed);

            Destroy(spawnablePieUI.gameObject);

            yield return new WaitForEndOfFrame();
        }

    }
}
