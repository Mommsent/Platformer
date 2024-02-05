using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public int healtRestore = 10;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);

    IHealable playerHealth;

    private AudioSource pickupSource;
    
    private void Awake()
    {
        pickupSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool checkIfHas = collision.TryGetComponent(out playerHealth);
        if (checkIfHas)
        {
            bool wasHealed = playerHealth.IsCanBeRestored(healtRestore);
            if(wasHealed)
            {
                playerHealth.Restore(healtRestore);
                if (pickupSource != null)
                    AudioSource.PlayClipAtPoint(pickupSource.clip, gameObject.transform.position, pickupSource.volume);
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}
