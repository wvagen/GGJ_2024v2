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
        myAnim.Play("LoadingAnim");
        StartCoroutine(LoadSceneCoroutine("LabScene"));
    }

    IEnumerator LoadSceneCoroutine(string sceneName_Path)
    {
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
