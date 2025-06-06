using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;
using System.Linq;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager instance { get; private set; }

    [SerializeField] public List<PlacementChecker> horses = new List<PlacementChecker>();
    [SerializeField] public List<PlacementChecker> sortedHorses = new List<PlacementChecker>();



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
        sortedHorses = horses;
    }

    // Update is called once per frame
    void Update()
    {
        sortedHorses = sortedHorses.OrderBy(ch => ch.placementDistance).ToList();
        foreach(PlacementChecker checker in sortedHorses)
        {
            checker.intPlacement = sortedHorses.IndexOf(checker) + 1;
            if (checker.player)
            {
                foreach (PlacementChecker secondChecker in sortedHorses)
                {
                    secondChecker.playerPlacement = sortedHorses.IndexOf(checker) + 1;
                    secondChecker.playerPlacementDistance = checker.placementDistance;
                }
            }
        }
    }
}
