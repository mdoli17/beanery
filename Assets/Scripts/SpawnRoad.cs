using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoad : MonoBehaviour
{
    public float[] timestamps;
    public GameObject roadBlock;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = Vector3.zero;
        for (int i = 0; i < timestamps.Length; i++)
        {
            GameObject curroad = Instantiate(roadBlock, pos, Quaternion.identity);
            
            //float halflength = curroad.transform.localScale.z / 2;
            if (i%2 == 1)
            {
                //curroad.transform.Translate(0, 0, halflength);
                curroad.transform.localScale = new Vector3(timestamps[i], 1, 1);
                pos.Set(pos.x + timestamps[i], pos.y, pos.z);

            }
            else
            {
                
                //curroad.transform.Translate(halflength, 0, 0);
                if(i == 0)
                {
                    curroad.transform.localScale = new Vector3(1, 1, timestamps[i] + 1);
                }
                else
                {
                    curroad.transform.localScale = new Vector3(1, 1, timestamps[i]);
                }
                pos.Set(pos.x, pos.y, pos.z + timestamps[i]);
                
            }
            
            // Debug.Log(timestamps[i]);
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
