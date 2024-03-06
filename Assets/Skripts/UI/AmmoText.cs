using TMPro;
using UnityEngine;
using Zenject;

public class AmmoText : MonoBehaviour
{
    TextMeshProUGUI textMeshPro;
    IAmmo ammo;
    private void OnEnable()
    {
        ammo.wasChanged += SetTextValue;
    }

    [Inject]
    private void Construct(IAmmo ammo)
    {
        this.ammo = ammo;
    }

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        SetTextValue(ammo.AmmoAmount);
    }

    private void SetTextValue(int ammountOfAmmo)
    {
        textMeshPro.text = "Arrows: " + ammo.AmmoAmount;
    }

    private void OnDisable()
    {
        ammo.wasChanged -= SetTextValue;
    }
}
