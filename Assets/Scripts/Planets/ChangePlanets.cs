using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlanets : MonoBehaviour
{
    public List<GameObject> listaPlanetas;
    private SpriteRenderer actualSpriteRenderer;
    private Animator actualAnimator;

    private void Awake()
    {
        actualSpriteRenderer = GetComponent<SpriteRenderer>();
        actualAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        ChangePlanetProperties();
    }

    private void ChangePlanetProperties()
    {
        GameObject randomPlanet = RandomPrefabAsign(ref listaPlanetas);
        if (randomPlanet != null)
        {
            SpriteRenderer randomSpriteRenderer = randomPlanet.GetComponent<SpriteRenderer>();
            if (randomSpriteRenderer != null)
            {
                actualSpriteRenderer.sprite = randomSpriteRenderer.sprite;
            }
            Animator randomAnimator = randomPlanet.GetComponent<Animator>();
            if (randomAnimator != null)
            {
                actualAnimator.runtimeAnimatorController = randomAnimator.runtimeAnimatorController;
            }
            //Debug.Log("Nuevo planeta: " + randomPlanet.name);
        }
    }

    private GameObject RandomPrefabAsign(ref List<GameObject> lista)
    {
        int randomNum = Random.Range(0, lista.Count);
        return lista[randomNum];
    }
}