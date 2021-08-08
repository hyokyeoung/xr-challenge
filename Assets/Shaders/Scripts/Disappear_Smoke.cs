using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear_Smoke : MonoBehaviour
{
    public ParticleSystem part;
    public ParticleSystem smoke;
    MeshRenderer mesh;
    Material mat;
    //public static int exp = 0;

    void Start()
    {
        part = GetComponent<ParticleSystem>(); // water
        smoke = GetComponent<ParticleSystem>();
    }

    private void Update()
    {

    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("�浹����");
        if (other.tag == "Water")
        {
            Debug.Log("���� �������.");
            smoke.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

    }
}
