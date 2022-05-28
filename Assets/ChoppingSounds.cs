using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppingSounds : MonoBehaviour
{

    public List<AudioSource> chopSound = new List<AudioSource>();

    float timer;

    public bool isChopping;

    public void IsChopping(bool value)
    {
        isChopping = value;
    }

    private void Update()
    {
        if (isChopping)
        {
            if (timer <= 0)
            {
                chopSound[Random.Range(0, 3)].Play();
                timer = Random.Range(0.2f, 0.7f);
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }

    }

}
