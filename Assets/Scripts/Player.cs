using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 mousePos;
    Vector3 startPos;
    public bool hit;
    bool hoveringScale;
    bool hoveringMove;
    bool holding;
    bool scale;
    bool move;

    void Start()
    {
        hit = false;
        hoveringScale = false;
        hoveringMove = false;
        holding = false;
    }

    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0f;

        holding = Input.GetMouseButton(0);
        if (!holding)
        {
            move = false;
            scale = false;
        }
        // set anchor position
        if (holding && !move && !scale)
        {
            startPos = Input.mousePosition;
            startPos = Camera.main.ScreenToWorldPoint(startPos);
            if (hoveringMove)
            {
                move = true;
            }
            else if (hoveringScale)
            {
                scale = true;
            }
        }
        if (scale)
        {
            //Debug.Log("scalin");
            Vector3 currPos = Input.mousePosition;
            currPos = Camera.main.ScreenToWorldPoint(currPos);
            float diff = Vector3.Distance(currPos, startPos);
            diff /= 2f;
            diff = Mathf.Clamp(diff, 0.8f, 9.5f);
            transform.localScale = new Vector3(diff, diff, 1f);
        }
        if (move)
        {
            //Debug.Log("movin");
            Vector3 newPos = mousePos;
            float offset = (transform.localScale.x / 2f);
            newPos.x = Mathf.Clamp(newPos.x, -15.5f + offset, -6f - offset);
            newPos.y = Mathf.Clamp(newPos.y, -3f + offset, 6.5f - offset);
            transform.position = newPos;
        }
    }
    private void FixedUpdate()
    {
        hoveringScale = false;
        hoveringMove = false;
        RaycastHit2D ray = Physics2D.Raycast(new Vector2(mousePos.x, mousePos.y), Vector2.zero);
        if (ray)
        {
            if (ray.collider.CompareTag("Move"))
            {
                //Debug.Log("HOVERING OVER MOVE");
                hoveringMove = true;
            }
            else if (ray.collider.CompareTag("Scale"))
            {
                //Debug.Log("HOVERING OVER SCALE");
                hoveringScale = true;
            }
            else if (ray.collider.CompareTag("Player"))
            {
                //Debug.Log("please stop triggering");
                hoveringMove = true;
            }
        }
    }

    public float GetScale()
    {
        return transform.localScale.x;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Wall"))
    //    {
    //        Debug.Log("HIT");
    //        hit = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Wall"))
    //    {
    //        Debug.Log("hole");
    //        //hit = false;
    //    }
    //}
}
