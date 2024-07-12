using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScale : MonoBehaviour
{
    [SerializeField] private Vector2 minScale = new Vector2(0.5f, 0.5f);
    [SerializeField] private Vector2 maxScale = new Vector2(2.0f, 2.0f);
    //Orbita
    private GameObject sol;
    private float _distanciaOrbita;
    private float _velocidadOrbita;
    private float _angulo;

    void Start()
    {
        sol = GameObject.FindWithTag("Sun");        
        
        

        Vector3 randomScale = new Vector3(
            Random.Range(minScale.x, maxScale.x),
            Random.Range(minScale.y, maxScale.y),
            1.0f
        );

        transform.localScale = randomScale;
    }

    private void Update()
    {
        _distanciaOrbita = (transform.position - sol.transform.position).magnitude;
        _velocidadOrbita = CalculateOrbitalSpeed(_distanciaOrbita);
        Vector3 direccionInicial = transform.position - sol.transform.position;
        _angulo = Mathf.Atan2(direccionInicial.y, direccionInicial.x);
        Orbitar();
    }

    float CalculateOrbitalSpeed(float distance)
    {
        // Ajustar la constante según sea necesario para el efecto deseado
        float orbitalSpeed = 0.3f / Mathf.Sqrt(distance);
        return orbitalSpeed;
    }

    void Orbitar()
    {
        _angulo += _velocidadOrbita * Time.deltaTime;

        Vector3 nuevaPosicion = new Vector3(
            Mathf.Cos(_angulo) * _distanciaOrbita,
            Mathf.Sin(_angulo) * _distanciaOrbita, 0f
        );

        transform.position = sol.transform.position + nuevaPosicion;
    }
}
