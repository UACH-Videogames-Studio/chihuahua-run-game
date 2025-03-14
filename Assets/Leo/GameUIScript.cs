using UnityEngine;
using UnityEngine.UI;

public class GameUIScript : MonoBehaviour
{
    public static GameUIScript Instance { get; private set;} //This line is for the function QuitMoment, with Singleton design
    [SerializeField] private Image progressLevelBar;
    [SerializeField] private Image progressDeathBar;
    [SerializeField] private float totalTimeLevel;
    [SerializeField] private float maximumbooster;
    [SerializeField] private float timeToWaitOfTheDeathBar;
    private float counter, currentbooster, currentcounter, deathSpeed = 1f;
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
    }
    private void EndLevel()
    {
        Debug.Log("El nivel se ha acabado");
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
            maximumbooster -= momentum;
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
