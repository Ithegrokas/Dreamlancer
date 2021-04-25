using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlameAxis { Horizontal, Vertical }
public enum FlameMovementType { OneWay, BackAndForth }
public enum FlameDirection { Forward, Backward }
public class FlameMovement : MonoBehaviour
{
    [SerializeField] private FlameAxis axis = FlameAxis.Horizontal;
    [SerializeField] private FlameMovementType type = FlameMovementType.BackAndForth;
    [SerializeField] private float speed = 2.0f;

    [Tooltip("Forward: Up or Right, Backwards: Down or Left")]
    [SerializeField] private FlameDirection direction = FlameDirection.Forward;
    [SerializeField] private float distance = 10f;

    private float distanceTraveled = 0f;
    private Rigidbody2D flameRB;
    private Vector3 initialPosition;
    private float phase = 0;
    private float phasedirection = 1;
    private Vector3 destination;
    private SpriteRenderer flameSPR;
    private CircleCollider2D flameCol;

    // Start is called before the first frame update
    void Start()
    {
        flameRB = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        destination = DestinationVector();
        flameSPR = GetComponent<SpriteRenderer>();
        flameCol = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (type == FlameMovementType.BackAndForth)
        {
            transform.position = Vector3.Lerp(initialPosition, destination, phase);

            phase += Time.deltaTime * speed * phasedirection;

            phase = Mathf.Clamp(phase, 0, 1);

            if ((phase >= 1) || (phase <= 0))
                phasedirection *= -1;

            if (phase <= 0)
                enableFlame();
        }
        else 
        {
            if (transform.position == destination)
            {
                transform.position = initialPosition;
                enableFlame();
            }
                

            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.CompareTag("Key"))
            disableFlame();
    }

    Vector3 DestinationVector()
    {
        Vector3 orientation = Vector3.zero;

        if (axis == FlameAxis.Horizontal)
            orientation = new Vector3(distance, 0f, 0f);

        else
            orientation = new Vector3(0f, distance, 0f);

        if (direction == FlameDirection.Backward)
            orientation *= (-1f);

        return orientation + initialPosition;
    }

    void enableFlame()
    {
        flameSPR.enabled = true;
        flameCol.enabled = true;
    }

    void disableFlame()
    {
        flameSPR.enabled = false;
        flameCol.enabled = false;
    }
}
