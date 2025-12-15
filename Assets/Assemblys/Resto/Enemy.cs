using System.Collections;
using UnityEngine;



public class Enemy : MonoBehaviour
{
	[Header("Dados Monstro")]

	public float speedEnemy = 5f;
	public float HpMonstro = 5;
	public float HpMonstro2 = 1.3f;

	[Header("Sistema")]
	public GameMaster gm;
	public GameObject impactEffect;
	private Transform target;
	public int wavepointIndex = 0;
	Animator animator1;
	public WaveSpawner wawaw;
	public float distanciaPercorrida = 0f;
	public float distanciaInicial = 0f;
	Vector3 lastPosition;
	public bool vivo = true;
	public bool andando = true;

	[Header("Tipo de Monstro")]
	public bool ExistidoNormal = false;
	public bool ExistidoAtivo = false;
	public bool ExistidoMorte = false;
	public bool ZumbiBestial = false;
	public bool ZumbiDeSangue = false;
	public bool Degolificada = false;
	public bool Carniçal = false;
	public bool Mumia = false;
	public bool Anarquico = false;
	public bool AnarquicoDescontrolado = false;
	public bool PerturbadoDeEnergia = false;
	public bool Viajante = false;
	public bool Vulto = false;

	[Header("Estados")]
	public bool Oculto;
	public bool Maldito;

	public bool Sangrando;
	public int SangrandoDano = 1;
	private float SangrandoCountdown = 3f;
	public float SangrandoDuração = 4f;
	private float SangrandoCountDuração = 4f;

	public bool PegandoFogo;
	public int PegandoFogoDano = 1;
	private float PegandoFogoCountdown = 0f;
	public float PegandoFogoDuração = 5f;
	private float PegandoFogoCountDuração = 5f;

	public GameObject OcultoObj;
	public GameObject MalditoObj;
	public GameObject SangrandoObj;
	public GameObject PegandoFogoObj;

	[Header("Fase2 Monstros")]
	public GameObject TransformarEfeito;
	public GameObject ExistidoNormalGO;
	public GameObject ExistidoAtivoGO;
	public float TimerHabilidade1 = 5f;
	public float CooldownH1 = 12f;
	public bool H1bool = false;

	[Header("Elemento do Outro Lado")]
	public bool MorteE;
	public bool SangueE;
	public bool ConhecimentoE;
	public bool EnergiaE;
	public bool MedoE;



	IEnumerator Start()
	{
		target = Waypoints.points[wavepointIndex];
		distanciaInicial = Waypoints.distanciaInicial;
		lastPosition = transform.position;
		animator1 = GetComponent<Animator>();
        yield return .2f;

		gm.mm.L_Criaturas.Add(gameObject);
		if (Maldito)
		{
			HpMonstro = Mathf.Round(HpMonstro * HpMonstro2);
		}
	}

	void Update()
	{		
		if (Vector3.Distance(transform.position, target.position) <= 0.4f)
		{
			GetNextWaypoint();
		}
		//distancia andada
		distanciaPercorrida += Vector3.Distance(transform.position, lastPosition);
		lastPosition = transform.position;


		if (!Maldito) { MalditoObj.SetActive(false); } else { MalditoObj.SetActive(true); }

		if (!Oculto) { OcultoObj.SetActive(false); } else { OcultoObj.SetActive(true); }

		if (Sangrando)
		{
			if (SangrandoCountDuração > 0f)
			{
				if (SangrandoCountdown <= 0f)
				{
					HpMonstro -= SangrandoDano;
					GameObject effectIns = (GameObject)Instantiate(SangrandoObj, transform.position, transform.rotation);
					effectIns.transform.position = new Vector3(effectIns.transform.position.x, effectIns.transform.position.y + 1, effectIns.transform.position.z);
					Destroy(effectIns, 1f);
					SangrandoCountdown = 3f;
				}
				SangrandoCountdown -= Time.deltaTime;
				SangrandoCountDuração -= Time.deltaTime;
			}
			else { Sangrando = false; }
		} else { SangrandoCountDuração = SangrandoDuração; }

		if (PegandoFogo)
		{
			if (PegandoFogoCountDuração > 0f)
			{
				if (PegandoFogoCountdown <= 0f)
				{
					HpMonstro -= PegandoFogoDano;
					GameObject effectIns = (GameObject)Instantiate(SangrandoObj, transform.position, transform.rotation);
					effectIns.transform.position = new Vector3(effectIns.transform.position.x, effectIns.transform.position.y + 1, effectIns.transform.position.z);
					Destroy(effectIns, 1f);
					PegandoFogoObj.SetActive(true);
					PegandoFogoCountdown = 2f;
				}
				PegandoFogoCountdown -= Time.deltaTime;
				PegandoFogoCountDuração -= Time.deltaTime;
			}
			else { PegandoFogo = false; }
		} else { PegandoFogoCountDuração = PegandoFogoDuração; PegandoFogoObj.SetActive(false); }


		//Código Dos Existidos Normais / 3hp
		if (ExistidoNormal)
		{
			ConhecimentoE = true;
			if (HpMonstro <= 4)
			{
				GameObject effectIns = (GameObject)Instantiate(TransformarEfeito, transform.position, transform.rotation);
				effectIns.transform.position = new Vector3(effectIns.transform.position.x, effectIns.transform.position.y + 1, effectIns.transform.position.z);
				Destroy(effectIns, 1f);
				ExistidoAtivoGO.SetActive(true);
				ExistidoNormalGO.SetActive(false);

				ExistidoNormal = false;
				ExistidoAtivo = true;
				speedEnemy = speedEnemy * 2;
			}


			Vector3 dir = target.position - transform.position;
			transform.Translate(dir.normalized * speedEnemy * Time.deltaTime, Space.World);
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 3f);

			return;
		}


