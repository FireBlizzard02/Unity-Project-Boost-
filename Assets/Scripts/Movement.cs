using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem leftThrusterParticle;
    [SerializeField] ParticleSystem rightThrusterParticle;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrusting();
        ProcessRotation();
    }

    void ProcessThrusting()
    {
       if(Input.GetKey(KeyCode.Space)){
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if(!mainEngineParticle.isPlaying){
            mainEngineParticle.Play();}
            if(!audioSource.isPlaying){
            audioSource.PlayOneShot(mainEngine);}
       }
       else{
        audioSource.Stop();
        mainEngineParticle.Stop();
       }
    }

    void ProcessRotation(){
        if(Input.GetKey(KeyCode.D)){
        Debug.Log("Going Right");
        ApplyRotation(-rotationThrust);
        if(!leftThrusterParticle.isPlaying){
        leftThrusterParticle.Play();}
       }
       else if(Input.GetKey(KeyCode.A)){
        Debug.Log("Going Left");
        ApplyRotation(rotationThrust);
        if(!rightThrusterParticle.isPlaying){
        rightThrusterParticle.Play();
       }
       else{
        leftThrusterParticle.Stop();
        rightThrusterParticle.Stop();
       }
    }
    void ApplyRotation(float rotationthisframe){
       rb.freezeRotation = true;  // freezing the rotation manually
       transform.Rotate(Vector3.forward * rotationthisframe * Time.deltaTime);
       rb.freezeRotation = false; // unfreezing the rotation manually
    }
}
}
