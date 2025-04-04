using UnityEngine;
using UnityEngine.UI;
public class GameUIScript : MonoBehaviour
{
    public static GameUIScript Instance { get; private set;} //This line is for the function QuitMoment, with Singleton design
    [Header("Variables to assign")][Space(10)]
    [SerializeField][Tooltip("The progress level bar in editor")] private Image progressLevelBar;
    [SerializeField][Tooltip("The death level bar in editor")] private Image progressDeathBar;
    [SerializeField][Tooltip("The total time of the level")] private float totalTimeLevel;
    [SerializeField][Tooltip("Maximum booster of the bar")] private float maximumbooster;
    [SerializeField][Tooltip("The time to wait of the death bar")] private float timeToWaitOfTheDeathBar;
    [Header("Variables that you dont have to change")][Space(10)]
    [SerializeField][Tooltip("If it isn't assing, assign it in 3")] private float momentumMultiplier = 3f;
    private float counter, currentbooster, currentcounter, deathSpeed = 1f, originalMaximumBooster, recoveryRate = 0.2f;
    private bool deathBarStart = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        originalMaximumBooster = maximumbooster;
    }
    private void EndLevel()
    {
    }
    private void LoseLevel()
    {
        Debug.Log("You've lost");
    }
    public void QuitMomentum(float momentum)
    {
        if ((maximumbooster - momentum) <= 0)
        {
            maximumbooster = 0;
        }
        else
        {
            maximumbooster -= momentumMultiplier * momentum;
        }
    }
    private void FixedUpdate()
    {
        if (maximumbooster < originalMaximumBooster)
        {
            maximumbooster += recoveryRate * Time.fixedDeltaTime * GameManager.Instance.timeMultiplier;
        }
        currentbooster = Mathf.Min(currentbooster + Time.fixedDeltaTime * GameManager.Instance.timeMultiplier, maximumbooster); //The function Mathf.Min(a,b) gives you the minus value of two of them
        currentcounter += Time.fixedDeltaTime * GameManager.Instance.timeMultiplier;
        if (currentcounter >= timeToWaitOfTheDeathBar)
        {
            deathBarStart = true;
        }
        counter += Time.fixedDeltaTime * GameManager.Instance.timeMultiplier * currentbooster;
        progressLevelBar.fillAmount = counter / totalTimeLevel;
        if (deathBarStart)
        {
            progressDeathBar.fillAmount = Mathf.Lerp(progressDeathBar.fillAmount, currentcounter / totalTimeLevel, Time.fixedDeltaTime * GameManager.Instance.timeMultiplier * deathSpeed);
            //The function Mathf.Lerp(a, b, t) is a function that goes a -> b, with a increment t
        }
        if ((counter / totalTimeLevel) >= 1)
        {
            EndLevel();
        }
        if (progressLevelBar.fillAmount <= progressDeathBar.fillAmount)
        {
            LoseLevel();
        }
    }
}
