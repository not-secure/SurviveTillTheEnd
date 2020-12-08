using Enemy;
using UnityEngine;

public class EnemySample : EnemyBase
{
    private float startY;

    public int speed = 5;
    public int damage = 1;

    public void Start()
    {
        startY = transform.position.y;   
    }

    public void Update()
    {
        // Moving towards player
        Quaternion playerRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, Time.deltaTime * 5);
        transform.position += transform.forward * Time.deltaTime * speed;

        // Simple floating effect
        transform.position = new Vector3(transform.position.x, startY, transform.position.z);
    }
}
