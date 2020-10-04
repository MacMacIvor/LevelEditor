using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiScript : MonoBehaviour
{
    private int down = 0;
    public GameObject objectToSpawnPrefab;
    public Transform pointPos;
    private float mouseWheel = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mouseWheel = Input.mouseScrollDelta.y;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (down == 0)
            {
                if (transform.position.x - 50 <= mousePos.x && transform.position.x + 50 >= mousePos.x && transform.position.y - 50 <= mousePos.y && transform.position.y + 50 >= mousePos.y)
                {
                    GameObject objects = Instantiate(objectToSpawnPrefab) as GameObject;
                    objects.name += mousePos.x * 0.1f * Random.Range(Random.Range(-1000000.0f, 0.0f), Random.Range(0.0f, 1000000.0f));
                    objects.transform.position = pointPos.transform.position;
                    objects.transform.parent = null;
                }
            }
            down = 1;
        }
        else
        {
            down = 0;
        }

        if (mouseWheel != 0 && Input.mousePosition.x < 200)
        {
            transform.position += new Vector3(0, mouseWheel * 10, 0);
        }
    }
}
