using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject car;
    [SerializeField] private float spawnRate = 2f;

    private float timer;
    private GameObject carsParent;

    private void Start()
    {
        carsParent = GameObject.Find("Cars");
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            Instantiate(car, transform.position, Quaternion.identity, carsParent.transform);
            timer = 0;
        }
    }
}
