using System.Collections;
using UnityEngine;

public class HoleWall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject[] shapes;
    GameObject curr;
    GameObject prev;
    SpriteRenderer sprite;
    [SerializeField] Vector3 startPos;
    [SerializeField] Vector3 endPos;
    //[SerializeField] float wallSpeed;
    bool loss = false;
    bool flashing = false;
    int score = 0;
    float switchTime = 0f;
    float flashSpeed = 9f;
    void Start()
    {
        Spawn();
    }

    void Update()
    {
        // update game if player hasnt lost
        if (!loss)
        {
            if (flashing)
            {
                sprite.color = Color.Lerp(Color.red, Color.yellow, Mathf.PingPong(Time.time * flashSpeed, 1));
            }

            switchTime += Time.deltaTime;
            if (switchTime >= 4f)
            {
                switchTime = -400000f;
                StartCoroutine(Switch());
            }

            //// spawn new wall if old one has been passed
            //if (curr.transform.position.x <= endPos.x)
            //{
            //    score++;
            //    // increase speed every 5 walls
            //    if (score % 5 == 0)
            //    {
            //        SpeedUp();
            //    }
            //    Spawn();
            //}

            //// move wall
            //Vector3 newPos = curr.transform.position;
            //newPos.x -= wallSpeed * Time.deltaTime;
            //curr.transform.position = newPos;
        }
    }

    // destroy curr wall and spawn new random one
    void Spawn()
    {
        if (curr != null)
        {
            Destroy(curr);
        }
        curr = shapes[Random.Range(0, shapes.Length)];
        curr = Instantiate(curr, startPos, Quaternion.identity);
    }

    //void SpeedUp()
    //{
    //    wallSpeed *= 1.3f;
    //}

    IEnumerator Switch()
    {
        curr = shapes[Random.Range(0, shapes.Length)];
        curr = Instantiate(curr, new Vector3(7.5f, 5f, 0f), Quaternion.identity);
        sprite = curr.GetComponentInChildren<SpriteRenderer>();
        yield return new WaitForSeconds(2.5f);
        flashing = true;
        yield return new WaitForSeconds(1.5f);
        flashing = false;
        sprite.color = Color.red;
        curr.transform.position = new Vector3(-14f, 5f, 0f);
        prev = curr;
        StartCoroutine(Switch());
        yield return new WaitForSeconds(2f);
        Destroy(prev);
    }
}
