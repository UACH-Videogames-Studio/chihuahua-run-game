using UnityEngine;

public class UIPauseScript : MonoBehaviour
{
    public void Resume()
    {
        GameManager.Instance.TryResumeGame();
    }
}
