using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioClip buttonHover;
    public AudioClip points;
    public AudioSource AudioSource;

    public void PlayPointsSound()
    {
        AudioSource.PlayOneShot(points, 0.25f);
    }
}
