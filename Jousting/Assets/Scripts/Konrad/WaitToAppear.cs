using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitToAppear : MonoBehaviour
{
    [SerializeField] float timeToWait;
    SpriteRenderer spriteRenderer;
    float timer = 0;
    private InputSystem_Actions actions;
    public bool needtoClick = true;
    public bool loadScene = true;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeToWait)
        {
            
            if (needtoClick)
            {
                spriteRenderer.enabled = true;
                if (actions.Player.AButton.triggered && actions.Player.BButton.IsPressed() || actions.Player.BButton.triggered && actions.Player.AButton.IsPressed())
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
            else
            {
                if (loadScene)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else
                {
                    spriteRenderer.enabled = true;
                }
            }
        }
    }
}
