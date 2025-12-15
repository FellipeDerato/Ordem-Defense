using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class WaveSpawner : MonoBehaviour {

    [Header("Monstros")]
	public Transform ZumbiDeSangue;
	public Transform Degolificada;
	public Transform Carniçal;
	public Transform ZumbiBestial;
	public Transform Existido;
	public Transform ExistidoMorte;
	public Transform Mumia;
	public Transform Anarquico;
	public Transform AnarquicoDescontrolado;
	public Transform PerturbadosDeEnergia;
	public Transform Viajante;
	public Transform Vulto;

	[Header("WaveCoisas")]
	public bool RoundRolando = false;
	public bool ComeçoRoundRolando = false;
	public bool VeloDobrada = false;

    [Header("AutoStart")]
	public bool AutoStartBOOL = false;

	[Header("SeleçãoAgentes")]
	public SeleçãoAgentes sA;
	public GameObject VelôBotão;
	public GameObject Camera;
	

	[Header("Sistema")]
	public GameObject UiVida;
	public Transform spawnPoint;
	bool PassouRound = false;
	public float num;
	public int waveIndex = 1;
	public Text waveIndexNumero;
	public float perto = 0;
	public GameMaster gm;
	

	[Header("Sons")]
	public AudioSource somCerto;
	public AudioSource som1;
	public AudioSource som2;
	public AudioSource som3;
	public AudioSource som4;

    private void Start()
    {
		sA = gm.sA;


		//uivi.sA = seleçãoAgentes;
    }
    void Update()
	{

		if (Keyboard.current.eKey.wasPressedThisFrame)
		{
			Spawnar(Anarquico);
		}

		waveIndexNumero.text = "Round\n"+Mathf.Round(waveIndex).ToString();

		//Para testes;
		if (Keyboard.current.pageUpKey.wasPressedThisFrame)
		{
			waveIndex++;
		}
		if (Keyboard.current.pageDownKey.wasPressedThisFrame)
		{
			waveIndex--;
		}


        if (!sA.seleçãoAtiva)
		{
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                BotaoPrincipal();
            }
        }

        if (AutoStartBOOL)
        {
			if (!ComeçoRoundRolando)
			{
				if (!RoundRolando)
				{
					if (PassouRound)
					{
						waveIndex++;
						ComeçarRound();
						PassouRound = false;
					}
                }
			}
		}
        else
        {
			if (!ComeçoRoundRolando)
			{
				if (!RoundRolando)
				{
					if (PassouRound)
					{
                        gm.mm.ValorDinheiro += 50;
						waveIndex++;
						PassouRound = false;
                        VelôBotão.GetComponentInChildren<Text>().text = "Iniciar";
                        return;
					}
					if (sA.seleçãoAtiva)
					{
						VelôBotão.GetComponentInChildren<Text>().text = "";
					}
					VeloDobrada = true;
					Time.timeScale = 1;
					return;
				}
			}
		}
	}

	public void Vocidade()
	{
        if (VeloDobrada)
        {
            Time.timeScale = 2;
            VeloDobrada = false;
            VelôBotão.GetComponentInChildren<Text>().text = "X 2";
        }
        else if (!VeloDobrada)
        {
            Time.timeScale = 1;
            VeloDobrada = true;
            VelôBotão.GetComponentInChildren<Text>().text = "X 1";
        }
    }
	
	public void BotaoPrincipal()
    {
		if (AutoStartBOOL)
		{
            if (ComeçoRoundRolando)
            {
                if (RoundRolando)
                {
                    Vocidade();
                    return;
                }
            }
			else
			{
				if (!RoundRolando)
				{
                    ComeçarRound();
                }
			}
        }
		if (!ComeçoRoundRolando)
		{
			if (!RoundRolando)
			{
                VelôBotão.GetComponentInChildren<Text>().text = "X 1";
                ComeçarRound();
				return;
			}
		}
        Vocidade();

    }

	public void BotaoAutoStart()
    {
		if (AutoStartBOOL)
        {			
			AutoStartBOOL = false;
        }
		else if (!AutoStartBOOL)
        {
			AutoStartBOOL = true;
        }
    }

    void ComeçarRound()
    {
        ComeçoRoundRolando = true;
        StartCoroutine(SpawnWave());
        if (gm.mm.L_Agentes != null)
        {
            foreach (Agente_Base agente in gm.mm.L_Agentes)
            {
               // StartCoroutine(agente.GetComponent<Torre1>().Começo());
            }
        }
    }

    public IEnumerator SpawnWave ()
	{
		num = Random.Range(1, 2);
		if(num == 1) {somCerto = som1; } if(num == 2) {somCerto = som2; }
		somCerto.GetComponent<AudioSource>().pitch = 1;
		AudioSource fx = somCerto.GetComponent<AudioSource>(); fx.PlayOneShot(fx.clip);

		if (waveIndex == 1)
        {
			Spawnar(ZumbiDeSangue);
            yield return new WaitForSeconds(1f);
        }
		if (waveIndex == 2)
        {
			Instantiate(ZumbiDeSangue.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(1f);
			Instantiate(ZumbiDeSangue.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(1f);
			Instantiate(ZumbiDeSangue.gameObject, spawnPoint.position, spawnPoint.rotation);
		}
		if (waveIndex == 3)
        {
			Instantiate(ZumbiDeSangue.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(.5f);
			Instantiate(ZumbiDeSangue.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(.5f);
			Instantiate(ZumbiDeSangue.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(.5f);
			Instantiate(ZumbiDeSangue.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(.5f);
			Instantiate(ZumbiDeSangue.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(3f);
			Instantiate(Existido.gameObject, spawnPoint.position, spawnPoint.rotation);
		}
		if (waveIndex == 4)
        {
			Instantiate(Existido.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(4f);
			Instantiate(Existido.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(4f);
			Instantiate(ZumbiDeSangue.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(1f);
			Instantiate(ZumbiDeSangue.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(1f);
			Instantiate(ZumbiDeSangue.gameObject, spawnPoint.position, spawnPoint.rotation);
		}
		if (waveIndex == 5)
		{
			Instantiate(Existido.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(2f);
			Instantiate(Existido.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(4f);
			Instantiate(ZumbiDeSangue.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(.5f);
			Instantiate(ZumbiDeSangue.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(.5f);
			Instantiate(ZumbiDeSangue.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(.5f);
			Instantiate(ZumbiDeSangue.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(.5f);
			Instantiate(ZumbiDeSangue.gameObject, spawnPoint.position, spawnPoint.rotation);
		}
		if (waveIndex == 6)
        {
			Instantiate(Existido.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(2f);
			Instantiate(Existido.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(2f);
			Instantiate(Existido.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(2f);
			Instantiate(ZumbiDeSangue.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(.5f);
			Instantiate(ZumbiDeSangue.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(.5f);
			Instantiate(ZumbiDeSangue.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(.5f);
			Instantiate(ZumbiDeSangue.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(.5f);
			Instantiate(ZumbiDeSangue.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(3f);
			Instantiate(Mumia.gameObject, spawnPoint.position, spawnPoint.rotation);
		}
		if (waveIndex == 7)
		{
			Instantiate(Existido.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(2f);
			Instantiate(Mumia.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(2f);
			Instantiate(Mumia.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(2f);
			Instantiate(Mumia.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(4f);
			StartCoroutine(SpawnWave2());
		}
		if (waveIndex == 8)
        {
			Instantiate(Mumia.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(2f);
            for (int i = 0; i < 10; i++)
            {
				Instantiate(ZumbiDeSangue.gameObject, spawnPoint.position, spawnPoint.rotation);
				yield return new WaitForSeconds(1f);
			}

		}
		if (waveIndex == 9)
        {
			Instantiate(ExistidoMorte.gameObject, spawnPoint.position, spawnPoint.rotation);
        }
		if (waveIndex == 10)
        {
			Instantiate(Existido.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(1f);
			Instantiate(Existido.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(2f);
			Instantiate(ExistidoMorte.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(3f);
			Instantiate(ExistidoMorte.gameObject, spawnPoint.position, spawnPoint.rotation);
		}
		if (waveIndex == 11)
        {
			Instantiate(ExistidoMorte.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(1f);
			Instantiate(ExistidoMorte.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(5f);
            for (int i = 0; i < 5; i++)
            {
				Instantiate(Existido.gameObject, spawnPoint.position, spawnPoint.rotation);
				yield return new WaitForSeconds(2f);
            }
			for (int i = 0; i < 10; i++)
			{
				Instantiate(ZumbiDeSangue.gameObject, spawnPoint.position, spawnPoint.rotation);
				yield return new WaitForSeconds(1f);
			}
		}
		if (waveIndex == 12)
        {
			Instantiate(ZumbiBestial.gameObject, spawnPoint.position, spawnPoint.rotation);
        }
		if (waveIndex == 13)
        {
            for (int i = 0; i < 10; i++)
            {
				Instantiate(Mumia.gameObject, spawnPoint.position, spawnPoint.rotation);
				yield return new WaitForSeconds(3f);
            }
			StartCoroutine(SpawnWave2());
		}
		if (waveIndex == 14)
        {
			Instantiate(ZumbiBestial.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(3f);
			Instantiate(ZumbiBestial.gameObject, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(3f);
            for (int i = 0; i < 20; i++)
            {
				Instantiate(ZumbiDeSangue.gameObject, spawnPoint.position, spawnPoint.rotation);
				yield return new WaitForSeconds(.3f);
			}
		}
		if (waveIndex == 15)
        {
            for (int i = 0; i < 3; i++)
            {
				Instantiate(Mumia.gameObject, spawnPoint.position, spawnPoint.rotation);
				yield return new WaitForSeconds(.7f);
			}
			for (int i = 0; i < 4; i++)
			{
				Instantiate(ExistidoMorte.gameObject, spawnPoint.position, spawnPoint.rotation);
				yield return new WaitForSeconds(1f);
			}
			for (int i = 0; i < 25; i++)
            {
				Instantiate(ZumbiDeSangue.gameObject, spawnPoint.position, spawnPoint.rotation);
				yield return new WaitForSeconds(.2f);
            }
        }
		if (waveIndex == 16)
        {
			Instantiate(Anarquico.gameObject, spawnPoint.position, spawnPoint.rotation);
        }
		if (waveIndex == 17)
		{
            for (int i = 0; i < 5; i++)
            {
				Instantiate(Existido.gameObject, spawnPoint.position, spawnPoint.rotation);
				yield return new WaitForSeconds(.5f);
			}
            for (int i = 0; i < 4; i++)
            {
				Instantiate(ZumbiBestial.gameObject, spawnPoint.position, spawnPoint.rotation);
				yield return new WaitForSeconds(1f);
			}
            for (int i = 0; i < 3; i++)
            {
				Instantiate(Anarquico.gameObject, spawnPoint.position, spawnPoint.rotation);
				yield return new WaitForSeconds(2f);
			}
		}
		if (waveIndex == 18)
        {
            for (int i = 0; i < 10; i++)
            {
				Instantiate(Mumia.gameObject, spawnPoint.position, spawnPoint.rotation);
				yield return new WaitForSeconds(.8f);
			}
            for (int i = 0; i < 8; i++)
            {
				Instantiate(ExistidoMorte.gameObject, spawnPoint.position, spawnPoint.rotation);
				yield return new WaitForSeconds(1f);
			}
            for (int i = 0; i < 5; i++)
            {
				Instantiate(Anarquico.gameObject, spawnPoint.position, spawnPoint.rotation);
				yield return new WaitForSeconds(1.5f);
			}
        }
		if (waveIndex == 19)
        {
			StartCoroutine(SpawnWave2());
            for (int i = 0; i < 6; i++)
            {
				Instantiate(Anarquico.gameObject, spawnPoint.position, spawnPoint.rotation);
				yield return new WaitForSeconds(1f);
			}
            for (int i = 0; i < 8; i++)
            {
				Instantiate(ZumbiBestial.gameObject, spawnPoint.position, spawnPoint.rotation);
				yield return new WaitForSeconds(1f);
			}
        }
		if (waveIndex == 20)
        {
			Instantiate(Degolificada.gameObject, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(1f);
        }
		if (waveIndex == 21)
        {
			StartCoroutine(SpawnWave2());
            for (int i = 0; i < 5; i++)
            {
				Instantiate(Anarquico.gameObject, spawnPoint.position, spawnPoint.rotation);
				yield return new WaitForSeconds(1.4f);
            }
			yield return new WaitForSeconds(4f);
			for (int i = 0; i < 5; i++)
			{
				Instantiate(Anarquico.gameObject, spawnPoint.position, spawnPoint.rotation);
				yield return new WaitForSeconds(1.4f);
			}

		}
		if (waveIndex == 22)
        {
            for (int i = 0; i < 8; i++)
            {
				Instantiate(Mumia.gameObject, spawnPoint.position, spawnPoint.rotation);
				yield return new WaitForSeconds(.3f);
			}
			StartCoroutine(SpawnWave2());
            for (int i = 0; i < 7; i++)
            {
				Instantiate(ExistidoMorte.gameObject, spawnPoint.position, spawnPoint.rotation);
				yield return new WaitForSeconds(.2f);
			}
        }
		if (waveIndex == 23)
        {
			StartCoroutine(SpawnWave2());
            for (int i = 0; i < 15; i++)
            {
				Instantiate(Anarquico.gameObject, spawnPoint.position, spawnPoint.rotation);
				yield return new WaitForSeconds(1.3f);
			}
        }
		if (waveIndex == 40)
		{
			Instantiate(Carniçal.gameObject, spawnPoint.position, spawnPoint.rotation);
		}
		if (waveIndex == 60)
        {
			Instantiate(Viajante.gameObject, spawnPoint.position, spawnPoint.rotation);
        }


        yield return new WaitForSeconds(1f);
        PassouRound = true;
		ComeçoRoundRolando = false;
        }

	public IEnumerator SpawnWave2()
	{
		if (waveIndex == 7)
        {
            for (int i = 0; i < 10; i++)
            {
				Instantiate(ZumbiDeSangue.gameObject, spawnPoint.position, spawnPoint.rotation);
				yield return new WaitForSeconds(.2f);
			}
        }
		if (waveIndex == 13)
        {
            for (int i = 0; i < 20; i++)
            {
				Instantiate(ZumbiDeSangue.gameObject, spawnPoint.position, spawnPoint.rotation);
				yield return new WaitForSeconds(.7f);
			}
        }
		if (waveIndex == 19)
        {
            for (int i = 0; i < 30; i++)
            {
				Instantiate(ZumbiDeSangue.gameObject, spawnPoint.position, spawnPoint.rotation);
				yield return new WaitForSeconds(.2f);
			}
		}
		if (waveIndex == 21)
        {
			for (int i = 0; i < 15; i++)
			{
				Instantiate(Existido.gameObject, spawnPoint.position, spawnPoint.rotation);
				yield return new WaitForSeconds(1f);
			}
		}
		if (waveIndex == 22)
        {
            for (int i = 0; i < 4; i++)
            {
				Instantiate(ZumbiBestial.gameObject, spawnPoint.position, spawnPoint.rotation);
				yield return new WaitForSeconds(.4f);
			}
        }
		if (waveIndex == 23)
        {
            for (int i = 0; i < 40; i++)
            {
				Instantiate(ZumbiDeSangue.gameObject, spawnPoint.position, spawnPoint.rotation);
				yield return new WaitForSeconds(.4f);
			}
        }
	}

	public void Spawnar(Transform bixo)
	{
        Instantiate(bixo.gameObject, spawnPoint.position, spawnPoint.rotation);
    }





}