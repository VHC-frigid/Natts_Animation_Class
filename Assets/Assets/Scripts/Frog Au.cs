using UnityEngine;

public class FrogAu : MonoBehaviour
{
    public AudioSource computerSpeaker;
    public AudioClip frogNoise;

    public void FrogAlert()
    {
        computerSpeaker.PlayOneShot(frogNoise);
    }
}
