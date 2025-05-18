using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            LevelManager.Instance.Lose();
        }
    }
}
