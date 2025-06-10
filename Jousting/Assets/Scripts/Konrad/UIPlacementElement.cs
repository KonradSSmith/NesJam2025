using UnityEngine;
using UnityEngine.UI;

public class UIPlacementElement : MonoBehaviour
{
    public PlacementChecker assignedChecker;
    [SerializeField] Image healthBar;
    bool assigned = false;
    bool dead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            if (PlacementManager.instance.racing)
            {
                if (!assigned)
                {
                    assignedChecker = PlacementManager.instance.horses[transform.GetSiblingIndex()];
                    assigned = true;
                }
                else
                {
                    transform.SetSiblingIndex(assignedChecker.intPlacement - 1);
                    healthBar.fillAmount = assignedChecker.health / 100;
                    
                    if (assignedChecker.health <= 0)
                    {
                        dead = true;
                    }
                }
            }
        }
        else
        {
            assignedChecker.transform.SetAsLastSibling();
        }
    }
}
