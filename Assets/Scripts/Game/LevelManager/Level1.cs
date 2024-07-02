using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    private Dictionary<string, bool> firstActionSteps;
    private bool firstStep, secondStep, thirdStep;
    private byte stepNumber = 0;
    public GameObject[] objectsToHide;
    public GameObject planet;

    void Start()
    {
        firstActionSteps = new Dictionary<string, bool>
        {
            {"Move", false},
            {"ChangeWeapon", false},
            {"Shoot", false},
            {"Shield", false}
        };

        firstStep = false;
        secondStep = false;
        thirdStep = false;
    }

    void Update()
    {
        if (!firstStep && stepNumber == 0)
        {
            Step1();
        }
        else if (!secondStep && stepNumber == 1)
        {
            Step2();
        }
        else if (!thirdStep && stepNumber == 2)
        {
            Step3();
        }
        if (thirdStep)
        {
            HUDManager.Instance.SetHUD(HUDType.Victory);
        }
    }

    void Step1()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            firstActionSteps["Move"] = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) ||
            Input.GetKeyDown(KeyCode.Alpha3))
        {
            firstActionSteps["ChangeWeapon"] = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            firstActionSteps["Shoot"] = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            firstActionSteps["Shield"] = true;
        }

        if (AllActionsCompleted(firstActionSteps))
        {
            Debug.Log("Step 1 Completed: Movements");
            firstStep = true;
            stepNumber++;
        }
    }

    void Step2()
    {
        bool allObjectsHidden = true;

        foreach (GameObject obj in objectsToHide)
        {
            if (obj.activeSelf)
            {
                allObjectsHidden = false;
                break;
            }
        }

        if (allObjectsHidden)
        {
            Debug.Log("Step 2 completed: All objects are hidden.");
            secondStep = true;
            stepNumber++;
        }
    }

    void Step3()
    {
        if (planet.GetComponent<Enemy_Spawner>().GetCaptured())
        {
            Debug.Log("Step 3 completed: The planet is captured.");
            thirdStep = true;
            stepNumber++;
        }
    }

    bool AllActionsCompleted(Dictionary<string, bool> actions)
    {
        foreach (var action in actions)
        {
            if (!action.Value)
            {
                return false;
            }
        }
        return true;
    }
}