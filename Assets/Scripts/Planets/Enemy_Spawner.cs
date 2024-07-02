using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Spawner : MonoBehaviour
{
    [SerializeField]
    private string[] enemies = {"RedEnemy", "GreenEnemy", "BlueEnemy"};
    private Transform player;
    [SerializeField]
    private GameObject _flag;

    //Estados del Spawn
    [SerializeField]
    private float totalTimeToCapture = 3f;
    [SerializeField]
    private Slider captureProgress;
    private bool isCaptured = false, isCapturing = false, spawning = true;
    private float currentCaptureTime;  

    //Propiedades del Spawn  
    [SerializeField]
    private float _enemySpawnInterval = 2.5f;    
    private float timeLastSpawn, _detectionDistance = 25f;   

    //Orbita
    private GameObject sol;
    private float _distanciaOrbita;
    private float _velocidadOrbita = 0.1f;
    private float _angulo;

    public byte segments = 100;
    private LineRenderer lineRenderer;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sol = GameObject.FindWithTag("Sun");
        _flag.SetActive(false);
    }

    void Start()
    {        
        captureProgress.gameObject.SetActive(false);
        captureProgress.maxValue = totalTimeToCapture;
        Vector3 direccionInicial = transform.position - sol.transform.position;
        _angulo = Mathf.Atan2(direccionInicial.y, direccionInicial.x);
        _distanciaOrbita = (transform.position - sol.transform.position).magnitude;
        _velocidadOrbita = CalculateOrbitalSpeed(_distanciaOrbita);
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1;
        CalculateOrbit();
    }
    void Update()
    {
        timeLastSpawn += Time.deltaTime;
        IsPlayerNear(ref player);
        Capturing();
        Orbitar();
        CalculateOrbit();
    }
    void CalculateOrbit()
    {
        Vector3[] positions = new Vector3[segments + 1];

        // Calcula los puntos de la órbita
        for (byte i = 0; i < segments; i++)
        {
            float angle = i * 2 * Mathf.PI / segments;
            float x = sol.transform.position.x + Mathf.Cos(angle) * _distanciaOrbita;
            float y = sol.transform.position.y + Mathf.Sin(angle) * _distanciaOrbita;
            positions[i] = new Vector3(x, y, 0f);
        }

        positions[segments] = positions[0];

        // Asigna los puntos al LineRenderer
        lineRenderer.SetPositions(positions);
    }

    void Orbitar()
    {
        _angulo += _velocidadOrbita * Time.deltaTime;

        // Calcula la nueva posición del planeta
        Vector3 nuevaPosicion = new Vector3(
            Mathf.Cos(_angulo) * _distanciaOrbita,
            Mathf.Sin(_angulo) * _distanciaOrbita, 0f
        );

        transform.position = sol.transform.position + nuevaPosicion;
    }
    void SpawnEnemy()
    {
        float randomOffsetX = Random.Range(-15f, 15f);
        float randomOffsetY = Random.Range(-15f, 15f);
        Vector3 offset = new(randomOffsetX, randomOffsetY, 0f);

        float randomValue = Random.value;
        sbyte enemyIndex;
        string enemyType;
        if (randomValue <= 0.40f)
        {
            enemyIndex = 0;
            enemyType = enemies[enemyIndex];
        }
        else if (randomValue <= 0.80f)
        {
            enemyIndex = 1;
            enemyType = enemies[enemyIndex];
        }
        else
        {
            enemyIndex = 2;
            enemyType = enemies[enemyIndex];
        }
        GameObject enemy = ObjectPooler.SharedInstance.GetPooledObjects(enemyType);
        if (enemy != null)
        {
            enemy.transform.SetPositionAndRotation(this.transform.position + offset, this.transform.rotation);
            enemy.SetActive(true);
        }
    }

    private void IsPlayerNear(ref Transform player)
    {
        Vector2 direction = player.position - transform.position;
        if (timeLastSpawn >= _enemySpawnInterval && direction.magnitude < _detectionDistance && spawning)
        {
            SpawnEnemy();
            timeLastSpawn = 0f;
        }
    }

    void Capturing()
    {
        if (isCaptured) { captureProgress.gameObject.SetActive(false); return; }
        if (isCapturing)
        {
            currentCaptureTime += Time.deltaTime / totalTimeToCapture;
            captureProgress.value = currentCaptureTime;
            //Debug.Log(currentCaptureTime);
            if (currentCaptureTime >= totalTimeToCapture)
            {
                spawning = false;
                isCaptured = true;
                _flag.SetActive(true);
                //captureProgress.gameObject.SetActive(false);
            }
        } else
        {
            currentCaptureTime -= Time.deltaTime / totalTimeToCapture;
            currentCaptureTime = Mathf.Clamp(currentCaptureTime, 0f, totalTimeToCapture);
            captureProgress.value = currentCaptureTime;
           // Debug.Log(currentCaptureTime);
        }
       
    }

    public bool GetCaptured()
    {
        return isCaptured;
    }

    float CalculateOrbitalSpeed(float distance)
    {
        // Ajustar la constante según sea necesario para el efecto deseado
        float orbitalSpeed = 1f / Mathf.Sqrt(distance);
        return orbitalSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCaptured)
        {
            isCapturing = true;           
            captureProgress.gameObject.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCapturing = false;
        }
    }
}