		//Código Existidos Ativos / 5hp
		if (ExistidoAtivo)
		{
			ConhecimentoE = true;
			if (HpMonstro <= 0)
			{
				GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
				effectIns.transform.position = new Vector3(effectIns.transform.position.x, effectIns.transform.position.y + 1, effectIns.transform.position.z);
				Destroy(effectIns, 1f);
                gm.mm.L_Criaturas.Remove(gameObject);
				Destroy(gameObject);
			}

			Vector3 dir = target.position - transform.position;
			transform.Translate(dir.normalized * speedEnemy * Time.deltaTime, Space.World);
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 3f);
			

			return;
		}


		//Código Dos Existidos de Morte
		if (ExistidoMorte)

		{
			MorteE = true;
			animator1.speed = speedEnemy / speedEnemy;

			if (andando)
            {
				Vector3 dir = target.position - transform.position;
				transform.Translate(dir.normalized * speedEnemy * Time.deltaTime, Space.World);
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 3f);				
			}

			if (HpMonstro <= 0)
			{
				GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
				effectIns.transform.position = new Vector3(effectIns.transform.position.x, effectIns.transform.position.y + 1, effectIns.transform.position.z);
				Destroy(effectIns, 1f);
                gm.mm.L_Criaturas.Remove(gameObject);
				Destroy(gameObject);
			}
			return;
		}


		//Código dos Zumbis de Sangue
		if (ZumbiDeSangue)
		{
			SangueE = true;
			animator1.speed = speedEnemy / speedEnemy;

			if (andando)
			{
				Vector3 dir = target.position - transform.position;
				transform.Translate(dir.normalized * speedEnemy * Time.deltaTime, Space.World);
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 3f);
			}

			if (HpMonstro <= 0)
			{
				GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
				effectIns.transform.position = new Vector3(effectIns.transform.position.x, effectIns.transform.position.y + 1, effectIns.transform.position.z);
				Destroy(effectIns, 1f);
                gm.mm.L_Criaturas.Remove(gameObject);
				Destroy(gameObject);
			}
			return;
		}


		//Código do Zumbis Bestiais
		if (ZumbiBestial)
		{
			SangueE = true;
			animator1.speed = speedEnemy / speedEnemy;

			if (andando)
			{
				Vector3 dir = target.position - transform.position;
				transform.Translate(dir.normalized * speedEnemy * Time.deltaTime, Space.World);
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 10f);
			}

			if (HpMonstro <= 0)
			{
				GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
				effectIns.transform.position = new Vector3(effectIns.transform.position.x, effectIns.transform.position.y + 1, effectIns.transform.position.z);
				Destroy(effectIns, 1f);
                gm.mm.L_Criaturas.Remove(gameObject);
				Destroy(gameObject);
			}
			return;
		}


		//Código da Degolificada
		if (Degolificada)
		{
			MedoE = true;
			animator1.speed = speedEnemy / speedEnemy;

			if (TimerHabilidade1 <= 0)
            {
				animator1.Play("DegolificadaAnimGrito");
				TimerHabilidade1 = CooldownH1;
            } else { TimerHabilidade1 -= Time.deltaTime; }



			if (andando)
			{
				Vector3 dir = target.position - transform.position;
				transform.Translate(dir.normalized * speedEnemy * Time.deltaTime, Space.World);
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 3f);
			}

			if (HpMonstro <= 0)
			{
				GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
				effectIns.transform.position = new Vector3(effectIns.transform.position.x, effectIns.transform.position.y + 1, effectIns.transform.position.z);
				Destroy(effectIns, 1f);
                gm.mm.L_Criaturas.Remove(gameObject);
				Destroy(gameObject);
			}
			return;
		}


		//Código do Carniçal
		if (Carniçal)
		{
			MorteE = true;
			animator1.speed = speedEnemy / speedEnemy;

			if (TimerHabilidade1 <= 0)
			{
				animator1.SetBool("Especial1", true);
				AudioSource fx = GetComponent<AudioSource>(); fx.PlayOneShot(fx.clip);
				TimerHabilidade1 = CooldownH1;
			}
			else { TimerHabilidade1 -= Time.deltaTime; }

			if (andando)
			{
				Vector3 dir = target.position - transform.position;
				transform.Translate(dir.normalized * speedEnemy * Time.deltaTime, Space.World);
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 3f);
			}

			if (HpMonstro <= 0)
			{
				GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
				effectIns.transform.position = new Vector3(effectIns.transform.position.x, effectIns.transform.position.y + 1, effectIns.transform.position.z);
				Destroy(effectIns, 1f);
                gm.mm.L_Criaturas.Remove(gameObject);
				Destroy(gameObject);
			}
			return;
		}


		//Código da Mumia
		if (Mumia)
		{
			MorteE = true;
			animator1.speed = speedEnemy / speedEnemy;

			
			if (andando)
			{
				Vector3 dir = target.position - transform.position;
				transform.Translate(dir.normalized * speedEnemy * Time.deltaTime, Space.World);
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 3f);
			}

			if (HpMonstro <= 0)
			{
				GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
				effectIns.transform.position = new Vector3(effectIns.transform.position.x, effectIns.transform.position.y + 1, effectIns.transform.position.z);
				Destroy(effectIns, 1f);
                gm.mm.L_Criaturas.Remove(gameObject);
				Destroy(gameObject);
			}
			return;
		}


		//Código dos Anarquico
		if (Anarquico)
		{
			EnergiaE = true;
			animator1.speed = speedEnemy / speedEnemy;

			if (andando)
			{
				Vector3 dir = target.position - transform.position;
				transform.Translate(dir.normalized * speedEnemy * Time.deltaTime, Space.World);
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 3f);
			}

			if (HpMonstro <= 0)
			{
				GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
				effectIns.transform.position = new Vector3(effectIns.transform.position.x, effectIns.transform.position.y + 1, effectIns.transform.position.z);
				Destroy(effectIns, 1f);
                gm.mm.L_Criaturas.Remove(gameObject);
				Destroy(gameObject);
			}
			return;
		}


		//Código dos Anarquico
		if (AnarquicoDescontrolado)
		{
			EnergiaE = true;
			animator1.speed = (speedEnemy / speedEnemy) + .5f;

			if (andando)
			{
				Vector3 dir = target.position - transform.position;
				transform.Translate(dir.normalized * speedEnemy * Time.deltaTime, Space.World);
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 3f);
			}

			if (HpMonstro <= 0)
			{
				GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
				effectIns.transform.position = new Vector3(effectIns.transform.position.x, effectIns.transform.position.y + 1, effectIns.transform.position.z);
				Destroy(effectIns, 1f);
                gm.mm.L_Criaturas.Remove(gameObject);
				Destroy(gameObject);
			}
			return;
		}


		//Código dos Perturbados De Energia
		if (PerturbadoDeEnergia)
		{
			EnergiaE = true;
			animator1.speed = speedEnemy / speedEnemy;

			if (andando)
			{
				Vector3 dir = target.position - transform.position;
				transform.Translate(dir.normalized * speedEnemy * Time.deltaTime, Space.World);
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 3f);
			}

			if (HpMonstro <= 0)
			{
				GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
				effectIns.transform.position = new Vector3(effectIns.transform.position.x, effectIns.transform.position.y + 1, effectIns.transform.position.z);
				Destroy(effectIns, 1f);
                gm.mm.L_Criaturas.Remove(gameObject);
				Destroy(gameObject);
			}
			return;
		}


		//Código do Viajante
		if (Viajante)
		{
			EnergiaE = true;
			ConhecimentoE = true;
			MedoE = true;
			

			if (TimerHabilidade1 <= 0)
			{
				AudioSource fx = GetComponent<AudioSource>(); fx.PlayOneShot(fx.clip);
				AtaqueEspecial();
				TimerHabilidade1 = CooldownH1;
			}
			else { TimerHabilidade1 -= Time.deltaTime; }

			if (andando)
			{
				Vector3 dir = target.position - transform.position;
				transform.Translate(dir.normalized * speedEnemy * Time.deltaTime, Space.World);
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 4f);
			}

			if (HpMonstro <= 0)
			{
				GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
				effectIns.transform.position = new Vector3(effectIns.transform.position.x, effectIns.transform.position.y + 1, effectIns.transform.position.z);
				Destroy(effectIns, 1f);
                gm.mm.L_Criaturas.Remove(gameObject);
				Destroy(gameObject);
			}
			return;
		}


		//Código do Vulto
		if (Vulto)
		{
			ConhecimentoE = true;
			animator1.speed = speedEnemy / speedEnemy;


			if (andando)
			{
				Vector3 dir = target.position - transform.position;
				transform.Translate(dir.normalized * speedEnemy * Time.deltaTime, Space.World);
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 3f);
			}

			if (HpMonstro <= 0)
			{
				GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
				effectIns.transform.position = new Vector3(effectIns.transform.position.x, effectIns.transform.position.y + 1, effectIns.transform.position.z);
				Destroy(effectIns, 1f);
                gm.mm.L_Criaturas.Remove(gameObject);
				Destroy(gameObject);
			}
			return;
		}
	}
	void GetNextWaypoint()
	{
		if (wavepointIndex >= Waypoints.points.Length - 1)
		{
			vivo = false;
            gm.mm.L_Criaturas.Remove(gameObject);
			Destroy(gameObject);
            gm.mm.ValorVida -= (int)Mathf.Ceil(HpMonstro);
            gm.mm.ValorSanidade -= (int)Mathf.Ceil(HpMonstro / 2);
			return;
		}
		wavepointIndex++;
		target = Waypoints.points[wavepointIndex];
	}
	public void AndarSwitch()
    {
		if (andando)
        {
			andando = false;
        }
        else
        {
			andando = true;
        }
    }

	public void AtaqueEspecial()
    {
		if (Degolificada)
        {
			AudioSource fx = GetComponent<AudioSource>(); fx.PlayOneShot(fx.clip);
			speedEnemy += .5f;
            gm.mm.ValorSanidade -= 15;
		}
		if (Carniçal)
        {
			animator1.SetBool("Especial1", false);
			GameObject Inimigo = Instantiate(wawaw.ExistidoMorte.gameObject, transform.position, transform.rotation);
			Inimigo.GetComponent<Enemy>().wavepointIndex = wavepointIndex;
			GameObject effectIns = (GameObject)Instantiate(TransformarEfeito, transform.position, transform.rotation);
			Destroy(effectIns, 0.3f);
		}
		if (Viajante)
        {
			//Debug.Log(TransformarEfeito.GetComponent<Renderer>().material.color);
			if (!H1bool)
			{
				H1bool = true;
				speedEnemy -= 2;
				animator1.speed = (speedEnemy / speedEnemy) - .5f;
				//TransformarEfeito.GetComponent<Renderer>().material.shader = Shader.Find("Monstros/ViajanteMudado");
				TransformarEfeito.GetComponent<Renderer>().material.color = new Color(1, 1, 1, .1f);
				TransformarEfeito.GetComponent<Renderer>().material.SetFloat("_Mode", 3);
				TransformarEfeito.GetComponent<Renderer>().material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
				TransformarEfeito.GetComponent<Renderer>().material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
				TransformarEfeito.GetComponent<Renderer>().material.DisableKeyword("_ALPHATEST_ON");
				TransformarEfeito.GetComponent<Renderer>().material.EnableKeyword("_ALPHABLEND_ON");
				TransformarEfeito.GetComponent<Renderer>().material.DisableKeyword("_ALPHAPREMULTPLY_ON");
				TransformarEfeito.GetComponent<Renderer>().material.renderQueue = 3000;
				GetComponent<CapsuleCollider>().enabled = false; //fazer pegar as outras capsulaas e mais divertido
            }
			else
            {
				H1bool = false;
				speedEnemy += 2;
				animator1.speed = speedEnemy / speedEnemy;
				//TransformarEfeito.GetComponent<Renderer>().material.shader = Shader.Find("Monstros/ViajanteNormal");
				TransformarEfeito.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1.0f);
				TransformarEfeito.GetComponent<Renderer>().material.SetFloat("_Mode", 0);
				TransformarEfeito.GetComponent<Renderer>().material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
				TransformarEfeito.GetComponent<Renderer>().material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
				TransformarEfeito.GetComponent<Renderer>().material.DisableKeyword("_ALPHATEST_ON");
				TransformarEfeito.GetComponent<Renderer>().material.renderQueue = -1;
				GetComponent<CapsuleCollider>().enabled = true;
			}

		}
    }
}