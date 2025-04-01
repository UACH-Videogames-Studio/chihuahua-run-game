using UnityEngine;
using UnityEngine.UI;

public class CameraPositionData : MonoBehaviour
{
    [Header("Ortho size")]
    [SerializeField] public float orthoSize = 5f;

    [Header("Image")]
    public Image targetImage;

    [Range(1f, 2f)]
    public float marginMultiplier = 1.5f;

    public void CalculateAutoFit(Camera referenceCamera)
    {
        if (targetImage == null)
        {
            Debug.LogWarning("No hay targetImage asignado en " + gameObject.name);
                return;
        }

        RectTransform rt = targetImage.GetComponent<RectTransform>();
        if (rt == null)
        {
            Debug.LogWarning("No RectTransform in targgetImage");
            return;
        }

        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);

        float minX = Mathf.Min(corners[0].x, corners[1].x, corners[2].x, corners[3].x);
        float maxX = Mathf.Max(corners[0].x, corners[1].x, corners[2].x, corners[3].x);
        float minY = Mathf.Min(corners[0].y, corners[1].y, corners[2].y, corners[3].y);
        float maxY = Mathf.Max(corners[0].y, corners[1].y, corners[2].y, corners[3].y);
        
        float width = maxX - minX;
        float height = maxY - minY;

        float centerX = (minX + maxX) / 2f;
        float centerY = (minY + maxY) / 2f;

        float centerZ = this.transform.position.z;

        float aspect = referenceCamera.aspect;

        float neededSizeVertical = (height / 2f) * marginMultiplier;
        float neededSizeHorizontal = (width / 2f) * marginMultiplier / aspect;
        float finalSize = Mathf.Max(neededSizeVertical, neededSizeHorizontal);

        this.transform.position = new Vector3(centerX, centerY, centerZ);
        this.orthoSize = finalSize;
    }
}

