using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMan : MonoBehaviour
{
    [SerializeField]
    private Animator mainCanvasAnimator;

    [SerializeField]
    private Animator myAnim;

    [SerializeField]
    private Animator camAnim;

    bool isSPacePressed = false;
    // Update is called once per frame

    private void Start()
    {
        Game_Over_2_AudioManager.audioManInstance.InitMusicPitch();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 && !isSPacePressed
            && Input.GetKeyDown(KeyCode.Space))
        {
            isSPacePressed = true;
            StartMainGame();
        }
    }

    void StartMainGame()
    {
        camAnim.Play("CamAnim");
        mainCanvasAnimator.Play("MainReverse");
        StartCoroutine(LoadSceneCoroutine("LabScene"));
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    IEnumerator LoadSceneCoroutine(string sceneName_Path)
    {
        myAnim.Play("LoadingAnim");

        yield return new WaitForSeconds(0.5f);

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName_Path);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;

        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
