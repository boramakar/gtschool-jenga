using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSimulationButton : MonoBehaviour
{
    [SerializeField] private GameObject startSimulationButton;
    [SerializeField] private GameObject endSimulationButton;

    private bool _isSimulationStarted;

    private void Awake()
    {
        ResetButton();
    }

    private void OnEnable()
    {
        EventManager.OnChangeStackEvent += ResetButton;
    }

    private void OnDisable()
    {
        EventManager.OnChangeStackEvent -= ResetButton;
    }

    private void ResetButton(StackChangeDirection _ = 0)
    {
        startSimulationButton.SetActive(true);
        endSimulationButton.SetActive(false);
        _isSimulationStarted = false;
    }

    public void OnClick()
    {
        if (_isSimulationStarted)
        {
            _isSimulationStarted = false;
            endSimulationButton.SetActive(false);
            startSimulationButton.SetActive(true);
            EventManager.EndPhysicsSimulation();
        }
        else
        {
            _isSimulationStarted = true;
            endSimulationButton.SetActive(true);
            startSimulationButton.SetActive(false);
            EventManager.StartPhysicsSimulation();
        }
    }
}
