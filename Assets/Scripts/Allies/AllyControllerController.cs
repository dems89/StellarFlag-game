using UnityEngine;

public class AllyController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float speed;
    private float distanciaMaxima = 10f;
    private float distanciaMinima = 1.8f;
    void Start()
    {
  
    }
  
    public void FixedUpdate()
    {
        if (player != null)
        {
            float distancia = Vector2.Distance(transform.position, player.transform.position);
            if (distancia < distanciaMaxima && distancia> distanciaMinima)
            {
                Movimiento(player.transform.position);
            }
         
        }
    }
    private void Movimiento(Vector2 playerPosition)
    {    
            Vector2 targetPosition = Vector2.Lerp(transform.position, playerPosition, speed * Time.fixedDeltaTime);
            transform.position = targetPosition;

    }
}
