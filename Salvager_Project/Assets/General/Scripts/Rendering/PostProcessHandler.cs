using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class PostProcessHandler : MonoBehaviour
{
    public Volume volume;

    public float aberrationResetSpeed = 1;
    ChromaticAberration _chromaticAberration;

    private void Start()
    {
        volume.profile.TryGet<ChromaticAberration>(out _chromaticAberration);
    }

    public void SetMaxAberration()
    {
        _chromaticAberration.intensity.value = 0.5f;

        StartCoroutine(ResetAberration());
    }

    IEnumerator ResetAberration()
    {
        while (true) 
        {
            _chromaticAberration.intensity.value -= aberrationResetSpeed * Time.deltaTime;

            if(_chromaticAberration.intensity.value <= 0)
            {
                yield break;
            }

            yield return null;
        }
    }
}
