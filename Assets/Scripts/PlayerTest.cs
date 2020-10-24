using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public float[] timestamps;
    private float dt;
    private int index = 0;
    private float currDelta = 0;
    // Start is called before the first frame update
    void Start()
    {
        dt = Time.fixedDeltaTime;
}

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (index % 2 == 0)
        {
            gameObject.transform.Translate(Vector3.forward * dt);
        }
        else
        {
            gameObject.transform.Translate(Vector3.right * dt);
        }
        currDelta += dt;
        if(currDelta >= timestamps[index])
        {
            index++;
            currDelta = 0;
        }

    }
}
