using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesabilitarObjeto : MonoBehaviour
{
    public void GameObjectAtivar()
    {
        gameObject.SetActive(true);
    }
    public void GameObjectDesativar()
    {
        gameObject.SetActive(false);
    }

    public void AnimatorAtivar()
    {
        gameObject.GetComponent<Animator>().enabled = true;
    }

    public void AnimatorDesativar()
    {
        gameObject.GetComponent<Animator>().enabled = false;
    }
}
