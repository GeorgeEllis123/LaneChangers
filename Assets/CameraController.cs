using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float offset = 5f;
    [SerializeField] float zValue = -10f;

    private Transform playerTransform;
    private float laneLength = -1;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        var aLane = GameObject.Find("Lane(Clone)");
        if (aLane != null)
        {
            laneLength = aLane.transform.localScale.y;
        }
    }

    void Update()
    {

        if (playerTransform != null && laneLength != -1)
        {
            if (playerTransform.position.y < -laneLength / 2 + offset)
            {
                transform.position = new Vector3(playerTransform.position.x, -laneLength / 2 + offset, zValue);
            }
            else if (playerTransform.position.y > laneLength / 2 - offset)
            {
                transform.position = new Vector3(playerTransform.position.x, laneLength / 2 - offset, zValue);
            }
            else {
                transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, zValue);
            }
        }
    }
}
