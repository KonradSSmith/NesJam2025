using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;
using System.Linq;
using System.Collections;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager instance { get; private set; }

    [SerializeField] public List<PlacementChecker> horses = new List<PlacementChecker>();
    [SerializeField] public List<PlacementChecker> sortedHorses = new List<PlacementChecker>();
    [SerializeField] Dictionary<int, int> placementMultipliers = new Dictionary<int, int>();

    public int amountAlive = 8;

    public bool racing = false;


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

        placementMultipliers.Add(1, 10);
        placementMultipliers.Add(2, 5);
        placementMultipliers.Add(3, 3);
        placementMultipliers.Add(4, 2);
        placementMultipliers.Add(5, 1);
        placementMultipliers.Add(6, 1);
        placementMultipliers.Add(7, 1);
        placementMultipliers.Add(8, 0);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sortedHorses = horses;
    }

    // Update is called once per frame
    void Update()
    {
        if (amountAlive <= 1)
        {
            StartCoroutine(YouWin());
        }
        sortedHorses = sortedHorses.OrderBy(ch => ch.placementDistance).ToList();
        foreach(PlacementChecker checker in sortedHorses)
        {
            checker.intPlacement = sortedHorses.IndexOf(checker) + 1;
            if (checker.player)
            {
                checker.multiplier = placementMultipliers[sortedHorses.IndexOf(checker) + 1];
                checker.jousting.multiplier = placementMultipliers[sortedHorses.IndexOf(checker) + 1];
                foreach (PlacementChecker secondChecker in sortedHorses)
                {
                    secondChecker.playerPlacement = sortedHorses.IndexOf(checker) + 1;
                    secondChecker.playerPlacementDistance = checker.placementDistance;
                }
            }
        }
    }

    IEnumerator YouWin()
    {
        yield return null;
    }
}
