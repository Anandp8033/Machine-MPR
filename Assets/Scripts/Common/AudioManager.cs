using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource audio_Source;
    [SerializeField]
    private AudioClip Each_Step_Congrats;
    [SerializeField]
    private AudioClip Final_Step_Congrats;
    

    public void PlayEachStepCongratsAudio()
    {
        audio_Source.clip = Each_Step_Congrats;
        audio_Source.PlayOneShot(audio_Source.clip);
    }

    public void PlayFinalStepCongratsAudio()
    {
        audio_Source.clip = Final_Step_Congrats;
        audio_Source.PlayOneShot(audio_Source.clip);
    }

    //stop all audios
    public void StopAllAudio()
    {
        audio_Source.Stop();
    }
}
