using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]
    private float nextLevelTime;
    [SerializeField]
    AudioClip successClip,crashClip;
    AudioSource successSound,crashSound;
    [SerializeField]
    ParticleSystem successParticles;
    [SerializeField]
    ParticleSystem crashParticles;

    bool isTransitioning = false;
    bool collisionDisabled = false;
    void Start()
    {
        successSound = gameObject.GetComponent<AudioSource>();
        crashSound = gameObject.GetComponent<AudioSource>();
    }
    void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
       else if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
    }

    void OnCollisionEnter(Collision collision)
    {


        if(isTransitioning || collisionDisabled )
        {
            return;    //direkt olarak kodu atlýyor 

        }
       
        switch (collision.gameObject.tag)
        {
            case "Finish" :
                Debug.Log("Congrats, yo, you finished!");
                StartSuccessSequence();
                break;

            case "Friendly":
                Debug.Log("This thing is friendly");
                break;

            default:
                Debug.Log("Sorry, you blew up!");
                StartCrashSequence();
                break;
        }
    }
    void StartSuccessSequence()
    {
            isTransitioning = true;
            successSound.Stop();
            successSound.PlayOneShot(successClip);
            successParticles.Play();
            GetComponent<Movement>().enabled = false;

            Invoke("LoadNextLevel", nextLevelTime);
            
        
    }
    void StartCrashSequence()
    {
            isTransitioning = true;
            crashSound.Stop();
            crashSound.PlayOneShot(crashClip);
            crashParticles.Play();
            gameObject.GetComponent<Movement>().enabled = false;

            Invoke("ReloadLevel", 3f);
          
       
        
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentSceneIndex + 1;
        if(nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }
}
