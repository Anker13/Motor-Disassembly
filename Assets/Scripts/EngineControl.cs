using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EngineControl : MonoBehaviour
{
    private Transform rsengine; //Required Component to rotate and scale the object
    private bool rotate; //True = Engine can be rotated, False = Engine can not be rotated
    private bool scale;//True = Engine can be scaled, False = Engine can not be scaled
    private Vector3 origin;//The Origin of the Engine
    private List<Vector3> backupChilds;//The Backup of the Children Objects so we can later take them back to their beginning position
    // Start is called before the first frame update
   
    void Start()
    {
        rsengine = GetComponent<Transform>();
        origin = rsengine.position;
        rotate = false;
        scale = false;
        backupChilds = new List<Vector3>();
        for(int i=0; i< transform.childCount; i++)
        {
            backupChilds.Add(transform.GetChild(i).transform.position);
        }
        
    }

    // Update is called once per frame
    void Update()
    {     
        if (rotate)//Rotation of Engine
        {
            Rotate();      
        }
        if (scale)//Scalation of Engine
        {
            Scale();
        }
    }
    //Disassembly (Explode) the Engine
    public void setRotate()
    {
        if (rotate)
        {
            rotate = false;
        }
        else if (!rotate)
        {
            rotate = true;
            scale = false;
        }
    }
    public void setScale()
    {
        if (scale)
        {
            scale = false;
        }
        else if (!scale)
        {
            scale = true;
            rotate = false;
        }
    }
    public void Reset()
    {
        rsengine.rotation = new Quaternion(0f, 0f, 0f, 0f);
        rsengine.localScale = new Vector3(1f, 1f, 1f);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).position = backupChilds[i];
        }
    }
    public void DisassemblyEngine()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 direction = (transform.GetChild(i).position - origin).normalized;
            transform.GetChild(i).transform.position += direction * Time.deltaTime * 10.0f;
        }
    }
    public void Scale()
    {
        if (Input.GetMouseButton(1)) //Scale: Left Mouse Button
        {
            if (Input.GetAxisRaw("Mouse X") < 0f)//Negative
                rsengine.localScale = new Vector3(rsengine.localScale.x - 0.1f, rsengine.localScale.y - 0.1f, rsengine.localScale.z - 0.1f);
            if (Input.GetAxisRaw("Mouse X") > 0f)//Positive
                rsengine.localScale = new Vector3(rsengine.localScale.x + 0.1f, rsengine.localScale.y + 0.1f, rsengine.localScale.z + 0.1f);
        }
    }

    public void Rotate()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f) //Positive Rotation on y Axis
        {
            rsengine.Rotate(0f, 1f, 0f);
        }
        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f) // Negative Rotation on y Axis
        {
            rsengine.Rotate(0f, -1f, 0f);
        }
        if (Input.GetMouseButton(1)) //Rotation on X-Axis: Right Mouse Button
        {
            if (Input.GetAxisRaw("Mouse X") < 0f)//Negative
            {
                rsengine.Rotate(-1f, 0f, 0f);
            }
            if (Input.GetAxisRaw("Mouse X") > 0f)//Positive
            {
                rsengine.Rotate(1f, 0f, 0f);
            }
        }
        if (Input.GetMouseButton(0)) //Rotation on Z-Axis: Left Mouse Button
        {
            if (Input.GetAxisRaw("Mouse Y") < 0f) //Negative
            {
                rsengine.Rotate(0f, 0f, -1f);
            }
            if (Input.GetAxisRaw("Mouse Y") > 0f)//Positive
            {
                rsengine.Rotate(0f, 0f, 1f);
            }
        }
    }
}
