using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [Header("Configurações de Órbita")]
    public Transform target;              
    public float orbitSpeed = 10f;        // Velocidade de rotação (graus por segundo)
    public float distance = 5f;           
    public float height = 2f;
    public Animator animator;

    [Header("Oscilação Vertical (opcional)")]
    public bool enableVerticalOscillation = false; 
    public float oscillationAmplitude = 1f;  
    public float oscillationSpeed = 1f;    
    

    [Header("Controle de Voltas")]
    public int maxRotations = 1;          // Quantas voltas completas a câmera deve dar
    private int completedRotations = 0;   // Quantas voltas já completou

    private float angle = 0f;
    private bool isOrbiting = false;

    void Update()
    {
        // Inicia a rotação ao apertar F9
        if (Input.GetKeyDown(KeyCode.F9))
        {
            if (!isOrbiting) // só reinicia se estiver parado
            {
                animator.enabled = true;
                isOrbiting = true;
                angle = -30f;
                completedRotations = 0;
            }
        }

        if (!isOrbiting || target == null) return;

        // Atualiza ângulo
        angle += orbitSpeed * Time.deltaTime;

        // Checa se completou uma volta
        if (angle >= 360f)
        {
            angle -= 360f;
            completedRotations++;

            // Se atingiu o número máximo de voltas, para
            if (completedRotations >= maxRotations)
            {
                isOrbiting = false;
                return;
            }
        }

        // Calcula posição orbital
        float radians = angle * Mathf.Deg2Rad;
        float x = Mathf.Cos(radians) * distance;
        float z = Mathf.Sin(radians) * distance;

        // Oscilação opcional no Y
        float yOffset = height;
        if (enableVerticalOscillation)
        {
            yOffset += Mathf.Sin(Time.time * oscillationSpeed) * oscillationAmplitude;
        }

        Vector3 desiredPosition = new Vector3(x, yOffset, z) + target.position;

        transform.position = desiredPosition;
        transform.LookAt(target.position);
    }
}
