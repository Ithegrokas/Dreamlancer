using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlameAxis { Horizontal, Vertical }
public enum FlameMovementType { OneWay, BackAndForth }
public enum FlameDirection { Forward, Backward }
public class FlameMovement : MonoBehaviour
{
    [SerializeField] private FlameAxis axis = FlameAxis.Horizontal;
    [SerializeField] private FlameMovementType type = FlameMovementType.OneWay;
    [SerializeField] private float speed = 2.0f;

    [Tooltip("Forward: Up or Right, Backwards: Down or Left")]
    [SerializeField] private FlameDirection direction = FlameDirection.Forward;
    [SerializeField] private float distance = 10f;

    private float distanceTraveled = 0f;
    private Rigidbody2D flameRB;
    private Vector3 initialPosition;
    private float phase = 0;
    private float phasedirection = 1;

    // Start is called before the first frame update
    void Start()
    {
        flameRB = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(initialPosition, DestinationVector(), phase);

        phase += Time.deltaTime * speed * phasedirection;

        phase = Mathf.Clamp(phase, 0, 1);

        if ((phase >= 1) || (phase <= 0))
            phasedirection *= -1;
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

    void distanceHandler()
    {
        distanceTraveled += (speed * Time.deltaTime);

        if (distanceTraveled >= distance)
        {
            distanceTraveled = 0;
            flameRB.velocity = flameRB.velocity * (-1f);
        }

    }
}
