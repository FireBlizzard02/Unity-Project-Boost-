using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float levelloaddelay = 1.5f;
    [SerializeField] AudioClip CrashSound;
    [SerializeField] AudioClip SuccessSound;
    
    [SerializeField] ParticleSystem SuccessParticle;
    [SerializeField] ParticleSystem CrashParticle;

    AudioSource audioSource;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning){
            return;
        }
        switch(other.gameObject.tag){
            
            case "Friendly":
                Debug.Log("Collide at Launch Pad");
                break;

            case "Fuel":
                Debug.Log("Collide at Fuel");
                break;

            case "Finish":
                Debug.Log("Reached Finish Point");
                StartSuccessSequence();
                break;

            default:
                StartCrashSequence();
                break;
             
        }

    }

    void StartSuccessSequence(){
        isTransitioning = true;
        SuccessParticle.Play();
        audioSource.PlayOneShot(SuccessSound);
        GetComponent<Movement>().enabled = false;
        Invoke("NextScene" , levelloaddelay);
    }

    void StartCrashSequence(){
        isTransitioning = true;
        CrashParticle.Play();
        audioSource.PlayOneShot(CrashSound);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", levelloaddelay);
    }

    void ReloadScene(){
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentSceneIndex);
    }

    void NextScene(){
        int NextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(NextSceneIndex == SceneManager.sceneCountInBuildSettings){
            NextSceneIndex = 0;
            SceneManager.LoadScene(NextSceneIndex); 
        }
        else{
            SceneManager.LoadScene(NextSceneIndex);
        }
    }

}
