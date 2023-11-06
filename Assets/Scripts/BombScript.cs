using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    private GameObject gm = null;
    private float timer;
    public GameObject explote;
    public AudioClip burning;
    public GameObject particle;
    private Vector3 particleVector;
    // Start is called before the first frame update
    void Start()
    {
        particleVector = new Vector3(transform.position.x, transform.position.y + 0.19f, transform.position.z);
        GameObject.Instantiate(particle, particleVector, Quaternion.identity, this.transform);
        gm = GameObject.FindGameObjectWithTag("GameManager");
        GetComponent<MeshRenderer>().material.color = Color.gray;
        timer = 0.0f;
        GetComponent<AudioSource>().PlayOneShot(burning);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        this.GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.gray, Color.red, timer / 4.0f);
        //Si han passat 4 segons --> Destroy this GameObject & damage in GameManager:
        if (timer >= 4.0f){
            DestroyBomb();
        }
        if (PlayerPrefs.GetInt("GameFinish") == 1)
        {
            Destroy(gameObject);
        }
    }

    void OnMouseDown()
    {
        if (PlayerPrefs.GetInt("GameFinish") == 0)
        {
            GetComponent<AudioSource>().Stop();
            gm.GetComponent<GameManager>().AddScore();
            Destroy(gameObject);
        }
    }

    public void DestroyBomb()
    {
        if (PlayerPrefs.GetInt("GameFinish") == 0)
        {
            GetComponent<AudioSource>().Stop();
            GameObject.Instantiate(explote, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            gm.GetComponent<GameManager>().TakeDamage();
            Destroy(gameObject);
        }
    }

}
