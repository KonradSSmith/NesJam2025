using UnityEngine;
using UnityEngine.UI;

public class UIPlacementElement : MonoBehaviour
{
    public PlacementChecker assignedChecker;
    [SerializeField] Image healthBar;
    bool assigned = false;
    bool dead = false;
    [SerializeField] Sprite times10;
    [SerializeField] Sprite times5;
    [SerializeField] Sprite times3;
    [SerializeField] Sprite times2;
    [SerializeField] Sprite times1;
    [SerializeField] Sprite times0;

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
                    if (assignedChecker.player)
                    {
                        switch (assignedChecker.multiplier)
                        {
                            case 10:
                                assignedChecker.spriteRendererMultiplier.sprite = times10;
                                break;
                            case 5:
                                assignedChecker.spriteRendererMultiplier.sprite = times5;
                                break;
                            case 3:
                                assignedChecker.spriteRendererMultiplier.sprite = times3;
                                break;
                            case 2:
                                assignedChecker.spriteRendererMultiplier.sprite = times2;
                                break;
                            case 1:
                                assignedChecker.spriteRendererMultiplier.sprite = times1;
                                break;
                            case 0:
                                assignedChecker.spriteRendererMultiplier.sprite = times0;
                                break;
                        }
                        
                    }
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
