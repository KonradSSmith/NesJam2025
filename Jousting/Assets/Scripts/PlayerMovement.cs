using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [Header("Basic Movement")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float driftingAcceleration;
    private float boostAcceleration = 1;
    private Rigidbody rb;
    private Vector2 moveInput;
    bool reversing = false;

    [Header("Sprites")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite horseForward;
    [SerializeField] Sprite horseTurn1;
    [SerializeField] Sprite horseTurn2;
    [SerializeField] Sprite horseTurn3;

    [SerializeField] private InputSystem_Actions actions;

    [Header("Camera")]
    [SerializeField] private float defaultCameraRotationAmount;
    [SerializeField] private float driftCameraTurnAmount;

    [Header("Drifting")]
    [SerializeField] private float sharpDriftCameraRotationAmount;
    [SerializeField] private float wideDriftCameraRotationAmount;
    bool driftingLeft;
    bool driftingRight;
    bool waitingToDrift;
    private Coroutine driftCoroutine;
    float timeDrifting = 0;
    float _timer = 0;

    [Header("Drifting Boosts")]
    [SerializeField] private float timeUntilBoost1;
    [SerializeField] private float boostAmount1;
    [SerializeField] private float boostTime1;
    [SerializeField] private float timeUntilBoost2;
    [SerializeField] private float boostAmount2;
    [SerializeField] private float boostTime2;
    bool boosting;
    private Coroutine boostCoroutine;

    private void Awake()
    {
        actions = new InputSystem_Actions();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        actions.Player.Enable();
    }

    private void OnDisable()
    {
        actions.Player.Disable();
    }

    private void FixedUpdate()
    {
        //get wasd vector
        moveInput = actions.Player.Move.ReadValue<Vector2>();
        if (driftingLeft)
        {
            moveInput.x -= driftCameraTurnAmount;
        }
        else if (driftingRight)
        {
            moveInput.x += driftCameraTurnAmount;
        }
        //check if reversing
        if (moveInput.y > 0)
        {
            reversing = false;
        }
        else if (moveInput.y < 0 && !boosting)
        {
            reversing = true;
        }

        //add forward/backward velocity
        if (boosting && moveInput.y < 0)
        {
            //if reversing while boosting, still move forward but slower
            rb.AddForce((transform.forward * moveInput.y) * acceleration * (0.4f * boostAcceleration));
        }
        else
        {
            rb.AddForce((transform.forward * moveInput.y) * acceleration * boostAcceleration);
        }
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);

        /*
        //invert steering if reversing
        switch (reversing)
        {
            case false:
                if (moveInput.x > 0 && driftingRight || moveInput.x < 0 && driftingLeft)
                {
                    transform.Rotate(0, moveInput.x * sharpDriftCameraRotationAmount, 0);
                }
                else if (moveInput.x < 0 && driftingRight || moveInput.x > 0 && driftingLeft)
                {
                    transform.Rotate(0, moveInput.x * wideDriftCameraRotationAmount, 0);
                }
                else
                {
                    transform.Rotate(0, moveInput.x * defaultCameraRotationAmount * boostAcceleration, 0);
                }
                break;
            case true:
                transform.Rotate(0, -moveInput.x * defaultCameraRotationAmount * boostAcceleration, 0);
                break;
        }
        */
        if (moveInput.x > 0 && driftingRight || moveInput.x < 0 && driftingLeft)
        {
            transform.Rotate(0, moveInput.x * sharpDriftCameraRotationAmount, 0);
        }
        else if (moveInput.x < 0 && driftingRight || moveInput.x > 0 && driftingLeft)
        {
            transform.Rotate(0, moveInput.x * wideDriftCameraRotationAmount, 0);
        }
        else
        {
            transform.Rotate(0, moveInput.x * defaultCameraRotationAmount * boostAcceleration, 0);
        }
    }

    private void Update()
    {
        if (!PlacementManager.instance.racing && moveInput != Vector2.zero)
        {
            PlacementManager.instance.racing = true;
        }
        CheckDrift();
        SetSprite();
    }

    private void SetSprite()
    {
        if (moveInput.x == 0)
        {
            spriteRenderer.sprite = horseForward;
        }
        else if(moveInput.x > 0)
        {
            spriteRenderer.flipX = true;
            spriteRenderer.sprite = horseTurn1;
        }
        else
        {
            spriteRenderer.flipX = false;
            spriteRenderer.sprite = horseTurn1;
        }

        if (driftingLeft || driftingRight)
        {
            _timer += Time.deltaTime;
            if (driftingLeft)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }

            if (_timer > timeUntilBoost2)
            {
                //boost2
                spriteRenderer.sprite = horseTurn3;
            }
            else if (_timer > timeUntilBoost1)
            {
                //boost1
                spriteRenderer.sprite = horseTurn2;
            }
        }
    }

    private void CheckDrift()
    {
        if (!driftingLeft && !driftingRight && actions.Player.AButton.IsPressed() && actions.Player.BButton.IsPressed() && !waitingToDrift)
        {
            waitingToDrift = true;
        }
        if (waitingToDrift)
        {
            if (moveInput.x != 0)
            {
                driftCoroutine = StartCoroutine(Drift());
            }
            if (moveInput.x > 0)
            {
                waitingToDrift = false;
                driftingLeft = false;
                driftingRight = true;
            }
            else if (moveInput.x < 0)
            {
                waitingToDrift = false;
                driftingLeft = true;
                driftingRight = false;
            }
        }
    }

    IEnumerator Drift()
    {
        bool drifting = true;
        while (drifting)
        {
            timeDrifting += Time.deltaTime;
            if (!actions.Player.AButton.IsPressed() || !actions.Player.BButton.IsPressed())
            {
                waitingToDrift = false;
                driftingLeft = false;
                driftingRight = false;
                drifting = false;
                if (boostCoroutine != null)
                {
                    StopCoroutine(boostCoroutine);
                }
                boostCoroutine = StartCoroutine(DriftBoost(timeDrifting));
                timeDrifting = 0;
                _timer = 0;
                StopCoroutine(driftCoroutine);
            }
            yield return null;
        }
        yield return null;
    }

    IEnumerator DriftBoost(float time)
    {
        _timer = 0;
        float boostTime = 0;
        if (time > timeUntilBoost2)
        {
            //boost2
            boosting = true;
            boostAcceleration = boostAmount2;
            boostTime = boostTime2;
        }
        else if (time > timeUntilBoost1)
        {
            //boost1
            boosting = true;
            boostAcceleration = boostAmount1;
            boostTime = boostTime1;
        }

        float timer = 0;
        while (timer < boostTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        boosting = false;
        boostAcceleration = 1;

        yield return null;
    }
}
