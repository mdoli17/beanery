using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour
{
    AudioSource audioSource;


    public static float[] audioSpectrum = new float[512];

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAudioSpectrum();
    }

    void UpdateAudioSpectrum()
    {
        audioSource.GetSpectrumData(audioSpectrum, 0, FFTWindow.Blackman);
    }
}
