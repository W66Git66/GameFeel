using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransformSceneManager : Singleton<TransformSceneManager>
{
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration;
    public Transform playerTransform;

    private bool isFade;

    protected override void Awake()//��ʼ��
    {
        base.Awake();
    }

    protected override void OnDestroy()//����
    {
        base.OnDestroy();
    }

    private void OnEnable()
    {
        EventSystem.TeleportToScene += TransformScene;
    }
    private void OnDisable()
    {
        EventSystem.TeleportToScene -= TransformScene;
    }

    /// <summary>
    /// ����ת���߼�
    /// </summary>
    /// <param name="from">ж�صĳ���</param>
    /// <param name="to">���صĳ���</param>
    /// <param name="positionToGo">��ɫ���³��������ɵ�λ��</param>
    public void TransformScene(string from,string to,Vector3 positionToGo)
    {
        if(!isFade)
        {
            StartCoroutine(TranstionToScene(from, to,positionToGo));
        }
    }

   private IEnumerator TranstionToScene(string from,string to,Vector3 positionToGo)
    {
        yield return Fade(1);
        yield return SceneManager.UnloadSceneAsync(from);
        yield return SceneManager.LoadSceneAsync(to,LoadSceneMode.Additive);
       
        //�����³���Ϊ�����
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);

        //�л���������¼�
        playerTransform.position = positionToGo;

        yield return Fade(0);
    }

    //���䣬ת����ָ��͸����
    private IEnumerator Fade(float targetAlpha)
    {
        isFade = true;

        fadeCanvasGroup.blocksRaycasts = true;

        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration;

        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }

        fadeCanvasGroup.blocksRaycasts = false;

        isFade = false;
    }
}
