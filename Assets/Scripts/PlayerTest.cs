using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public float[] timestamps;
    public float upSpeed;
    public int[] labels;
    public float fallSpeed = 3f;
    public float travelSpeed = 1f;
    public float maxDistanceFloat = 1f;

    private float dt;
    private int index = 0;
    private float currDelta = 0;
    private bool triggerEntered = false;
    private bool triggerStayed = false;
    private Transform t;
    private int dir;
    private bool start = false;
    private bool exit = false;
    private Rigidbody rb;
    private bool gravity = false;
    public LayerMask mask;
    private bool end = false;

    // Start is called before the first frame update
    void Start()
    {
        dt = Time.fixedDeltaTime;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (labels[index] == 1)
        {
            dir = index % 2;
            start = true;
        }
        if (labels[index] == 2)
        {
            start = false;
            end = true;
        }
        if (index != 0 && labels[index - 1] == 3)
        {
            start = false;
            end = false;
        }
        if (index % 2 == 0 || ((start == true || end == true) && dir == 0))
        {
            transform.Translate(Vector3.forward * dt);
        }
        else if (index % 2 == 1 || ((start == true || end == true) && dir == 1))
        {
            transform.Translate(Vector3.right * dt);
        }

        currDelta += dt;
        if (currDelta >= timestamps[index])
        {
            index++;
            currDelta = 0;
        }
        if (gravity)
        {
            transform.Translate(Vector3.down * fallSpeed * dt);
        }

        // Raycast
        RaycastHit hit;
        Debug.DrawRay(transform.position, Vector3.down * maxDistanceFloat, Color.red);
        if (Physics.Raycast(transform.position, Vector3.down, out hit, maxDistanceFloat, mask))
        {
        }
        else
        {
            gravity = true;
            triggerEntered = false;
        }

        if (triggerEntered && start)
        {
            t.Translate(Vector3.up * upSpeed);
            transform.Translate(Vector3.up * upSpeed);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        //Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawRay(transform.position, Vector3.down * maxDistanceFloat);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (start)
        {
            triggerEntered = true;
            t = other.transform.parent;
        }
        if (gravity)
        {
            gravity = false;
        }
    }
}
