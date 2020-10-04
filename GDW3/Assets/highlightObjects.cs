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

    private Vector3 startPosition;
    private int currentAxis = 0;
    private bool middleMouseClicked = false;
    private Vector3 mouseStartPosition;
    private int currentDisplacement = 0;

    private Vector3 mouseOffset;
    float mouseZoffPut;


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

            Ray aRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitStuff;

            if (Physics.Raycast(aRay, out hitStuff))
            {
                if (hitStuff.transform)
                {
                    if (hitStuff.transform.gameObject.name == gameObject.name)
                    {
                        isClicked = true;
                    }
                    else if (!Input.GetKey(KeyCode.LeftShift))
                    {
                        isClicked = false;
                    }
                }
            }




            //if (isOnMouse == false)
            //{
            //    isClicked = false; //Deselect the object
            //}
            //
            //isOnMouse = false;
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
        
        if (Input.GetKey(KeyCode.Mouse2) && isClicked)
        {

            //This will be a drag option for quick placement
            if (middleMouseClicked == false)
            {
                startPosition = transform.position;
                middleMouseClicked = true;
                mouseStartPosition = Input.mousePosition;
            }
            else
            {
                switch (currentAxis)
                {
                    case 0: //z axis
                        int theNewDisplacement = (int)((Input.mousePosition.y - mouseStartPosition.y) < 0 ? Mathf.Ceil((Input.mousePosition.y - mouseStartPosition.y) / 50) : Mathf.Floor((Input.mousePosition.y - mouseStartPosition.y) / 50));
                        if (theNewDisplacement > currentDisplacement && currentDisplacement >= 0)
                        {
                            GameObject cloneOfObject = Instantiate(gameObject) as GameObject;
                            cloneOfObject.transform.position = transform.position + new Vector3(0,0, GetComponent<Renderer>().bounds.size.z + GetComponent<Renderer>().bounds.size.z * currentDisplacement);
                            gameObject.name += Mathf.Pow(currentDisplacement * currentAxis * currentAxis * currentAxis, 3);
                            currentDisplacement = theNewDisplacement;
                        }
                        else if (theNewDisplacement < currentDisplacement && currentDisplacement <= 0)
                        {
                            GameObject cloneOfObject = Instantiate(gameObject) as GameObject;
                            cloneOfObject.transform.position = transform.position + new Vector3(0, 0, -GetComponent<Renderer>().bounds.size.z + GetComponent<Renderer>().bounds.size.z * currentDisplacement);
                            gameObject.name += Mathf.Pow(currentDisplacement * currentAxis * currentAxis * currentAxis, 3);
                            currentDisplacement = theNewDisplacement;
                        }
                        break;

                    case 1: //x axis
                        int theNewDisplacement2 = (int)((Input.mousePosition.x - mouseStartPosition.x) < 0 ? Mathf.Ceil((Input.mousePosition.x - mouseStartPosition.x) / 100) : Mathf.Floor((Input.mousePosition.x - mouseStartPosition.x) / 100));
                        if (theNewDisplacement2 > currentDisplacement)
                        {
                            currentDisplacement = theNewDisplacement2;
                            GameObject cloneOfObject = Instantiate(gameObject) as GameObject;
                            gameObject.name += Mathf.Pow(currentDisplacement * currentAxis * currentAxis * currentAxis, 3);
                            cloneOfObject.transform.position = transform.position + new Vector3(GetComponent<Renderer>().bounds.size.x * currentDisplacement, 0, 0);
                        }
                        else if (theNewDisplacement2 < currentDisplacement && currentDisplacement <= 0)
                        {
                            GameObject cloneOfObject = Instantiate(gameObject) as GameObject;
                            cloneOfObject.transform.position = transform.position + new Vector3(GetComponent<Renderer>().bounds.size.x * currentDisplacement, 0, 0);
                            gameObject.name += Mathf.Pow(currentDisplacement * currentAxis * currentAxis * currentAxis, 3);
                            currentDisplacement = theNewDisplacement2;
                        }
                        break;

                    case 2: //y axis
                        int theNewDisplacement3 = (int)((Input.mousePosition.y - mouseStartPosition.y) < 0 ? Mathf.Ceil((Input.mousePosition.y - mouseStartPosition.y) / 100) : Mathf.Floor((Input.mousePosition.y - mouseStartPosition.y) / 100));
                        if (theNewDisplacement3 > currentDisplacement)
                        {
                            currentDisplacement = theNewDisplacement3;
                            GameObject cloneOfObject = Instantiate(gameObject) as GameObject;
                            gameObject.name += Mathf.Pow(currentDisplacement * currentAxis * currentAxis * currentAxis, 3);
                            cloneOfObject.transform.position = transform.position + new Vector3(0, GetComponent<Renderer>().bounds.size.y * currentDisplacement, 0);
                        }
                        else if (theNewDisplacement3 < currentDisplacement && currentDisplacement <= 0)
                        {
                            GameObject cloneOfObject = Instantiate(gameObject) as GameObject;
                            cloneOfObject.transform.position = transform.position + new Vector3(0, GetComponent<Renderer>().bounds.size.y * currentDisplacement, 0);
                            gameObject.name += Mathf.Pow(currentDisplacement * currentAxis * currentAxis * currentAxis, 3);
                            currentDisplacement = theNewDisplacement3;
                        }
                        break;
                }
            }

        }
        else
        {
            currentDisplacement = 0;
            middleMouseClicked = false;
        }
        
        if (Input.GetKey(KeyCode.Z))
        {
            currentAxis = 0;
        }
        else if (Input.GetKey(KeyCode.X))
        {
            currentAxis = 1;
        }
        else if(Input.GetKey(KeyCode.Y))
        {
            currentAxis = 2;
        }
        

        if (isClicked && Input.GetKey(KeyCode.Space))
        {
            //movement of object
        }


    }

    //private void OnMouseDown()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        isClicked = true;
    //        isOnMouse = true;
    //    }
    //}
}
