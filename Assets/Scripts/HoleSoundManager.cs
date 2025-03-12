using UnityEngine;

public class HoleSoundManager : MonoBehaviour
{
    public AudioSource successSound; // Assign an AudioSource with a success clip in the Inspector

    public void PlaySuccessSound()
    {
        if (successSound != null)
        {
            successSound.Play();
        }
        else
        {
            Debug.LogError("No AudioSource assigned to HoleSoundManager!");
        }
    }
}
