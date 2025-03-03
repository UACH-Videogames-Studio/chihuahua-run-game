using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberText;
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        SetNumberText(slider.value);
    }


    public void SetNumberText(float value)
    {
        numberText.text = value.ToString();
    }
}
