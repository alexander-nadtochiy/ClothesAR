using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRotator : MonoBehaviour {

    byte rotatorLeft = 0, rotatorRight = 0;
    float modelPosition = -1.2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	/*void Update () { // Update метод выполняется каждый кадр
		
	}*/

    public void RotatorChangeLeft()
    {
        if (transform.position == new Vector3(transform.position.x, transform.position.y, modelPosition)) // координаты на сцене отличаются (на Scene z = 17)
        {
            if (rotatorLeft == 1)
            {
                rotatorLeft = 0;
            }
            else if (rotatorLeft == 0)
            {
                rotatorLeft = 1;
            }
            if (rotatorLeft == 1 && rotatorRight == 1)
            {
                rotatorRight = 0;
                rotatorLeft = 0;
            }
        }
    }
    public void RotatorChangeRight()
    {
        if (transform.position == new Vector3(transform.position.x, transform.position.y, modelPosition))
        {
            if (rotatorRight == 1)
            {
                rotatorRight = 0;
            }
            else if (rotatorRight == 0)
            {
                rotatorRight = 1;
            }
            if (rotatorRight == 1 && rotatorLeft == 1)
            {
                rotatorLeft = 0;
                rotatorRight = 0;
            }
        }
    }

    public void RotatorStop()
    {
        rotatorLeft = 0;
        rotatorRight = 0;
    }

    void FixedUpdate() // или Update
    {
        if (rotatorLeft == 1)
        {
            Quaternion rotationY = Quaternion.AngleAxis(1, new Vector3(0, 0, -1)); // x, y, z
            transform.rotation *= rotationY;
        }
        else if (rotatorRight == 1)
        {
            Quaternion rotationY = Quaternion.AngleAxis(1, new Vector3(0, 0, 1));
            transform.rotation *= rotationY;
        }
    }
}
