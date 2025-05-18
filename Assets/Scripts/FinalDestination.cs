using UnityEngine;
using UnityEngine.UI;

public class FinalDestination : MonoBehaviour
{
    private bool isFinalDestination = false;
    private Text distanceText;
    private Transform player;

    public void IsFinalDest()
    {
        distanceText = GameObject.Find("DistanceText").GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        isFinalDestination = true; 
        
    }

    private void Update()
    {
        if (isFinalDestination)
        {
            float dist = Vector3.Distance(player.transform.position, transform.position) * 2; // 2x to make a little more realistic
            distanceText.text = $"Distance From Exit: {dist:F1}m";
        }
    }
}
