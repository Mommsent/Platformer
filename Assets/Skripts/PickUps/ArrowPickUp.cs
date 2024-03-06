using UnityEngine;
using Zenject;

public class ArrowPickUp : MonoBehaviour
{
    public int ammountOfAmmo = 5;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);
    Player player;

    private AudioSource pickupSource;
    IAmmo playersAmmo;

    [Inject]
    private void Construct(IAmmo ammo)
    {
        playersAmmo = ammo;
    }
    
    private void Awake()
    {
        pickupSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool checkIfHas = collision.TryGetComponent(out player);
        if (checkIfHas)
        {
            if (pickupSource != null)
                AudioSource.PlayClipAtPoint(pickupSource.clip, gameObject.transform.position, pickupSource.volume);
            playersAmmo.AmmoAmount += ammountOfAmmo;

            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}
