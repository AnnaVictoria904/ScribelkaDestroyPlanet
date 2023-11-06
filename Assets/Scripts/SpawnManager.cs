using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float respawnTime = 3.0f;
    private float timer = 0.0f;
    public GameObject prefabBomb;
    public GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("GameFinish") == 0)
        {
            timer += Time.deltaTime;
            if (timer >= respawnTime)
            {
                CreateNewBomb();
                timer = 0.0f;
            }
        }
    }

    private void CreateNewBomb()
    {
        Vector3 randPosition = Random.onUnitSphere * 0.6f;
        GameObject.Instantiate(prefabBomb, randPosition, Quaternion.identity, parent.transform);
    }
    public void SetRespawnTime(float newRespawnTime)
    {
        respawnTime = newRespawnTime;
    }

}