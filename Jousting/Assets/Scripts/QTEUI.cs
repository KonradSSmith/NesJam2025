using System.Collections;
using System.Timers;
using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;

public class QTEUI : MonoBehaviour
{
    public static QTEUI instance { get; private set; }

    public RectTransform pointA; // Reference to the starting point
    public RectTransform pointB; // Reference to the ending point
    public RectTransform safeZone; // Reference to the safe zone RectTransform
    public float moveSpeed = 100f; // Speed of the pointer movement
    public GameObject holder;
    public GameObject indicatorObject1;
    public GameObject indicatorObject2;
    public float cooldownTime = 5;
    float _timer = 0;
    bool indicating = false;
    public bool level1 = false;

    private float direction = 1f; // 1 for moving towards B, -1 for moving towards A
    public RectTransform pointerTransform;
    private Vector3 targetPosition;
    public bool stopped = true;
    bool canGetJousted = true;
    private InputSystem_Actions actions;
    Coroutine checkForInputCoroutine;

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

        actions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        actions.Player.Enable();
    }

    private void OnDisable()
    {
        actions.Player.Disable();
    }

    void Start()
    {
        targetPosition = new Vector3(pointB.anchoredPosition.x, pointerTransform.anchoredPosition.y, 0);
        //pointerTransform.anchoredPosition = Vector3.MoveTowards(pointerTransform.anchoredPosition, targetPosition, moveSpeed * Time.deltaTime);
        //transform.localPosition = new Vector3(-20, 0, 0) * Time.deltaTime * moveSpeed;
        //pointerTransform.anchoredPosition = Vector3.MoveTowards(pointerTransform.anchoredPosition, targetPosition, moveSpeed * Time.deltaTime);
        //StartMinigame();
    }

    public void StartMinigame()
    {
        _timer = 0;
        int width = Random.Range(25, 70);
        int placement = Random.Range(-55, 55);
        safeZone.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 20);
        safeZone.GetComponent<SpriteRenderer>().size = new Vector2(width, 20);
        safeZone.anchoredPosition = new Vector2(placement, 0);
        moveSpeed = Random.Range(110, 210);
        stopped = false;
        canGetJousted = false;
        holder.SetActive(true);
        indicating = false;
    }

    void Update()
    {
        if (!stopped)
        {
            _timer += Time.deltaTime;
            /*
                    //pointerTransform.anchoredPosition = new Vector2(pointerTransform.anchoredPosition.x - (100 * Time.deltaTime), pointerTransform.anchoredPosition.y);
                    if (pointerTransform.anchoredPosition.x <= -100)
                    {
                        //go to pointB
                        Debug.Log("to pointB");
                        pointerTransform.anchoredPosition = new Vector2(pointerTransform.anchoredPosition.x + (100 * Time.deltaTime), pointerTransform.anchoredPosition.y);

                    }
                    else if(pointerTransform.anchoredPosition.x >= 98 )
                    {
                        //go to point A
                        Debug.Log("to pointA");
                        //pointerTransform.anchoredPosition = new Vector2(pointerTransform.anchoredPosition.x - (100 * Time.deltaTime), pointerTransform.anchoredPosition.y);

                    }
                    pointerTransform.anchoredPosition = new Vector2(Mathf.Clamp(pointerTransform.anchoredPosition.x, -100, 100), pointerTransform.anchoredPosition.y);
            */
            // Move the pointer towards the target position
            pointerTransform.anchoredPosition = Vector2.MoveTowards(pointerTransform.anchoredPosition, targetPosition, moveSpeed * Time.deltaTime);

            // Change direction if the pointer reaches one of the points
            if (Vector3.Distance(pointerTransform.anchoredPosition, pointA.anchoredPosition) < 0.1f)
            {
                targetPosition = new Vector3(pointB.anchoredPosition.x, pointerTransform.anchoredPosition.y, 0);
                direction = 1f;
                //pointerTransform.anchoredPosition = new Vector2(pointerTransform.anchoredPosition.x - (100 * Time.deltaTime), pointerTransform.anchoredPosition.y);

            }
            else if (Vector3.Distance(pointerTransform.anchoredPosition, pointB.anchoredPosition) < 0.1f)
            {
                targetPosition = new Vector3(pointA.anchoredPosition.x, pointerTransform.anchoredPosition.y, 0);
                direction = -1f;
                // pointerTransform.anchoredPosition = new Vector2(pointerTransform.anchoredPosition.x - (-100 * Time.deltaTime), pointerTransform.anchoredPosition.y);

            }
            // Check for input
            if (checkForInputCoroutine == null)
            {
                checkForInputCoroutine = StartCoroutine(checkForDrift());
            }
            if (_timer > 3.5f)
            {
                _timer = 0;
                stopped = true;
                StopCoroutine(checkForInputCoroutine);
                checkForInputCoroutine = null;
                StartCoroutine(PlayerMovement.instance.takeDamage());
                StartCoroutine(fade());

            }
        }
    }

    IEnumerator checkForDrift()
    {
        bool drifted = false;
        bool oneTriggered = false;

        while (!drifted)
        {
            if (actions.Player.AButton.triggered || actions.Player.BButton.triggered || oneTriggered)
            {
                oneTriggered = true;
                if (actions.Player.AButton.IsPressed() && actions.Player.BButton.triggered || actions.Player.BButton.IsPressed() && actions.Player.AButton.triggered)
                {
                    drifted = true;
                    CheckSuccess(true);
                }
            }
            yield return null;
        }

        checkForInputCoroutine = null;
        yield return null;
    }

    void CheckSuccess(bool succeed)
    {
        stopped = true;
        if (!succeed)
        {
            StartCoroutine(PlayerMovement.instance.takeDamage());
        }
        // Check if the pointer is within the safe zone
        else if (RectTransformUtility.RectangleContainsScreenPoint(safeZone, pointerTransform.position))
        {
            //success
            AudioManager.instance.JoustSucceed();
        }
        else
        {
            //fail
            StartCoroutine(PlayerMovement.instance.takeDamage());
        }

        StartCoroutine(fade());
    }

    public IEnumerator indicator(Transform enemyPosition)
    {
            if (canGetJousted)
            {
            StartCoroutine(AudioManager.instance.JoustIndicator());
            canGetJousted = false;
                float startingDistance = Vector3.Distance(enemyPosition.position, Camera.main.transform.position);

                GameObject indicatorToAppear;
                if (Random.Range(0, 2) == 0)
                {
                    indicatorToAppear = indicatorObject1;
                }
                else
                {
                    indicatorToAppear = indicatorObject2;
                }
                for (int i = 0; i < 5; i++)
                {
                    indicatorToAppear.SetActive(false);
                    yield return new WaitForSeconds(0.1f);
                    indicatorToAppear.SetActive(true);
                    yield return new WaitForSeconds(0.1f);
                }
                indicatorToAppear.SetActive(false);

                if (Mathf.Abs(Vector3.Distance(enemyPosition.position, Camera.main.transform.position) - startingDistance) < 15)
                {
                    if (!level1)
                {
                    StartMinigame();
                }
                }
                else
                {
                    StartCoroutine(AudioManager.instance.JoustEscape());
                    yield return new WaitForSeconds(1.5f);
                    canGetJousted = true;
                    
                }
            }
        yield return null;
    }

    IEnumerator fade()
    {
        for (int i = 0; i < 5; i++)
        {
            holder.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            holder.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }

        holder.SetActive(false);

        yield return new WaitForSeconds(Random.Range(4, 9));

        canGetJousted = true;
        indicating = false;

        yield return null;
    }
}