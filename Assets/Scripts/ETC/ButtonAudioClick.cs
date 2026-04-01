using UnityEngine;

public class ButtonAudioClick : MonoBehaviour
{
    [SerializeField] AudioClip press;

    public void playClick() 
    {
        AudioSource.PlayClipAtPoint(press, this.transform.position);
    }


}
