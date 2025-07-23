using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    public int NumBgCount = 5;

    public int ObstacleCount = 0;
    public Vector3 ObstacleLastPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        ObstacleLastPos = obstacles[0].transform.position;
        ObstacleCount = obstacles.Length;

        for(int i = 0; i < ObstacleCount; ++i)
        {
            ObstacleLastPos = obstacles[i].SetRandomPlace(ObstacleLastPos, ObstacleCount);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Triggered: {collision.name}");

        if(collision.CompareTag("BackGround"))
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * NumBgCount;
            collision.transform.position = pos;

            return;
        }

        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if(obstacle)
        {
            ObstacleLastPos = obstacle.SetRandomPlace(ObstacleLastPos, ObstacleCount);
        }
    }
}
