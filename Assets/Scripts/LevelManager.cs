using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;

    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<LevelManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "LevelManager";
                    instance = obj.AddComponent<LevelManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Win()
    {
        Debug.Log("Win!");
        LaneGenerator.Instance.IncrementLevel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void Lose()
    {
        Debug.Log("Lose!");
        LaneGenerator.Instance.ResetLevel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
