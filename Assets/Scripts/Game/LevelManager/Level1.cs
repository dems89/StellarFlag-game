using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    private Dictionary<string, bool> firstActionSteps;
    private bool firstStep, secondStep, thirdStep;
    private byte stepNumber = 0;
    [SerializeField]
    private GameObject[] enemiesToHide;
    [SerializeField]
    private GameObject planet;
    [SerializeField]
    private GameObject[] gates;

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
            firstStep = true;
            gates[0].SetActive(false);
            stepNumber++;
        }
    }

    void Step2()
    {
        bool allObjectsHidden = true;

        foreach (GameObject obj in enemiesToHide)
        {
            if (obj.activeSelf)
            {
                allObjectsHidden = false;
                break;
            }
        }

        if (allObjectsHidden)
        {
            secondStep = true;
            gates[1].SetActive(false);
            stepNumber++;
        }
    }

    void Step3()
    {
        if (planet.GetComponent<Enemy_Spawner>().GetCaptured())
        {
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