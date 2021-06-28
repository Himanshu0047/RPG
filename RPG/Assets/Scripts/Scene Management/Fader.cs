using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();

            StartCoroutine(FadeOutIn());
        }

        IEnumerator FadeOutIn()
        {
            yield return FadeOut(2f);
            Debug.Log("Faded out");
            yield return FadeIn(2f);
            Debug.Log("Faded in");
        }

        public IEnumerator FadeOut(float time)
        {
            while(canvasGroup.alpha < 1)
            {
                // Simple maths when Time.deltatime becomes equal to the given time then the result of
                // dividing them will be 1 hence gradually increasing the alpha value according to our given time
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null;
            }
        }

        public IEnumerator FadeIn(float time)
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }
    }
}
