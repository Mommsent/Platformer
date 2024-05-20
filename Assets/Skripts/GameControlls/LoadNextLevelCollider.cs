using UnityEngine;

public class LoadNextLevelCollider : MonoBehaviour
{
    [SerializeField] SceneController controller;
    private float dilayForLoading = 4f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            controller.LoadNextLevevl(dilayForLoading);
        }
    }
}
