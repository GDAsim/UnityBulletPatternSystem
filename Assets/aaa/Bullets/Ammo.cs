using UnityEngine;

public class Ammo : MonoBehaviour
{
    Vector3 speed;

    public void Setup(Vector3 speed)
    {
        this.speed = speed;
    }

    void Update()
    {
        Fly();
    }
    void Fly()
    {
        transform.Translate(speed * Time.deltaTime);
    }
    void Move()
    {

    }
}
