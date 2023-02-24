using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMechanic : MonoBehaviour
{

    public GameObject rotateObject;
    public float speed;
    public bool isRotating = false;
    public int rotateDegrees;
    Vector3 rotation = new Vector3(0, 0, 0);

    public AudioSource hitSound;
    public AudioSource hitBrickSound;
    private bool isOnBrick;
    private Vector3 initPosition;

    private void Rotate(int rotateDegrees)
    {
        var step = speed * Time.deltaTime;
        rotateObject.transform.rotation = Quaternion.RotateTowards(rotateObject.transform.rotation, Quaternion.Euler(rotation), step);
        if (rotateObject.transform.rotation == Quaternion.Euler(rotation))
        {
            isRotating = false;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume=0f;
        StartCoroutine(Wait());
        initPosition=transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotating)
        {
            Rotate(rotateDegrees);
        }
    }

    public void OnMouseDown()
    {
        rotation = new Vector3(0, 0, rotation.z + 90);
        isRotating = true;

    }

    public void OnMouseDownRight()
    {
        rotation = new Vector3(0, 0, rotation.z - 90);
        isRotating = true;

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Win")
        {
            rotateObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.position = initPosition;

        }
        else if(other.gameObject.tag == "Edges")
        {
            hitSound.Play();
            isOnBrick=false;
        }

        else if(other.gameObject.tag=="Bricks")
        {
            if(!isOnBrick)
            hitBrickSound.Play();
            isOnBrick=true;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        AudioListener.volume = 1.0f;
    }

}
