using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using Assets.Scripts;

public class timePass : MonoBehaviour
{

    float advance;
    Movement fox;
    float startPos;
    float endPos;
    float maxPos;

    ColorGradingModel.TonemappingSettings tonemapping;
    ColorGradingModel.BasicSettings basic;
    BloomModel.BloomSettings bloom;

    PostProcessingBehaviour postProcessing;
    // Start is called before the first frame update
    void Start()
    {
        maxPos = startPos;
        fox = FindObjectOfType<Movement>();
        advance = 0;
        postProcessing = GetComponent<PostProcessingBehaviour>();
        tonemapping = postProcessing.profile.colorGrading.settings.tonemapping;
        basic = postProcessing.profile.colorGrading.settings.basic;
        bloom = postProcessing.profile.bloom.settings.bloom;
    }

    // Update is called once per frame
    void Update()
    {
        if (fox.transform.position.x > maxPos)
            maxPos = fox.transform.position.x;
        advance = Mathf.Lerp(0, 1,maxPos/endPos);
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
