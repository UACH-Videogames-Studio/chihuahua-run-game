using UnityEngine;
using UnityEngine.UI;
public class GameUIScript : MonoBehaviour
{
    public static GameUIScript Instance { get; private set;} //This line is for the function QuitMoment, with Singleton design
    [Header("Variables to assign")][Space(10)]
    [SerializeField][Tooltip("The progress level bar in editor")] private Image progressLevelBar;
    [SerializeField][Tooltip("The death level bar in editor")] private Image progressDeathBar;
    [SerializeField][Tooltip("The Chacarito Face")] private Image chacaritoFace;
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
        GameManager.Instance.WinGame();
    }
    private void LoseLevel()
    {
        GameManager.Instance.LoseGame();
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
        //
        // Obtener el tamaño de la barra de progreso
        RectTransform barRect = progressLevelBar.GetComponent<RectTransform>();
        float barWidth = barRect.rect.width;

        // Calcular la posición X de Chacarito en base al fillAmount
        float filledX = (progressLevelBar.fillAmount * barWidth) - (barWidth * 0.5f);

        // Actualizar la posición local del RectTransform de la cara
        Vector3 chacaritoPos = chacaritoFace.rectTransform.localPosition;
        chacaritoPos.x = -filledX;
        chacaritoFace.rectTransform.localPosition = chacaritoPos;
        //
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
