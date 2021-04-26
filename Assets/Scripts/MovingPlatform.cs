using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovingPlatformDirection { Up, Down, Right, Left }
public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private MovingPlatformDirection orientation = MovingPlatformDirection.Up;
    [Range(0, 1)] [SerializeField] private float attractEffect = 1f;
    private Rigidbody2D platformRB;
    private Vector2 direction;
    private Vector2 attractDir;

    private void Start()
    {
        direction = Direction();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Key"))
        {
            Vector2 pos = col.transform.position;
            Vector2 delta = Vector2.MoveTowards(col.transform.position, transform.position, speed * Time.deltaTime);

            if (direction.y != 0)
                pos.x = delta.x * attractEffect;

            else if (direction.x != 0)
                pos.y = delta.y * attractEffect;

            col.transform.position = pos;

            col.GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
    }

    Vector2 Direction()
    {
        Vector2 dir = Vector2.zero;

        switch (orientation)
        {
            case MovingPlatformDirection.Up:
                dir.y = 1f;
                break;

            case MovingPlatformDirection.Down:
                dir.y = -1f;
                break;

            case MovingPlatformDirection.Right:
                dir.x = 1f;
                break;

            case MovingPlatformDirection.Left:
                dir.x = -1f;
                break;
        }

        return dir;
    }
}
