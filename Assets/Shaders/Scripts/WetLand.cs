using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WetLand : MonoBehaviour
{
    public ParticleSystem part;
    //public MeshRenderer land;
    //public GameObject gland;
    //public Renderer renderer;
    Rigidbody rigid;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        //gland = GameObject.Find("Land"); // land �±׵� ��ü
        //renderer = gland.GetComponent<Renderer>();
        rigid = GetComponent<Rigidbody>();
        
        //land = GetComponent<MeshRenderer>();
    }

    /*
    void OnParticleCollision(GameObject other)
    {
        Debug.Log("�浹����");
        if (other.tag == "Land")
        {
            Debug.Log("�뿡 ���� ��Ҵ�.");
            //renderer.material.color = new Color(51 / 255f, 0 / 255f, 0 / 255f, 255 / 255f);
        }
    }*/
}