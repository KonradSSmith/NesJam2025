using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    [SerializeField] AudioClip driftBoost1;
    [SerializeField] AudioClip driftBoost2;
    [SerializeField] AudioClip driftCharge1;
    [SerializeField] AudioClip driftCharge2;
    [SerializeField] AudioClip enemyDisappear;
    [SerializeField] AudioClip enemyHit;
    [SerializeField] AudioClip playerHit;
    [SerializeField] AudioClip playerDie;
    [SerializeField] AudioClip LapStinger;

    [SerializeField] AudioSource footsteps;
    bool footstepsOn = true;
    [SerializeField] AudioSource drifting;
    bool driftingOn = true;
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource lapSource;

    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] AudioSource audioSource;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (footstepsOn && !(playerMovement.driftingLeft || playerMovement.driftingRight) && playerMovement.moveInput != Vector2.zero)
        {
            footsteps.mute = false;
        }
        else
        {
            footsteps.mute = true;
        }
        if (driftingOn && (playerMovement.driftingLeft || playerMovement.driftingRight))
        {
            drifting.mute = false;
        }
        else
        {
            drifting.mute = true;
        }
    }

    public void EnemyHit()
    {
        audioSource.PlayOneShot(enemyHit);
    }

    public void PlayerHit()
    {
        audioSource.PlayOneShot(playerHit);
    }

    public void EnemyKilled()
    {
        audioSource.PlayOneShot(enemyDisappear);
    }

    public IEnumerator LapSound()
    {
        music.mute = true;
        lapSource.PlayOneShot(LapStinger);
        yield return new WaitForSeconds(LapStinger.length);
        music.mute = false;
    }

    public IEnumerator DriftCharge1()
    {
        footstepsOn = false;
        driftingOn = false;

        audioSource.Stop();
        audioSource.PlayOneShot(driftCharge1);
        yield return new WaitForSeconds(0.4f);

        footstepsOn = true;
        driftingOn = true;
        yield return null;
    }

    public IEnumerator DriftCharge2()
    {
        footstepsOn = false;
        driftingOn = false;

        audioSource.Stop();
        audioSource.PlayOneShot(driftCharge2);
        yield return new WaitForSeconds(0.4f);

        footstepsOn = true;
        driftingOn = true;
        yield return null;
    }

    public IEnumerator DriftBoost1()
    {
        footstepsOn = false;
        driftingOn = false;
        audioSource.Stop();

        audioSource.PlayOneShot(driftBoost1);
        yield return new WaitForSeconds(driftBoost1.length);

        footstepsOn = true;
        driftingOn = true;
        yield return null;
    }

    public IEnumerator DriftBoost2()
    {
        footstepsOn = false;
        driftingOn = false;
        audioSource.Stop();

        audioSource.PlayOneShot(driftBoost2);
        yield return new WaitForSeconds(driftBoost2.length);

        footstepsOn = true;
        driftingOn = true;
        yield return null;
    }
}
