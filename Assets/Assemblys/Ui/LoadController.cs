using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadController : MonoBehaviour
{
    Animator anim;
    int Escolha;
    public float m_progress = 0f;
    
    public AudioSource AudioBatida;
    public AudioSource AudioArrasta;
    public AudioSource AudioAbrindo;
    AudioSource fx;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        DontDestroyOnLoad(gameObject);
    }  


    public void CarregarCenaEscolhida(int CenaEscolhida)
    {
        AudioArrasta.PlayOneShot(AudioArrasta.clip);
        gameObject.SetActive(true);
        anim.Play("iniciar");
        Escolha = CenaEscolhida;
    }


    public void AnimacaoEventMUDARCENA()
    {
        StartCoroutine(LoadLevel(Escolha));
        SceneManager.LoadScene(Escolha);
    }
    
    public void AnimacaoEventeTERMINARTRASICAO()
    {
        gameObject.SetActive(false);
    }

    public void FECHARJOGO()
    {
        AudioArrasta.PlayOneShot(AudioArrasta.clip);
        gameObject.SetActive(true);
        anim.Play("quit");
    }
    public void FECHARANIMATION()
    {
        Application.Quit();
        Debug.Log("Fechando Jogo");
    }

    public void SomBatida()
    {
        AudioBatida.PlayOneShot(AudioBatida.clip);
    }

    public void SomAbrindo()
    {
        AudioAbrindo.PlayOneShot(AudioAbrindo.clip);
    }

    public void SomArrasta()
    {
        AudioArrasta.PlayOneShot(AudioArrasta.clip);
    }


    IEnumerator LoadLevel(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
        if (operation != null) { operation.allowSceneActivation = false; }
        while (!operation.isDone)
        {
            if (operation.progress.Equals(.9f))
            {
                operation.allowSceneActivation = false;
                yield return new WaitForSecondsRealtime(1f);                
            }
            m_progress = operation.progress;
            yield return new WaitForEndOfFrame();
        }
        anim.Play("terminar");
        operation.allowSceneActivation = true;
    }

}
