using UnityEngine;
using UnityEngine.UI;
public class GameUIScript : MonoBehaviour
{
    public static GameUIScript Instance { get; private set;} //This line is for the function QuitMoment, with Singleton design
    [Header("Variables to assign")][Space(10)]
    [SerializeField] private Image progressLevelBar;
    [SerializeField] private Image progressDeathBar;
    [SerializeField] private float totalTimeLevel;
    [SerializeField] private float maximumbooster;
    [SerializeField] private float timeToWaitOfTheDeathBar;
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
    private void Update()
    {
        /*
        if (currentbooster <= maximumbooster)
        {
            currentbooster += Time.deltaTime;
        }
        */
        if (maximumbooster < originalMaximumBooster)
        {
            maximumbooster += recoveryRate * Time.deltaTime;
        }
        currentbooster = Mathf.Min(currentbooster + Time.deltaTime, maximumbooster); //The function Mathf.Min(a,b) gives you the minus value of two of them
        currentcounter += Time.deltaTime;
        if (currentcounter >= timeToWaitOfTheDeathBar)
        {
            deathBarStart = true;
        }
        counter += Time.deltaTime * currentbooster;
        progressLevelBar.fillAmount = counter / totalTimeLevel;
        if (deathBarStart)
        {
            progressDeathBar.fillAmount = Mathf.Lerp(progressDeathBar.fillAmount, currentcounter / totalTimeLevel, Time.deltaTime * deathSpeed);
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
