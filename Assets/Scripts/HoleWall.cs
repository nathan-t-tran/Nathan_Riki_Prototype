using UnityEngine;

public class HoleWall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject[] shapes;
    GameObject curr;
    [SerializeField] Vector3 startPos;
    [SerializeField] Vector3 endPos;
    [SerializeField] float wallSpeed;
    bool loss = false;
    int score = 0;
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        // update game if player hasnt lost
        if (!loss)
        {
            // spawn new wall if old one has been passed
            if (curr.transform.position.x <= endPos.x)
            {
                score++;
                // increase speed every 5 walls
                if (score % 5 == 0)
                {
                    SpeedUp();
                }
                Spawn();
            }

            // move wall
            Vector3 newPos = curr.transform.position;
            newPos.x -= wallSpeed * Time.deltaTime;
            curr.transform.position = newPos;


        }
    }

    // destroy curr wall and spawn new random one
    void Spawn()
    {
        Destroy(curr);
        curr = shapes[Random.Range(0, shapes.Length + 1)];
        Instantiate(curr, startPos, Quaternion.identity);
    }

    void SpeedUp()
    {
        wallSpeed *= 1.3f;
    }
}
