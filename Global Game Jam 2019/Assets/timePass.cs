using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class timePass : MonoBehaviour
{

    float advance;

    ColorGradingModel.TonemappingSettings tonemapping;
    ColorGradingModel.BasicSettings basic;
    BloomModel.BloomSettings bloom;

    PostProcessingBehaviour postProcessing;
    // Start is called before the first frame update
    void Start()
    {
        advance = 0;
        postProcessing = GetComponent<PostProcessingBehaviour>();
        tonemapping = postProcessing.profile.colorGrading.settings.tonemapping;
        basic = postProcessing.profile.colorGrading.settings.basic;
        bloom = postProcessing.profile.bloom.settings.bloom;
    }

    // Update is called once per frame
    void Update()
    {
        tonemapping.neutralBlackIn = Mathf.Lerp(-0.006f,0.1f, advance);
        tonemapping.neutralWhiteIn = Mathf.Lerp(15.6f, 4.6f, advance);
        tonemapping.neutralWhiteOut = Mathf.Lerp(10, 4.31f, advance);
        tonemapping.neutralWhiteLevel = Mathf.Lerp(5.9f, 10.2f, advance);
        basic.saturation = Mathf.Lerp(1, 0.68f, advance);
        basic.contrast = Mathf.Lerp(1, 0.77f, advance);
        bloom.softKnee = Mathf.Lerp(0.5f, 0.895f, advance);
        bloom.radius = Mathf.Lerp(4f, 5.43f, advance);
    }
}
