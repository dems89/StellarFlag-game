using UnityEngine;
using UnityEngine.UI;

public class Enemy_Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyPrefabs;
    private Transform player;

    //Estados del Spawn
    [SerializeField]
    private float totalTimeToCapture = 4f;
    [SerializeField]
    private Slider captureProgress;
    private bool isCaptured = false, isCapturing = false, spawning = true;
    private float currentCaptureTime;  

    //Propiedades del Spawn  
    [SerializeField]
    private float _enemySpawnInterval = 4f;    
    private float timeLastSpawn, _detectionDistance = 25f;   

    //Orbita
    private GameObject sol; // Referencia al Sol
    private float _distanciaOrbita; // Distancia del planeta al Sol
    private float _velocidadOrbita = 0.1f; // Velocidad de la órbita
    private float _angulo; // Angulo de la órbita


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sol = GameObject.FindWithTag("Sun");
    }

    void Start()
    {        
        captureProgress.gameObject.SetActive(false);
        captureProgress.maxValue = totalTimeToCapture;
        Vector3 direccionInicial = transform.position - sol.transform.position;
        _angulo = Mathf.Atan2(direccionInicial.y, direccionInicial.x);
        _distanciaOrbita = (transform.position - sol.transform.position).magnitude;
    }
    void Update()
    {
        timeLastSpawn += Time.deltaTime;
        IsPlayerNear(ref player);
        Capturing();
        Orbitar();
    }

    void Orbitar()
    {
        _angulo += _velocidadOrbita * Time.deltaTime;

        // Calculamos la nueva posición del planeta en la órbita
        Vector3 nuevaPosicion = new Vector3(
            Mathf.Cos(_angulo) * _distanciaOrbita,
            Mathf.Sin(_angulo) * _distanciaOrbita, 0f
        );

        // Ajustamos la posición del planeta
        transform.position = sol.transform.position + nuevaPosicion;
    }
    void SpawnEnemy()
    {
        float randomOffsetX = Random.Range(-15f, 15f);
        float randomOffsetY = Random.Range(-15f, 15f);
        Vector3 offset = new(randomOffsetX, randomOffsetY, 0f);

        float randomValue = Random.value;
        byte enemyIndex;
        if (randomValue <= 0.40f)
        {
            enemyIndex = 0;
        }
        else if (randomValue <= 0.80f)
        {
            enemyIndex = 1;
        }
        else
        {
            enemyIndex = 2;
        }
        Instantiate(enemyPrefabs[enemyIndex], transform.position + offset, Quaternion.identity);
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
        if (isCaptured) return;
        if (isCapturing)
        {
            currentCaptureTime += Time.deltaTime / totalTimeToCapture;
            captureProgress.value = currentCaptureTime;
            //Debug.Log(currentCaptureTime);
            if (currentCaptureTime >= totalTimeToCapture)
            {
                spawning = false;
                isCaptured = true;
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
