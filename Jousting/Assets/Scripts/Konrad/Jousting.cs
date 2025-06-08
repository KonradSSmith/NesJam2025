using UnityEngine;

public class Jousting : MonoBehaviour
{

    public bool jousting = true;
    [SerializeField] Sprite hitSprite;

    [SerializeField] float joustRange;
    [SerializeField] LayerMask layerMask;
    RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (jousting)
        {
            RaycastHit hit;

            Vector3 dir = (transform.position - Camera.main.transform.position).normalized * joustRange;
            
            Physics.Raycast(Camera.main.transform.position, dir, out hit, Mathf.Infinity, layerMask);

            Debug.DrawRay(Camera.main.transform.position, dir);

            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<AgentDriving>().Jousted();
            }
        }
    }
}
