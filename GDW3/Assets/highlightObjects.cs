using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highlightObjects : MonoBehaviour
{
    //Need to make a shader for the outline to work
    Color colour = new Color(0, 1, 1, 1);
    MeshRenderer render;
    private bool isClicked = false;
    private bool isOnMouse = false;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (isOnMouse == false)
            {
                isClicked = false; //Deselect the object
            }
           
            isOnMouse = false;
        }

        if (isClicked)
        {
            //render.material.SetFloat("outline", 1.0f);
            //render.material.SetColor("outline", colour);
        }
        else
        {
            //render.material.SetFloat("outline", 0.0f);
        }

        if (Input.GetKey(KeyCode.Delete) && isClicked)
        {
            Destroy(gameObject);
        }
        else if (Input.GetMouseButtonDown(3) && isClicked)
        {
            //This will be a drag option for quick placement
            //Deal with it tomorrow
        }
        

    }
    private void OnMouseDown()
    {
        if (Input.GetMouseButton(0))
        {
            isClicked = true;
            isOnMouse = true;
        }
    }
}
