using UnityEngine;

public class DanoObstaculo : MonoBehaviour
{
    public float dano = 10f; // Dano em porcentagem

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Verifica se o objeto em contato é o Player
        {
            Debug.Log("Colidiu com o Player!");
            BarraDeVida barraDeVida = collision.gameObject.GetComponent<BarraDeVida>();
            if (barraDeVida != null)
            {
                barraDeVida.ReduzirVida(dano);
            }
        }
    }
}
