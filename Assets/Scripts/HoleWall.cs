using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HoleWall : MonoBehaviour
{
    [SerializeField] GameObject[] shapes;
    [SerializeField] SpriteRenderer[] spr;
    GameObject curr;
    GameObject prev;
    SpriteRenderer sprite;
    [SerializeField] Player player;
    [SerializeField] Slider slider;
    [SerializeField] Vector3 startPos;
    [SerializeField] Vector3 endPos;
    //[SerializeField] float wallSpeed;
    public bool active = false;
    public bool loss = false;
    bool flashing = false;
    int lives = 3;
    int prelives = 3;
    int score = 0;
    float switchTime = 0f;
    float flashSpeed = 12f;
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
        curr.tag = "Wall";
        sprite = curr.GetComponentInChildren<SpriteRenderer>();
        yield return new WaitForSeconds(2.5f);

        // flash warning before switch
        flashing = true;
        yield return new WaitForSeconds(1.5f);

        // switch and spawn nexr forecast
        active = true;
        flashing = false;
        GridRed();
        sprite.color = Color.green;
        curr.transform.position = new Vector3(-14f, 5f, 0f);
        prev = curr;
        CheckHit();
        StartCoroutine(Switch());
        yield return new WaitForSeconds(1f);

        // kill previous shape
        GridWhite();
        active = false;
        player.hit = false;
        Destroy(prev);
        //player.hit = false;
    }

    void CheckHit()
    {
        HoleSoundManager soundManager = FindObjectOfType<HoleSoundManager>();

        if (prelives > lives)
        {
            prelives = lives;
        }
        else
        {
            score++;
            Debug.Log("Sweat! Player fit the hole!");

            if (soundManager != null)
            {
                soundManager.PlaySuccessSound();
            }
        }

        slider.value = lives / 3f;

        if (lives <= 0)
        {
            loss = true;
        }
    }

    public void LoseLive()
    {
        lives--;
        Debug.Log("lives: " + lives);
        slider.value = lives / 3f;
    }

    void GridRed()
    {
        for (int i = 0; i < spr.Length; i++)
        {
            spr[i].color = Color.red;
        }
    }

    void GridWhite()
    {
        for (int i = 0; i < spr.Length; i++)
        {
            spr[i].color = Color.white;
        }
    }
}
