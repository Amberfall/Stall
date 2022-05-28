using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientRainSound : MonoBehaviour
{

    public AudioSource rain1;
    public AudioSource rain2;


    private void Awake()
    {
        Invoke("StartRain2", 1);
    }

    public void StartRain2()
    {
        rain2.Play();
    }

}
