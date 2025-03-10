using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HoleWall : MonoBehaviour
{
    [SerializeField] GameObject[] shapes;
    GameObject curr;
    GameObject prev;
    SpriteRenderer sprite;
    [SerializeField] Player player;
    [SerializeField] Slider slider;
    [SerializeField] Vector3 startPos;
    [SerializeField] Vector3 endPos;
    //[SerializeField] float wallSpeed;
    bool loss = false;
    bool flashing = false;
    int lives = 3;
    int score = 0;
    float switchTime = 0f;
    float flashSpeed = 9f;
    void Start()
    {
        //Spawn();
    }

    void Update()
    {
        // update game if player hasnt lost
        if (!loss)
        {
            if (flashing)
            {
                sprite.color = Color.Lerp(Color.green, Color.yellow, Mathf.PingPong(Time.time * flashSpeed, 1));
            }

            // activate wall sequence
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
    //void Spawn()
    //{
    //    if (curr != null)
    //    {
    //        Destroy(curr);
    //    }
    //    curr = shapes[Random.Range(0, shapes.Length)];
    //    curr = Instantiate(curr, startPos, Quaternion.identity);
    //}

    //void SpeedUp()
    //{
    //    wallSpeed *= 1.3f;
    //}

    // 4 second cycle between walls
    IEnumerator Switch()
    {
        // spawn shape in the forecast
        curr = shapes[Random.Range(0, shapes.Length)];
        curr = Instantiate(curr, new Vector3(7.5f, 5f, 0f), Quaternion.identity);
        sprite = curr.GetComponentInChildren<SpriteRenderer>();
        yield return new WaitForSeconds(2.5f);

        // flash warning before switch
        flashing = true;
        yield return new WaitForSeconds(1.5f);

        // switch and spawn nexr forecast
        flashing = false;
        sprite.color = Color.green;
        curr.transform.position = new Vector3(-14f, 5f, 0f);
        prev = curr;
        CheckHit();
        StartCoroutine(Switch());
        yield return new WaitForSeconds(2f);

        // kill previous shape
        Destroy(prev);
    }

    void CheckHit()
    {
        // lose life if hit the wall
        if (player.hit)
        {
            lives--;
        }
        // increase score if made it in the hole
        else
        {
            score++;
        }

        slider.value = lives / 3f;

        // end game
        if (lives <= 0)
        {
            loss = true;
        }
    }
}
