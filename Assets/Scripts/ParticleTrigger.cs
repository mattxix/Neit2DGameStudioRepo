using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
    public ParticleSystem effect;

    // This function will be called by the Animation Event
    public void TriggerBurst()
    {
        if (effect != null)
        {
            effect.Play();
        }
    }
}