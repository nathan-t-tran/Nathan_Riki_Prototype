using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 mousePos;
    Vector3 startPos;
    bool hoveringScale;
    bool hoveringMove;
    bool holding;
    bool scale;
    bool move;
    void Start()
    {
        hoveringScale = false;
        hoveringMove = false;
        holding = false;
    }

    // Update is called once per frame
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
            Debug.Log("scalin");
            Vector3 currPos = Input.mousePosition;
            currPos = Camera.main.ScreenToWorldPoint(currPos);
            float diff = Vector3.Distance(currPos, startPos);
            transform.localScale = new Vector3(diff / 2f, diff / 2f, 1f);
        }
        if (move)
        {
            Debug.Log("movin");
            transform.position = mousePos;
        }
    }
    private void FixedUpdate()
    {
        hoveringScale = false;
        hoveringMove = false;
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(mousePos.x, mousePos.y), Vector2.zero);
        if (hit)
        {
            if (hit.collider.CompareTag("Scale"))
            {
                Debug.Log("HOVERING OVER SCALE");
                hoveringScale = true;
            }
            else if (hit.collider.CompareTag("Move"))
            {
                Debug.Log("HOVERING OVER MOVE");
                hoveringMove = true;
            }
        }
    }
}
