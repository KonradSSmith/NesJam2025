using UnityEngine;

public class UIPlacementElement : MonoBehaviour
{
    public PlacementChecker assignedChecker;
    bool assigned = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
            }
        }
    }
}
