using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class SpawnablePie : MonoBehaviour
{
    [SerializeField]
    private Animator mainCanvas;

    [SerializeField]
    private GameObject _Pie;

    public float forceSpeed;
    public float collapseSpeed;

    [SerializeField]
    private GameObject _PieUI;

    [SerializeField]
    private GameObject _RealWorldCanvas;

    [SerializeField]
    private TextMeshProUGUI scoreTxt;

    [SerializeField]
    private TextMeshProUGUI finalScoreTxt, finalBestSCoreTxt;

    float nextSpawnTime = 0;
    float targetRate = 3;

    int score = 0;
    short multiplier = 1;
    public static bool isGameOver = false;

    private void Start()
    {
        nextSpawnTime = targetRate;
        isGameOver = false;
    }

    private void Update()
    {
        nextSpawnTime += Time.deltaTime;

        if (!isGameOver && nextSpawnTime >= targetRate)
        {
            nextSpawnTime = 0;
            StartCoroutine(SpawnPies());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Game_Over();
        }
    }

    IEnumerator SpawnPies()
    {
            SpawnablePieUI spawnablePieUI;
            spawnablePieUI = Instantiate(_PieUI, _RealWorldCanvas.transform).GetComponent<SpawnablePieUI>();
            spawnablePieUI.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(Random.Range(-650f, 650f), Random.Range(-150f, 150f), 0);
            spawnablePieUI.SetMe(collapseSpeed);

            yield return new WaitForSeconds(1);
            Pie pieScript = Instantiate(_Pie).GetComponent<Pie>();
            Vector3 pieSrciptPos = pieScript.transform.position;
            //pieSrciptPos.x = spawnablePieUI.transform.position.x;
            pieScript.transform.position = spawnablePieUI.transform.position;
            pieScript.transform.position += transform.forward * 5;
            pieScript.SetMe(this,forceSpeed);

            Destroy(spawnablePieUI.gameObject, collapseSpeed);
    }

    public void IncScore()
    {
        score += 100 * multiplier;
        scoreTxt.text = score.ToString();
        mainCanvas.Play("inGameCanvas_Laugher");
    }

    public void Game_Over()
    {
        finalScoreTxt.text = score.ToString();
        isGameOver = true;
        int bestScore = PlayerPrefs.GetInt("best", score);
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("best", bestScore);
        }

        finalBestSCoreTxt.text = bestScore.ToString();
        mainCanvas.Play("Game_Over");
    }
}
