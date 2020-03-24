using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    Rigidbody rigidBody;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {   
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal")*Time.deltaTime,5f,5f);
        transform.Translate(Input.GetAxis("Vertical")*Time.deltaTime,5f,5f);
        Thrust();
        Rotate();
    }
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                // do nothing
                print("OK"); //todo remove
                break;
            case "Fuel":
                print("Fuel"); //todo remove
                break;
            default:
                print("Dead");
                // kill player
                break;
        }
    }
    void Thrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            if (!audioSource.isPlaying) // so it doesn't layer
            {
                audioSource.Play();
            }
            }
        else
        {
            audioSource.Stop();
        }
    }
    void Rotate()
    {
        rigidBody.freezeRotation = true;//take manual control of rotation
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        rigidBody.freezeRotation = true;//resume physics control of rotation
    }    
}
