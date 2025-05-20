using UnityEngine;
using UnityEngine.SceneManagement;

public class LaneGenerator : MonoBehaviour
{
    private static LaneGenerator instance;

    [SerializeField] private GameObject lane;
    [SerializeField] private GameObject car;
    [SerializeField] private float minDistanceBetweenCars = 10f;

    private GameObject laneParent;
    private GameObject carsParent;

    private GameObject player;
    private int level = 0;
    private int numberOfLanes; // = level * 1.5 + 2
    private int length; // length of the level = # 75 + 7 * #-lanes
    private int numberOfCarsPerLane; // = length / 5 + level * 1.5

    public static LaneGenerator Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<LaneGenerator>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "LaneGenerator";
                    instance = obj.AddComponent<LaneGenerator>();
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

    public void GenerateLanes()
    {
        ClearLanes();

        Debug.Log("Generating Lanes...");

        numberOfLanes = (int) (level * 1.5) + 2;
        length = 75 + 7 * numberOfLanes;
        float laneWidth = lane.transform.localScale.x;

        for (int i = 0; i < numberOfLanes; i++)
        {
            float x = -((float)numberOfLanes / 2f) * laneWidth + i * laneWidth + laneWidth / 2f;
            Vector3 lanePosition = new Vector3(x, 0, 0);
            GameObject newLane = Instantiate(lane, lanePosition, Quaternion.identity, laneParent.transform);

            Vector3 scaledLane = new Vector3(newLane.transform.localScale.x, length, 0);
            newLane.transform.localScale = scaledLane;

            // Adds cars
            numberOfCarsPerLane = length / 5 + (int) (level * 1.5);
            float nextY;
            if (i == 0)
            {
                nextY = 15;
            }
            else
            {
                nextY = 0;
            }
            for (int j = 0; j < numberOfCarsPerLane; j++)
            {
                Vector3 spawnPoint = new Vector3(x, nextY - length / 2, 0);
                Instantiate(car, spawnPoint, Quaternion.identity, carsParent.transform);
                nextY += Random.Range(minDistanceBetweenCars, length / numberOfCarsPerLane);
            }

            
            // Sets player spawn
            if (i == 0)
            {
                PlayerController playerController = player.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    Vector3 startLocation = new Vector3(x, -length / 2 + 2, 0);
                    playerController.MoveToSpawn(startLocation);
                    playerController.LoadLaneInfo(numberOfLanes, laneWidth);
                }
            }

            // Sets winning lane
            if (i == numberOfLanes - 1)
            {
                LaneEndDetection laneEndDetection = newLane.GetComponent<LaneEndDetection>();
                if (laneEndDetection != null)
                {
                    laneEndDetection.MakeExitLane();
                }
            }
        }
    }

    private void ClearLanes()
    {
        GameObject[] lanes = GameObject.FindGameObjectsWithTag("Lane");
        foreach (GameObject l in lanes) { Destroy(l); }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        laneParent = GameObject.Find("Lanes");
        carsParent = GameObject.Find("Cars");
        player = GameObject.Find("Player");
        if (!lane || !laneParent || !player || !carsParent) return;

        ClearLanes();
        GenerateLanes();
    }

    public void IncrementLevel()
    {
        level++;
    }

    public void ResetLevel()
    {
        level = 0;
    }

}
