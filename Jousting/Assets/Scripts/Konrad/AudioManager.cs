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
    [SerializeField] AudioClip weakHit;
    [SerializeField] AudioClip youWin;
    [SerializeField] AudioClip JoustAlert;
    [SerializeField] AudioClip joustEscape;
    [SerializeField] AudioClip Intro;
    [SerializeField] AudioClip Tutorial;
    public bool tutorial = false;
    public bool intro = false;
    [SerializeField] AudioClip goHorn;

    [SerializeField] AudioSource footsteps;
    bool footstepsOn = true;
    [SerializeField] AudioSource drifting;
    bool driftingOn = true;
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource lapSource;
    [SerializeField] AudioSource joustingSource;

    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] AudioSource audioSource;

    bool indicating = false;
    bool animating = false;


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
        if (intro)
        {
            music.mute = true;
            audioSource.PlayOneShot(Intro);
        }
        else if (tutorial)
        {
            music.mute = true;
            audioSource.PlayOneShot(Tutorial);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!animating)
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
            if (QTEUI.instance.stopped == false)
            {
                music.mute = true;
                footsteps.mute = true;
                drifting.mute = true;
                joustingSource.mute = false;
            }
            else
            {
                joustingSource.mute = true;
            }
        }
    }

    public void EnemyHit()
    {
        audioSource.PlayOneShot(enemyHit);
    }

    public void EnemyHitZero()
    {
        audioSource.PlayOneShot(weakHit);
    }

    public IEnumerator YouWin()
    {
        if (!animating)
        {
            animating = true;
            music.mute = true;
            footsteps.mute = true;
            drifting.mute = true;
            joustingSource.mute = true;
            lapSource.mute = true;
            audioSource.Stop();
            yield return new WaitForSeconds(1);
            audioSource.PlayOneShot(youWin);
            yield return new WaitForSeconds(youWin.length);
            //music.mute = false;
            audioSource.mute = true;
        }

    }

    public IEnumerator YouLose()
    {
        if (!animating)
        {
            music.mute = true;
            footsteps.mute = true;
            drifting.mute = true;
            joustingSource.mute = true;
            lapSource.mute = true;
            animating = true;
            audioSource.Stop();
            audioSource.PlayOneShot(playerDie);
            yield return new WaitForSeconds(playerDie.length);
            //music.mute = false;   
            audioSource.mute = true;
        }
    }
    public IEnumerator GoHorn()
    {
        music.mute = true;
        lapSource.PlayOneShot(goHorn);
        yield return new WaitForSeconds(goHorn.length);
        music.mute = false;
    }

    public IEnumerator JoustEscape()
    {
        music.mute = true;
        lapSource.PlayOneShot(joustEscape);
        yield return new WaitForSeconds(joustEscape.length);
        music.mute = false;
    }

    public IEnumerator JoustIndicator()
    {
        if (!indicating)
        {
            indicating = true;
            music.mute = true;
            audioSource.PlayOneShot(JoustAlert);
            yield return new WaitForSeconds(JoustAlert.length);
            music.mute = false;
            indicating = false;
        }
    }

    public void JoustSucceed()
    {
        audioSource.PlayOneShot(joustEscape);
        music.mute = false;
    }

    public void PlayerHit()
    {
        music.mute = false;
        lapSource.Stop();
        audioSource.PlayOneShot(playerHit);
    }

    public void EnemyKilled()
    {
        audioSource.PlayOneShot(enemyDisappear);
    }

    public IEnumerator LapSound()
    {
        music.mute = true;
        joustingSource.mute = true;
        lapSource.PlayOneShot(LapStinger);
        yield return new WaitForSeconds(LapStinger.length);
        music.mute = false;
        joustingSource.mute = false;
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
