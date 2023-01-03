using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D Rg;
    [SerializeField]
    private int MainThrust,RotationSpeed;
    [SerializeField]
    AudioClip mainEngine;
    AudioSource sound;
    [SerializeField]
    ParticleSystem mainThrust,leftThrust,rightThrust;
    // Start is called before the first frame update
    void Start()
    {
        Rg = gameObject.GetComponent<Rigidbody2D>();
        sound = gameObject.GetComponent<AudioSource>();
    }
   
    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        ProcessRotation();
    }
   public void ProcessInput()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }

    }

    

    public void StartThrusting()
    {
        Rg.AddRelativeForce(Vector3.up * Time.deltaTime * MainThrust);

        if (!sound.isPlaying)
        {
            sound.PlayOneShot(mainEngine);
        }
        if (!mainThrust.isPlaying)
        {
            mainThrust.Play();
        }
    }
    public void StartRotating()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();

        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();

        }
        else
        {
            StopRotating();

        }
    }

    private void StopRotating()
    {
        rightThrust.Stop();
        leftThrust.Stop();
    }

    void StopThrusting()
    {
        sound.Stop();
        mainThrust.Stop();
    }

    void ProcessRotation()
    {
        StartRotating();
    }

 

   public void RotateRight()
    {
        ApplyRotation(-RotationSpeed);
        if (!leftThrust.isPlaying)
        {
            leftThrust.Play();
        }
    }

   public void RotateLeft()
    {
        if (!rightThrust.isPlaying)
        {
            leftThrust.Play();
        }
        ApplyRotation(RotationSpeed);
    }

    void ApplyRotation(float rotationThisFrame)
    {
        Rg.freezeRotation = true; // freezing rotation so we can rotate manually
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        Rg.freezeRotation = false; // unfreeze cuz physic system can work
    }
}
