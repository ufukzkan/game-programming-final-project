using UnityEngine;

public class DropingSpray : MonoBehaviour
{
    public bool isDropped;
    public bool getFirstPos;
    public float speedXZ;
    public float speedY;
    public float rotationSpeed = 10;
    float randomX;
    float randomZ;
    Vector3 Destination;

    private void Start()
    {
        isDropped = false;
        getFirstPos = false;
    }
    private void Update()
    {
        if (isDropped)
        {
            if (!getFirstPos)
            {

                
                while (!(randomX > 2 || randomX < -2))
                {
                    randomX = Random.Range(-7.0f, 7.0f);
                }
                
                while (!(randomZ > 2 || randomZ < -2))
                {
                    randomZ = Random.Range(-7.0f, 7.0f);
                }
                Destination = new Vector3(transform.position.x + randomX, -1f, transform.position.z + randomZ);
                getFirstPos = true;
            }

            
            transform.position = Vector3.Lerp(transform.position, new Vector3(Destination.x, transform.position.y, Destination.z), speedXZ * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, Destination.y, transform.position.z), Time.deltaTime * speedY);

            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.eulerAngles.y + rotationSpeed, transform.rotation.y));

            if (transform.position.y < 0.125)
                Destroy(gameObject);
        }

    }
}
