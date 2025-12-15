using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CenasController : MonoBehaviour
{
    public GameObject LoadingOBJ;
    public GameObject _Loading;
    public int IndexSelecionado;

    private void Awake()
    {
        //verifica se o loading ja foi instanciado
        if (GameObject.Find("=LOADING=") == null)
        {
            GameObject loading = Instantiate(LoadingOBJ);
            loading.name = "=LOADING=";
            _Loading = loading;
            _Loading.SetActive(false);
        }
        else
        {
            _Loading = GameObject.Find("=LOADING=");
        }
    }


    public void Cena_Restart()
    {
        ProximaCena(SceneManager.GetActiveScene().buildIndex);
    }


    public void ProximaCena(int indexLevel)
    {
        _Loading.SetActive(true);
        _Loading.GetComponent<LoadController>().CarregarCenaEscolhida(indexLevel);
    }

    public void FecharJOGO()
    {
        _Loading.SetActive(true);
        _Loading.GetComponent<LoadController>().FECHARJOGO();
    }

    public void SeleçãoSetar(int indexLevel)
    {
        IndexSelecionado = indexLevel;
    }

    public void SeleçãooLimpar()
    {
        IndexSelecionado = 1000;
    }

    public void SeleçãooIr()
    {
        if (IndexSelecionado != 1000)
        {
            _Loading.SetActive(true);
            _Loading.GetComponent<LoadController>().CarregarCenaEscolhida(IndexSelecionado);
        }
    }


}
