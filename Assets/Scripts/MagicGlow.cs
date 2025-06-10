using UnityEngine;

public class MagicalGlow : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [Header("Cores mágicas")]
    public Color startColor = new Color(0.6f, 0.8f, 1f, 1f); // azul claro
    public Color endColor = new Color(1f, 1f, 1f, 1f);       // branco

    [Header("Configuração do brilho")]
    public float pulseDuration = 1.5f; // tempo da transição

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float t = Mathf.PingPong(Time.time / pulseDuration, 1f);
        spriteRenderer.color = Color.Lerp(startColor, endColor, t);
    }
}
