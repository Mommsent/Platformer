using System.Collections;
using UnityEngine;

public class Fade : MonoBehaviour
{
    Animator animator;
    private float time;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void In(AudioSource audioSource)
    {
        animator.SetTrigger("FadeIn");
        StartCoroutine(AudioFade(audioSource));
    }

    private IEnumerator AudioFade(AudioSource audioSource)
    {
        time = 0f;
        while (time < 3)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0.03f, 0, time / 3);
            yield return null;
        }
        yield break;
    }

    public void Out()
    {
        animator.SetTrigger("FadeOut");
    }
}
