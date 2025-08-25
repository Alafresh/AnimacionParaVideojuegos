using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class RecoilCameraKick : MonoBehaviour
{
    [SerializeField] private CinemachineCamera[] cameras;

    CinemachineBasicMultiChannelPerlin[] perlins;

    float[] baseamplitude;

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
        //El peak y el recover son tiempos en segundos
        //ParentCharacter.IsAiming me sirve pa poder hacer que el recoil sea diferente si estoy apuntando o no
        //Tengo que reemplazar los lerp por el animation curve. El baseamplitude no sería un float, sino un AnimationCurve
        //Pico
        float t = 0;
        while (t < peak)
        {
            t += Time.deltaTime;
            float k = t/ Mathf.Max(0.001f, peak);
            if(isAiming) strenght = strenght * 0.6f;
            for (int i = 0; i < perlins.Length; i++)
            {
                if (perlins[i]) perlins[i].AmplitudeGain = Mathf.Lerp(baseamplitude[i], baseamplitude[i] + strenght, k);
            }
            yield return null;
        }

        //Recover
        t = 0f;
        while (t < recover)
        {
            t += Time.deltaTime;
            float k = t / Mathf.Max(0.001f, recover);
            for (int i = 0; i < perlins.Length; i++)
            {
                if (perlins[i]) perlins[i].AmplitudeGain = Mathf.Lerp(strenght, baseamplitude[i] + strenght, k);
            }
            yield return null;
        }

        for (int i = 0; i < perlins.Length; i++)
        {
            if (perlins[i]) perlins[i].AmplitudeGain = baseamplitude[i];
        }
    }
}
