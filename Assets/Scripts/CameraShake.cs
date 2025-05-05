using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration;
    public AnimationCurve shakeCurve;
    public float strengthDivider;

    public IEnumerator Shake()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            elapsedTime += Time.deltaTime;
            float strength = shakeCurve.Evaluate(elapsedTime / shakeDuration);
            transform.position = startPosition + (Random.insideUnitSphere * strength) / strengthDivider;
            yield return null;
        }

        transform.position = startPosition;
    }
}
