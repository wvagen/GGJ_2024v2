using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnablePie : MonoBehaviour
{
    [SerializeField]
    private Animator mainCanvas;

    [SerializeField]
    private GameObject _Pie;

    public float forceSpeed;
    public float collapseSpeed;
    public float targetRate = 3;

    [SerializeField]
    private GameObject _PieUI;

    [SerializeField]
    private GameObject _RealWorldCanvas;

    [SerializeField]
    private TextMeshProUGUI scoreTxt;

    [SerializeField]
    private TextMeshProUGUI finalScoreTxt, finalBestSCoreTxt;

    [SerializeField]
    private Transform satisfNeedle;

    [SerializeField]
    private Color diselectedSatisfactionCol;

    [SerializeField]
    private Image[] satisfLvlImgs;

    [SerializeField]
    private GameObject[] nonTutoPanel;
    [SerializeField]
    private GameObject tutoPanel;

    [SerializeField]
    private Sprite[] _motivationalSprites;
    [SerializeField]
    private Image _motivationalImg;

    [SerializeField]
    private TextMeshProUGUI _scoreIncTxt;

    Image selectedImg;

    Vector3 currentNeedleRot = Vector3.zero;
    int currentKingMood = 1;

    float nextSpawnTime = 0;

    int score = 0;
    short multiplier = 1;
    public static bool isGameOver = false;

    bool isSpacePressed = false;

    private void Start()
    {
        nextSpawnTime = targetRate;
        isGameOver = true;
        Game_Over_2_AudioManager.audioManInstance.Play_Sfx("talking");
        SelectLevel(1);
        TutoObjects(true);
    }

    void SelectLevel(int levelIndex)
    {
        if (selectedImg != null)
        {
            selectedImg.color = diselectedSatisfactionCol;
            selectedImg.transform.localScale = Vector3.one;
        }
        selectedImg = satisfLvlImgs[levelIndex];
        selectedImg.color = Color.white;
        selectedImg.transform.localScale = Vector3.one * 1.1f;

        mainCanvas.SetInteger("madness_level", levelIndex);
    }

    private void Update()
    {
        nextSpawnTime += Time.deltaTime;

        if (!isGameOver && nextSpawnTime >= targetRate)
        {
            nextSpawnTime = 0;
            StartCoroutine(SpawnPies());
        }

        //For testing
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Game_Over();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGameOver && !isSpacePressed)
        {
            isSpacePressed = true;
            isGameOver = false;
            TutoObjects(false);
        }

        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    currentKingMood--;
        //    SelectLevel(currentKingMood);

        //}
        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    currentKingMood++;
        //    SelectLevel(currentKingMood);
        //}
    }

    void TutoObjects(bool isTuto)
    {
        tutoPanel.SetActive(isTuto);
        for (int i = 0; i < nonTutoPanel.Length; i++)
        {
            nonTutoPanel[i].SetActive(!isTuto);
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

        _scoreIncTxt.text = "+" + (100 * multiplier);
        mainCanvas.Play("inGameCanvas_ScoreIncreaser");

        if (currentKingMood < 2)
        {
            currentKingMood++;
            StartCoroutine(RotateTowards(-30));
            SelectLevel(currentKingMood);

            if (currentKingMood == 2)
            {
                Game_Over_2_AudioManager.audioManInstance.Play_Sfx("king_laugh");
            }
        }
        else
        {
            multiplier ++;

            switch (multiplier)
            {
                case 2:
                    Play_Motivational_Word(0);break;
                case 4:
                    Play_Motivational_Word(1); break;
                case 6:
                    Play_Motivational_Word(2); break;
            }
        }
    }

    void Play_Motivational_Word(int index)
    {
        _motivationalImg.sprite = _motivationalSprites[index];
        mainCanvas.Play("inGameCanvas_Motivation");
        Game_Over_2_AudioManager.audioManInstance.Play_Sfx("motivation_" + index.ToString());
    }

    public void DecScore()
    {
        multiplier = 1;
        if (currentKingMood > 0)
        {
            currentKingMood--;
            StartCoroutine(RotateTowards(30));
            SelectLevel(currentKingMood);
        }
        else
        {
            Game_Over();
        }
    }

    IEnumerator RotateTowards(float angle)
    {
        Vector3 targetPos = currentNeedleRot;

        targetPos.z = currentNeedleRot.z + angle;

        while(Mathf.Abs(targetPos.z - currentNeedleRot.z) > 0.05f)
        {
            currentNeedleRot = Vector3.MoveTowards(currentNeedleRot, targetPos, Time.deltaTime * 100);
            satisfNeedle.eulerAngles = currentNeedleRot;
            yield return new WaitForEndOfFrame();
        }
        currentNeedleRot = targetPos;
    }

    public void Game_Over()
    {
        finalScoreTxt.text = score.ToString();
        isGameOver = true;
        int bestScore = PlayerPrefs.GetInt("best", score);
        if (score >= bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("best", bestScore);
        }

        finalBestSCoreTxt.text = bestScore.ToString();
        mainCanvas.Play("Game_Over");
        Game_Over_2_AudioManager.audioManInstance.ChangeMusicPitch();
    }
}
