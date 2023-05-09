using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CurseFX : MonoBehaviour
{
    public Volume volume;
    private ChromaticAberration chromaticAberation;
    private FilmGrain filmGrain;
    private CorruptionScript corruptionScript;
    private ColorAdjustments colorAdjustments;
    public float caContrastMax = 50f;

    private void Start()
    {
        corruptionScript = GetComponent<CorruptionScript>();
    }

    void Update()
    {

        if (volume.profile.TryGet<ChromaticAberration>(out chromaticAberation) && volume.profile.TryGet<FilmGrain>(out filmGrain) && volume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            chromaticAberation.intensity.value = corruptionScript.metre;
            filmGrain.intensity.value = corruptionScript.metre;
            colorAdjustments.contrast.value = corruptionScript.metre * caContrastMax;
        }
    }
}
