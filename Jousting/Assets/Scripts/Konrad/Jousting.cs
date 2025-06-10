using System.Collections;
using UnityEngine;

public class Jousting : MonoBehaviour
{

    public bool jousting = true;
    [SerializeField] Sprite hitSprite;
    [SerializeField] private float damage = 10;
    [SerializeField] public float multiplier;
    [SerializeField] float cooldown;
    float timer = 0;
    [SerializeField] private InputSystem_Actions actions;
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] float joustRange;
    [SerializeField] LayerMask layerMask;
    RectTransform rectTransform;
    bool hitAnimation = false;

    private void Awake()
    {
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

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (!hitAnimation && jousting && timer > cooldown && (actions.Player.AButton.IsPressed() || actions.Player.BButton.IsPressed()) && !(actions.Player.AButton.IsPressed() && actions.Player.BButton.IsPressed()))
        {
            MoveRod();
            rectTransform.anchoredPosition = new Vector3(rectTransform.anchoredPosition.x, -10, 0);
            RaycastHit hit;

            Vector3 dir = (transform.position - Camera.main.transform.position).normalized;
            
            Physics.Raycast(Camera.main.transform.position, dir, out hit, joustRange, layerMask);

            Debug.DrawRay(Camera.main.transform.position, dir);

            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<EnemyJousting>().GetJousted(damage * multiplier);
                timer = 0;
                StartCoroutine(HitSomeone());
            }
        }
        else if(!hitAnimation && actions.Player.AButton.IsPressed() && actions.Player.BButton.IsPressed())
        {
            rectTransform.anchoredPosition = new Vector3(rectTransform.anchoredPosition.x, -95, 0);
        }
        else if (!hitAnimation && jousting && timer > cooldown && (!actions.Player.AButton.IsPressed() && !actions.Player.BButton.IsPressed()))
        {
            rectTransform.anchoredPosition = new Vector3(rectTransform.anchoredPosition.x, -50, 0);
        }
        else if (!hitAnimation && jousting && timer < cooldown)
        {
            rectTransform.anchoredPosition = new Vector3(rectTransform.anchoredPosition.x, -70, 0);
        }
        else if (!hitAnimation)
        {
            rectTransform.anchoredPosition = new Vector3(rectTransform.anchoredPosition.x, -95, 0);
        }
    }

    public void MoveRod()
    {
        if (actions.Player.AButton.IsPressed() && !actions.Player.BButton.IsPressed())
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x - (150 * Time.deltaTime), rectTransform.anchoredPosition.y);
        }
        else if (!actions.Player.AButton.IsPressed() && actions.Player.BButton.IsPressed())
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + (150 * Time.deltaTime), rectTransform.anchoredPosition.y);
        }

        rectTransform.anchoredPosition = new Vector2(Mathf.Clamp(rectTransform.anchoredPosition.x, -100, 100), rectTransform.anchoredPosition.y);
    }

    IEnumerator HitSomeone()
    {
        AudioManager.instance.EnemyHit();
        hitAnimation = true;
        rectTransform.anchoredPosition = new Vector3(rectTransform.anchoredPosition.x, -10, 0);

        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        hitAnimation = false;
    }
}
