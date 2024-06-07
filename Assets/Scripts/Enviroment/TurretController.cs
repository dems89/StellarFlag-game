using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField]
    private float rangoDeVision = 20f;
    [SerializeField]
    private LayerMask capaDelJugador;
    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private GameObject mira;
    Vector2 posicionInicial;
    Vector2 direccion;

    private void Start()
    {
        posicionInicial = mira.transform.position;
        direccion = Vector2.right;
    }

    void Update()
    {
        // Disparar un rayo hacia adelante para buscar la colision.
        RaycastHit2D hit = Physics2D.Raycast(posicionInicial, direccion, rangoDeVision, capaDelJugador);

        if (hit.collider != null)
        {
            Debug.Log("Jugador detectado!");
        }
    }

    public void SwitchTurret()
    {
        if (laser.activeSelf)
        {
            Debug.Log("Torreta Desactivada");
            laser.SetActive(false);
        }else { laser.SetActive(true); Debug.Log("Torreta activada"); }
    }
}