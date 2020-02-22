using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{

    public float x_axis_speed_multiplier = 0.0f;
    public float y_axis_speed_multiplier = 0.0f;
    public float z_axis_speed_multiplier = 0.0f;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(x_axis_speed_multiplier * speed * Time.deltaTime, y_axis_speed_multiplier * speed* Time.deltaTime,z_axis_speed_multiplier*speed* Time.deltaTime);
    }
}
