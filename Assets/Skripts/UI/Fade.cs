using UnityEngine;

public class Fade : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void In()
    {
        animator.SetTrigger("FadeIn");
    }

    public void Out()
    {
        animator.SetTrigger("FadeOut");
    }
}
