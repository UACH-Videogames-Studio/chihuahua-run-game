using UnityEngine;
[CreateAssetMenu(fileName = "New Object", menuName = "ScriptableObjects/SceneLoader")]
public class PannelsScriptableObjects : ScriptableObject
{
    [SerializeField] private GameObject pausePannel;
    public GameObject PausePannel { get => pausePannel; set => pausePannel = value; }
    [SerializeField] private GameObject gamePannel;
    public GameObject GamePannel { get => gamePannel; set => gamePannel = value; }
    [SerializeField] private GameObject winPannel;
    public GameObject WinPannel { get => winPannel; set => winPannel = value; }
    [SerializeField] private GameObject losePannel;
    public GameObject LosePannel { get =>losePannel; set =>losePannel = value; }
}
