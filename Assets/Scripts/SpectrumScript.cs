using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrumScript : MonoBehaviour
{
    public GameObject[] spectrum = new GameObject[512];

    public float maxHeight;
    public GameObject spectrumObject;
    public Vector2 size;
    public float offset;
    public int eqOffset;

    // Start is called before the first frame update
    void Start()
    {
        int j = 0;
        for (int i = 0; i < spectrum.Length; i++)
        {
            if (i % 16 == 0) j++;
            GameObject spawnObject = Instantiate(spectrumObject);
            spawnObject.transform.position = new Vector3(transform.position.x + (offset + size.x) * (i % 16), transform.position.y, transform.position.z + (offset + size.y) * j);
            spawnObject.name = "sampleObject " + i;
            spawnObject.transform.parent = transform;
            //this.transform.eulerAngles = new Vector3(0, -0.703125f * i, 0);
            //spawnObject.transform.position = Vector3.forward * 10;
            spectrum[i] = spawnObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < spectrum.Length; i++)
        {
            if (spectrum[i] != null)
            {
                spectrum[(i + eqOffset)% 512].transform.localScale = new Vector3(size.x, size.x + AudioPeer.audioSpectrum[i] * maxHeight, size.y);
                //spectrum[i].transform.position = new Vector3(spectrum[i].transform.position.x, transform.position.y + spectrum[i].transform.localScale.y, spectrum[i].transform.position.z);

            }
        }
    }
}
