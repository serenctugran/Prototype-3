using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float playerJump = 10.0f;
    public float gravityModifier;
    private Animator playerAnim;
    public ParticleSystem explosionPartical;
    public ParticleSystem dirkPartical;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playAudio;
    public bool isOnGround = true;

    public bool gameOver;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playAudio = GetComponent<AudioSource>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * playerJump, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirkPartical.Stop();
            playAudio.PlayOneShot(jumpSound, 0.6f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirkPartical.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            dirkPartical.Stop();
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionPartical.Play();
            playAudio.PlayOneShot(crashSound, 1.2f);
            
        }
    }
}
