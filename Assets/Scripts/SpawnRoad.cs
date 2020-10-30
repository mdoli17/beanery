using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoad : MonoBehaviour
{
    // Distance between current and next beat
    public float[] timestamps;
    // 0 - beat; 1 - start; 2 - jump; 3 - end;
    public int[] labels;
    public GameObject roadBlock;
    private GameObject prevObject;
    public float height;
    //public float fallSpeed = 3f;
    //public float travelSpeed = 1f;
    private float fallSpeed;
    private float travelSpeed;

    private float maxHeightUnscaled = 0;
    private int dir;
    private bool start, jump, end;
    private Transform player;
    private bool mustChange = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player(Clone)").transform;
        fallSpeed = player.GetComponent<PlayerTest>().fallSpeed;
        travelSpeed = player.GetComponent<PlayerTest>().travelSpeed;
        Vector3 pos = Vector3.zero;
        for (int i = 0; i < timestamps.Length; i++)
        {
            // Is beat
            if ((labels[i] == 0 || labels[i] == 1) && start != true)
            {
                if (labels[i] == 1)
                {
                    start = true;
                }
                GameObject curroad = Instantiate(roadBlock, pos, Quaternion.identity);

                //float halflength = curroad.transform.localScale.z / 2;
                if (i % 2 == 1 || mustChange)
                {
                    //curroad.transform.Translate(0, 0, halflength
                    pos.Set(pos.x + timestamps[i], pos.y, pos.z);
                    if (labels[i] == 1)
                    {
                        curroad.transform.localScale = new Vector3(timestamps[i] + 0.5f, height, 1);
                    }
                    else
                    {
                        curroad.transform.localScale = new Vector3(timestamps[i], height, 1);
                    }
                }
                else
                {

                    //curroad.transform.Translate(halflength, 0, 0);
                    if (i == 0)
                    {
                        curroad.transform.localScale = new Vector3(1, height, timestamps[i] + 1);
                    }
                    else
                    {
                        if (labels[i] == 1)
                        {
                            curroad.transform.localScale = new Vector3(1, height, timestamps[i] + 0.5f);
                        } else
                        {
                            curroad.transform.localScale = new Vector3(1, height, timestamps[i]);
                        }
                    }
                    pos.Set(pos.x, pos.y, pos.z + timestamps[i]);
                }
            }
            else if (start == true)
            {
                if (labels[i - 1] == 1)
                {
                    if (i % 2 == 0)
                    {
                        dir = 0;
                        pos.Set(pos.x +0.5f, pos.y, pos.z);
                    } else
                    {
                        dir = 1;
                        pos.Set(pos.x, pos.y, pos.z + 0.5f);
                    }
                }
                if (labels[i] == 0 && !jump) // droebit
                {

                    maxHeightUnscaled += player.GetComponent<PlayerTest>().upSpeed * timestamps[i];
                    GameObject currentObj;
                    if (prevObject != null)
                    {
                        currentObj = Instantiate(roadBlock, pos, Quaternion.identity, prevObject.transform);
                    }
                    else
                    {
                        currentObj = Instantiate(roadBlock, pos, Quaternion.identity);
                    }
                    prevObject = currentObj;
                    
                    if (dir == 1)
                    {
                        pos.Set(pos.x, pos.y, pos.z + timestamps[i]);
                        Vector3 lossy = currentObj.transform.lossyScale;
              
                        currentObj.transform.localScale = new Vector3(1/lossy.x, height / lossy.y, timestamps[i]/lossy.z);
                    }
                    else
                    {
                        pos.Set(pos.x + timestamps[i], pos.y, pos.z);
                        Vector3 lossy = currentObj.transform.lossyScale;
                        currentObj.transform.localScale = new Vector3(timestamps[i]/lossy.x, height / lossy.y, 1/lossy.z);
                    }
                   
                } else if (labels[i] == 2)
                {
                    // droebit:
                    jump = true;
                    float y = pos.y + maxHeightUnscaled / Time.fixedDeltaTime - fallSpeed * timestamps[i];
                    GameObject currentObj = Instantiate(roadBlock, new Vector3(pos.x, y, pos.z), Quaternion.identity);
                    if (dir == 1)
                    {
                        pos.Set(pos.x, y, pos.z + timestamps[i] * travelSpeed);
                        currentObj.transform.localScale = new Vector3(1, height, timestamps[i] * travelSpeed);
                    }
                    else
                    {
                        pos.Set(pos.x + timestamps[i] * travelSpeed, y, pos.z);
                        currentObj.transform.localScale = new Vector3(timestamps[i] * travelSpeed, height, 1);
                    }
                    /*KeepFixedHeight cls = currentObj.AddComponent<KeepFixedHeight>();
                    cls.height = pos.y + maxHeightUnscaled / Time.fixedDeltaTime - fallSpeed * timestamps[i];
                    cls.player = player;*/
                    
                } else if (labels[i] == 3)
                {
                    GameObject currentObj = Instantiate(roadBlock, pos, Quaternion.identity);

                    if (dir == 1)
                    {
                        pos.Set(pos.x, pos.y, pos.z + timestamps[i] * travelSpeed);
                        currentObj.transform.localScale = new Vector3(1, height, timestamps[i] * travelSpeed);
                    }
                    else
                    {
                        pos.Set(pos.x + timestamps[i] * travelSpeed, pos.y, pos.z);
                        currentObj.transform.localScale = new Vector3(timestamps[i] * travelSpeed, height, 1);
                    }
                    start = false;
                    jump = false;
                    /*if (dir == 1)
                    {
                        mustChange = true;
                    } else
                    {
                        mustChange = false;
                    }*/
                }
            }
            // Debug.Log(timestamps[i]);
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
