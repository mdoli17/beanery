using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepFixedHeight : MonoBehaviour
{
    public float height = 0;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckRoutine());
    }

    IEnumerator CheckRoutine()
    {
        while (true)
        {
            if (height != 0 && player != null)
            {
                Vector2 playerXZ = new Vector2(player.position.x, player.position.z);
                Vector2 platformXZ = new Vector2(transform.position.x, transform.position.z);;
                if (Vector2.Distance(playerXZ, platformXZ) < 3)
                {
                    transform.position = new Vector3(transform.position.x, height, transform.position.z);
                }
            }
            
            yield return new WaitForSeconds(1f);
        }
    }
}
