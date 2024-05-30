using UnityEngine;

public class PlayOneShotBehaviour : StateMachineBehaviour
{
    [SerializeField] private AudioClip soundToPlay;
    [SerializeField] private AudioSource targetAudioSource;
    [SerializeField] private float volume = 20f;
    [SerializeField] private bool playOnEnter = true, playOnExit = false, playAfterDelay = false;

    [SerializeField] private float playDelay = 0.25f;
    private float timeSinceEntered = 0;
    private bool hasDelayedSoundPlayed = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(playOnEnter)
        {
            PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
        }
        
        timeSinceEntered = 0f;
        hasDelayedSoundPlayed = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(playAfterDelay && !hasDelayedSoundPlayed)
        {
            timeSinceEntered += Time.deltaTime;

            if(timeSinceEntered > playDelay) 
            {
                PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
                hasDelayedSoundPlayed =true;
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnExit)
        {
            PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
        }
    }

    public void PlayClipAtPoint(AudioClip clip, Vector3 position, [UnityEngine.Internal.DefaultValue("1.0F")] float volume)
    {
        GameObject gameObject = new GameObject("One shot audio");
        gameObject.transform.position = position;
        AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
        audioSource.clip = clip;
        audioSource.spatialBlend = 1f;
        audioSource.volume = volume;
        audioSource.outputAudioMixerGroup = targetAudioSource.outputAudioMixerGroup;
        audioSource.Play();
        Object.Destroy(gameObject, clip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale));
    }
}
