using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraComicController : MonoBehaviour
{
    [Header("Camera Points (Images order)")]
    public Transform[] cameraPositions;

    [Header("Movement parameters")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float zoomSpeed = 2f;

    [Header("Next Scene")]
    [SerializeField] private string nextScene = "Nivel";

    private Camera cam;
    private int currentIndex = 0;
    private bool isTransitioning = false;

    //private void Awake()
    //{
        
    //    cam = GetComponent<Camera>();
        
    //}

    private void Start()
    {
        cam = GetComponent<Camera>();
        foreach (Transform transform in cameraPositions)
        {
            CameraPositionData data = transform.GetComponent<CameraPositionData>();
            if (data != null && data.targetImage != null)
            {
                data.CalculateAutoFit(cam);
            }
        }
        if (cameraPositions.Length > 0)
        {
            Transform firstPos = cameraPositions[0];
            CameraPositionData firstData = firstPos.GetComponent<CameraPositionData>();
            transform.position = new Vector3(firstPos.position.x, firstPos.position.y, transform.position.z);

            if (firstData != null)
            {
                cam.orthographicSize = firstData.orthoSize;
            }
        }
    }

    private void Update()
    {
        if (isTransitioning)
        {
            MoveCameraToTarget();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextPanel();
        }
    }

    public void NextPanel()
    {
        if (isTransitioning) return;

        currentIndex++;
        if (currentIndex >= cameraPositions.Length)
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            isTransitioning = true;
        }
    }

    private void MoveCameraToTarget()
    {
        Transform target = cameraPositions[currentIndex];

        Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);

        float targetSize = target.GetComponent<CameraPositionData>().orthoSize;
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, Time.deltaTime * zoomSpeed);

        float dist = Vector2.Distance(transform.position, targetPos);
        if (dist < 0.01f)
        {
            transform.position = targetPos;
            cam.orthographicSize = targetSize;
            isTransitioning = false;
        }

    }
}
