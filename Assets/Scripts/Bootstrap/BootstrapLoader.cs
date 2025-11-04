using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class BootstrapLoader : MonoBehaviour
{
    [SerializeField] private string firstGameplayScene = "MainScene";
    [SerializeField] private Camera bootstrapCamera;

    private void Start()
    {
        SceneManager.LoadSceneAsync(firstGameplayScene, LoadSceneMode.Additive)
            .completed += _ =>
            {
                var loaded = SceneManager.GetSceneByName(firstGameplayScene);
                if (loaded.IsValid())
                    SceneManager.SetActiveScene(loaded);

                StartCoroutine(DisableBootstrapCameraWhenGameplayIsReady());
            };
    }

    private IEnumerator DisableBootstrapCameraWhenGameplayIsReady()
    {
        while (true)
        {
            var anyOtherCameraEnabled =
                FindObjectsOfType<Camera>(true).Any(c => c.enabled && c != bootstrapCamera);

            if (anyOtherCameraEnabled)
            {
                var listener = bootstrapCamera.GetComponent<AudioListener>();
                if (listener != null) listener.enabled = false;
                bootstrapCamera.gameObject.SetActive(false);

                yield break;
            }

            yield return null;
        }
    }
}
