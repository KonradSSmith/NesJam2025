using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;
using System.Linq;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager instance { get; private set; }

    [SerializeField] public List<AgentDriving> horses = new List<AgentDriving>();
    [SerializeField] public List<AgentDriving> sortedHorses = new List<AgentDriving>();



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
    }
}
