using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarMonstro : MonoBehaviour
{
    Animator animator;
    bool AnimRolando = false;
    public string NomeAnim1 = "Anim1";
    public string NomeAnim2 = "Anim2";
    public AudioSource Som1;
    public AudioSource Som2;
    public AudioSource Som3;
    public AudioSource Som4;
    bool teste = false;
    public bool TemInimigos = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            TemInimigos = true;
            if (!AnimRolando)
            {
                if (!teste)
                {
                    animator.Play(NomeAnim1);
                    teste = true;
                }
            }
        }
    }

    private IEnumerator OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            TemInimigos = false; //ARRUMAR, MAIS DE 1 INIMIGO ELE FECHA A PORTA ANTES
            yield return new WaitForSeconds(.2f);
            if (!AnimRolando)
            {
                if (teste)
                {
                    animator.Play(NomeAnim2);
                    teste = false;
                }
            }
        }
    }

    private void Entrou()
    {
        if (!AnimRolando)
        {
            if (!teste)
            {
                animator.Play(NomeAnim1);
                teste = true;
            }
        }
    }

    private IEnumerator Saiu()
    {
        yield return new WaitForSeconds(.2f);
        if (!AnimRolando)
        {
            if (teste)
            {
                animator.Play(NomeAnim2);
                teste = false;
            }
        }
    }

    public void AnimSom1()
    {
        Som1.PlayOneShot(Som1.clip);
    }

    public void AnimSom2()
    {
        Som2.PlayOneShot(Som2.clip);
    }

    public void AnimSom3()
    {
        Som3.PlayOneShot(Som3.clip);
    }

    public void AnimSom4()
    {
        Som4.PlayOneShot(Som4.clip);
    }

    public void StartAnimation()
    {
        AnimRolando = true;
    }

    public void EndAnimation()
    {
        AnimRolando = false;
        if (!TemInimigos) { StartCoroutine(Saiu()); };
    }


}
