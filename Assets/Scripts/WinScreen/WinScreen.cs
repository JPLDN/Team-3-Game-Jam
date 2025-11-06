using UnityEngine;
using TMPro;
using UnityEngine.Events;

[DisallowMultipleComponent]
[RequireComponent(typeof(TMP_Text))]
public sealed class FadeIn : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private TMP_Text target; 

    [Header("Fade Settings")]
    [Min(0f)] public float duration = 0.6f;
    [Min(0f)] public float delay = 0f;
    public bool ignoreTimeScale = false;
    public bool playOnEnable = true;

    [Header("Alpha (0..1)")]
    [Range(0f, 1f)] public float startAlpha = 0f;
    [Range(0f, 1f)] public float endAlpha = 1f;

    [Header("Easing")]
    public AnimationCurve ease = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    [Header("Initialization")]
    [Tooltip("If true, apply startAlpha on Awake so the text begins hidden before fading.")]
    public bool setInitialAlphaOnAwake = true;

    [Header("Events")]
    public UnityEvent onComplete;

    private Coroutine routine;
    private float baseAlpha = 1f; 

    private void Reset()
    {
        target = GetComponent<TMP_Text>();
    }

    private void OnValidate()
    {
        if (target == null) target = GetComponent<TMP_Text>();
        startAlpha = Mathf.Clamp01(startAlpha);
        endAlpha = Mathf.Clamp01(endAlpha);
        duration = Mathf.Max(0f, duration);
        delay = Mathf.Max(0f, delay);
    }

    private void Awake()
    {
        if (target == null) target = GetComponent<TMP_Text>();
        if (target == null)
        {
            Debug.LogError("[FadeIn] No TMP_Text found on GameObject.", this);
            enabled = false;
            return;
        }

        baseAlpha = target.color.a;

        if (setInitialAlphaOnAwake)
            ApplyAlpha(startAlpha);
    }

    private void OnEnable()
    {
        if (playOnEnable)
            Play();
    }

    private void OnDisable()
    {
        Stop();
    }

    public void Play()
    {
        if (!isActiveAndEnabled || target == null) return;

        Stop();
        ApplyAlpha(startAlpha);
        routine = StartCoroutine(FadeRoutine());
    }

    public void Stop()
    {
        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }
    }

    public void SetAlphaInstant(float normalizedAlpha)
    {
        ApplyAlpha(Mathf.Clamp01(normalizedAlpha));
    }

    private System.Collections.IEnumerator FadeRoutine()
    {
        if (delay > 0f)
        {
            if (ignoreTimeScale)
            {
                double until = Time.unscaledTimeAsDouble + delay;
                while (Time.unscaledTimeAsDouble < until)
                    yield return null;
            }
            else
            {
                yield return new WaitForSeconds(delay);
            }
        }

        float t = 0f;
        float dur = Mathf.Max(0.0001f, duration);

        while (t < 1f)
        {
            float dt = ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
            t += dt / dur;

            float eased = ease.Evaluate(Mathf.Clamp01(t));
            float a = Mathf.LerpUnclamped(startAlpha, endAlpha, eased);
            ApplyAlpha(Mathf.Clamp01(a));

            yield return null;
        }

        ApplyAlpha(endAlpha);
        routine = null;
        onComplete?.Invoke();
    }

    private void ApplyAlpha(float normalizedAlpha)
    {
        if (target == null) return;

        var c = target.color;
        c.a = Mathf.Clamp01(baseAlpha * normalizedAlpha);
        target.color = c;

        target.ForceMeshUpdate(false, false);
    }
}
