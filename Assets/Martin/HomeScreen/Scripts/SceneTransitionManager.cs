using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Animator transitionAnim;
    [SerializeField] private float transitionTime = 1f;
    [SerializeField] private string nextSceneName;
    public void LoadNextScene()
    {
        StartCoroutine(SceneLoad(nextSceneName));
    }

    public IEnumerator SceneLoad(string sceneName)
    {
        transitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }
}
