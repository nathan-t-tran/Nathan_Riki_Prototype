using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] HoleWall hole;
    [SerializeField] Player player;
    [SerializeField] GameObject p;
    Vector3 scale = Vector3.one;
    void Update()
    {
        float newScale = player.GetScale();
        scale = new Vector3(newScale, newScale, 1f);
        transform.localScale = scale;
        transform.position = p.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") && hole.active)
        {
            Debug.Log("HITbox");
            //player.hit = true;
            hole.LoseLive();
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Wall") && !hole.active)
    //    {
    //        player.hit = false;
    //    }
    //}
}
