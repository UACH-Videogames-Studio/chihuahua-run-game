using UnityEngine;

public class LaneManager : MonoBehaviour
{
    [SerializeField] private Transform laneContainer;
    private Transform[] lanes;

    private void Awake()
    {
        lanes = new Transform[laneContainer.childCount];
        for (int i = 0; i < laneContainer.childCount; i++)
        {
            lanes[i] = laneContainer.GetChild(i);
        }
    }

    public Transform GetLaneAtIndex(int index)
    {
        if (index < 0 || index >= lanes.Length)
        {
            Debug.LogError("Index out of range");
            return null;
        }
        return lanes[index];
    }

    public int GetLaneCount()
    {
        return lanes.Length;
    }
}
