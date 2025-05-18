using UnityEngine;

public class CarAI : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 0.3f;
    [SerializeField] private float minSpeed = 0.1f;

    private float speed;

    private void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 moveAmount = new Vector3(0, speed, 0);
        gameObject.transform.Translate(moveAmount);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            CarAI otherCar = collision.gameObject.GetComponentInParent<CarAI>();
            if (otherCar != null)
            {
                speed = otherCar.GetSpeed();
            }
        }
    }

    public float GetSpeed()
    {
        return speed;
    }
}
