using System;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private const string CUT = "Cut";
    [SerializeField] private CuttingCounter cuttingCounter;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cuttingCounter.OnRecipeCut += CuttingCounter_OnRecipeCut;
    }

    private void CuttingCounter_OnRecipeCut(object sender, System.EventArgs e) 
    {
        animator.SetTrigger(CUT);
    }
}
