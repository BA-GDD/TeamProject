using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class BulletTrail : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 0.2f;

    private TrailRenderer _trailRenderer;
    private void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    public void DrawTrail(Vector3 startPos, Vector3 endPos, float lifeTime)
    {
        transform.position = startPos;
        _trailRenderer.time = lifeTime;
        StartCoroutine(LifeTimeCoroutine(startPos,endPos));
    }

    private IEnumerator LifeTimeCoroutine(Vector3 startPos, Vector3 endPos)
    {
        float startTime = Time.time;
        float value = 0;
        while(value < 1)
        {
            float passedTime = Time.time - startTime;
            value = passedTime / _lifeTime;
            transform.position = Vector3.Lerp(startPos, endPos, value);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}