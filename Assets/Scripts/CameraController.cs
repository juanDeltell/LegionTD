
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool doMovement = true;
    
    public float panSpeed = 30f;
    public float panBorderThickness = 1f;
    public float scrollSpeed = 3f;
    public float minY = 3f;
    public float maxY = 25f;
    public float minX = 3f;
    public float maxX = 25f;

    private float offsetX = 1.7f;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GameIsOver)
        {
            this.enabled = false;
            return;
        }
            

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doMovement = !doMovement;
        }
        if (!doMovement)
        {
            return;
        }

        if (Input.GetKey("up") || (Input.mousePosition.y >= (/*Screen.currentResolution.height*/Screen.height) - panBorderThickness) )
        {
            //new Vector3 (0f,0f, 1f) * panSpeed;
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("down") || (Input.mousePosition.y <= panBorderThickness))
        {
            //new Vector3 (0f,0f, 1f) * panSpeed;
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("left") || (Input.mousePosition.x <= panBorderThickness))
        {
            //new Vector3 (0f,0f, 1f) * panSpeed;
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
           // Debug.Log("height: " + Screen.height);
          //  Debug.Log("raton : " + Input.mousePosition.x);
         //   Debug.Log("raton + offsetX : " + Input.mousePosition.x * offsetX);
        }
        if (Input.GetKey("right") || (Input.mousePosition.x >= (/*Screen.height*/Screen.width) - panBorderThickness))
        {
            //new Vector3 (0f,0f, 1f) * panSpeed;
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);

        }


        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;

    }
}
