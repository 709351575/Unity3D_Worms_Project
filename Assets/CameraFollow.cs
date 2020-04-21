using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{
    private GameObject obj;

    private Vector3 DesiredPosition;

    private float SmoothSpeed = 0.125f; // the higher the value is, the faster camera locks on the target

    private Vector3 offset = new Vector3(0, 0, -10);

    // Start is called before the first frame update
    void Start()
    {
        Button button;
        button = GameObject.Find("Button_Fly").GetComponent<Button>();
        button.gameObject.SetActive(false);
        button = GameObject.Find("Button_Hameha").GetComponent<Button>();
        button.gameObject.SetActive(false);
        button = GameObject.Find("Button_Restart").GetComponent<Button>();
        button.gameObject.SetActive(false);
        SetTrackingTarget(GameObject.Find("Button_Start"));
    }

    // Update is called once per frame
    void Update() // not updating the position in update due to competition
    {
        
    }

    private void FixedUpdate()
    {
        DesiredPosition = obj.transform.position + offset;
        DesiredPosition.x = Mathf.Max(-2.5f, DesiredPosition.x);
        DesiredPosition.x = Mathf.Min(2.5f, DesiredPosition.x);
        DesiredPosition.y = -1;
        Vector3 SmoothedPosition = Vector3.Lerp(transform.position, DesiredPosition, SmoothSpeed); // 0 first parameter 1 second parameter
        transform.position = SmoothedPosition;
    }

    public void SetTrackingTarget(GameObject gameObject)
    {
        Debug.Log("SetTrackingTarget:" + gameObject.name);
        obj = gameObject;
    }
}
