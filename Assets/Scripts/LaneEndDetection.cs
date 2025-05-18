using UnityEngine;

public class LaneEndDetection : MonoBehaviour
{
    [SerializeField] private FinalDestination finalDestination;

    private bool isExitLane = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Car"))
        {
            Destroy(collider.gameObject);
        }
        else if (collider.gameObject.CompareTag("Player"))
        {
            if (isExitLane)
            {
                LevelManager.Instance.Win();
            }
            else
            {
                LevelManager.Instance.Lose();
            }
        }
    }

    public void MakeExitLane()
    {
        isExitLane = true;
        finalDestination.IsFinalDest();

        Color exitColor;
        if (ColorUtility.TryParseHtmlString("#1BAB52", out exitColor))
        {
            gameObject.GetComponent<SpriteRenderer>().color = exitColor;
        }
    }
}
