using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public Vector3 initialposition;
    public GameObject player;
   

    [Range(0, 1f)]
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, initialposition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
