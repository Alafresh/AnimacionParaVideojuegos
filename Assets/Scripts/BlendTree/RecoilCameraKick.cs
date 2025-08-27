using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class RecoilCameraKick : MonoBehaviour
{
    [SerializeField] private CinemachineCamera[] cameras;

    CinemachineBasicMultiChannelPerlin[] perlins;

    float[] baseamplitude;
    public AnimationCurve recoilCurve;

    private void Awake()
    {
        perlins = new CinemachineBasicMultiChannelPerlin[cameras.Length];

        baseamplitude = new float[cameras.Length];
        for (int i = 0; i < cameras.Length; i++)
        {
            perlins[i] = cameras[i].GetComponent<CinemachineBasicMultiChannelPerlin>();
            if (perlins[i]) baseamplitude[i] = perlins[i].AmplitudeGain;
        }
    }

    public void Kick(float strenght, float peak, float recover, bool isAiming)
    { 
        StopAllCoroutines();
        StartCoroutine(KickRoutine(strenght, peak, recover, isAiming));

    }
    IEnumerator KickRoutine(float strenght, float peak, float recover, bool isAiming)
    {

        if (strenght <= 0) yield break;     
        if (isAiming) strenght = strenght * 0.6f;

        recoilCurve = AnimationCurve.EaseInOut(0, 0, peak + recover, strenght);

        float t = 0f;
        float duration = peak + recover;
        while (t < duration)
        {
            t += Time.deltaTime;
            float r = recoilCurve.Evaluate(t);
            for (int i = 0; i < perlins.Length; i++)
            {
                if (perlins[i]) perlins[i].AmplitudeGain = baseamplitude[i] + r;
            }
            yield return null;
        }
        for (int i = 0; i < perlins.Length; i++)
        {
            if (perlins[i]) perlins[i].AmplitudeGain = baseamplitude[i];
        }
    }
}
