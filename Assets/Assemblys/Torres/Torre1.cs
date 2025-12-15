using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Audio;
public class Torre1 : MonoBehaviour {


	private Animator animator;
	private Transform target;

	[Header("Attributes")]

	public bool balaSegue = false;
	public float range = 5f;
	public float RangeFinal = 0f;
	public float fireRate = 1f;
	public float velocidadeBala = 10f;
	public float rotaçãoBala = 10f;
	public float pierceBala = 1;
	public float lifeBala = 1.5f;
	public int DanoTotal = 1;
	public int DanoInfligido = 0;
	public int DanoAplicado = 0;
	public int DanoAplicadoTotal = 0;

	[Header("PodeAtirarEm")]

	public bool ocultoAcerta;

	public bool ocultoAcertaTemporario;

	public bool cadenciaTemporaria;
	public bool rangeTemporario;

	public bool danoTemporario1;
	public bool danoTemporario2;
	public bool danoTemporario3;
	public bool danoTemporario4;

	[Header("Unity Setup Fields")]

	public string enemyTag = "Enemy";
	public Transform partToRotate;
	public float turnSpeed = 10f;
	public GameObject Celular;
	public GameObject bulletPrefab;
	public GameObject ArmaMele;
	public Transform firePoint;
	public float SpeedInicial;
	public bool atirando;
	public float AnimatorSpeed;
	public GameObject AreaAtiravel;
	public Text AlvoTexto;

	[Header("N�o Mexer")]
	int EscolhaDeTarget = 1;
	public GameObject bl;
	public ComprarTorre cpt;
	public GameMaster gm;
	ScriptUpgrades UpScript;
	Enemy nme;
	MenuUpgd menuU;
	Bala1 bala1;
	CelularS cell;
	public WaveSpawner wawaw;
	public CardAgente cardOrigem;

    public GameObject TargetAtualT1;

	[Header("Habilidades Coisas")]
	public float timerHabilidade1 = 0f;
	public float timerHabilidade2 = 0f;
	public bool prontaHabilidade1 = true;
	public bool prontaHabilidade2 = true;
	public float CooldownH_1 = 5f;
	public float CooldownH_2 = 5f;
	public Color H_nao;
	public Color H_sim;

	[Header("Som")]

	public AudioSource somTiro1;
	public AudioSource somTiro2;

	[Header("Bonus")]

	public int BonusDinheiro;
	public int BonusEnumeratorDinheiro = 3;
	public int BonusVida;
	public int BonusSanidade;
	public float BonusCadencia = 1f;
	public int BonusDano;
	public float BonusRange = 0f;

	bool samuelDinBonus = false;
	bool x1 = false;
	bool x2 = false;
	bool x3 = false;
	bool y1 = false;
	bool y2 = false;
	//bool y3 = false;
	public bool vendeuCORTAREFEITOS = false;

	void Start()
	{
		animator = gameObject.GetComponent<Animator>();
		UpScript = gameObject.GetComponent<ScriptUpgrades>();
		cell = Celular.GetComponent<CelularS>();
		InvokeRepeating("UpdateTarget", 0f, 0.01f);
		menuU = gameObject.GetComponent<MenuUpgd>();
		SpeedInicial = animator.speed;
	}

	public void ChangeTarget()
	{
		EscolhaDeTarget++;
		if (EscolhaDeTarget == 2)
		{
			AlvoTexto.text = "Alvo:\nO �ltimo.";
		}
		if (EscolhaDeTarget == 3)
		{
			AlvoTexto.text = "Alvo:\nO Mais Forte.";
		}
		if (EscolhaDeTarget == 4)
		{
			AlvoTexto.text = "Alvo:\nO Mais Perto.";
		}
		if (EscolhaDeTarget == 5)
		{
			AlvoTexto.text = "Alvo:\nO Mais Longe.";
		}
		if (EscolhaDeTarget > 5)
		{
			EscolhaDeTarget = 1;
			AlvoTexto.text = "Alvo:\nO Primeiro.";
		}

	}

	public void AtaqueEvent()
	{
		//Tiro
		if (!menuU.DanoMele)
		{
			if (target != null)
			{
				if (balaSegue)
				{
					Vector3 dir = target.position - transform.position;
					Quaternion lookRotation = Quaternion.LookRotation(dir);
					Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
					partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

					bl = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
					bala1 = bl.GetComponent<Bala1>();
					//bala1.Seek(target);
					bala1.TargetAtual = TargetAtualT1;
					bala1.Cerne = gameObject;
					bala1.pierce = pierceBala;
					AudioSource fx = somTiro1.GetComponent<AudioSource>(); fx.PlayOneShot(fx.clip);
				}
				else
				{
					Vector3 dir = target.position - transform.position;
					Quaternion lookRotation = Quaternion.LookRotation(dir);
					Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
					partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

					bl = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
					bala1 = bl.GetComponent<Bala1>();
					bala1.TargetAtual = TargetAtualT1;
					bala1.Cerne = gameObject;
					bala1.pierce = pierceBala;
					bl.GetComponent<Rigidbody>().linearVelocity = bl.transform.forward * velocidadeBala;
					AudioSource fx = somTiro1.GetComponent<AudioSource>(); fx.PlayOneShot(fx.clip);
				}
			}
			else
			{
				animator.SetBool("AtirandoPistola", false);
			}
		}
	}


	public void RotaçãoMele()
	{
		if (target != null)
		{
			Vector3 dir = target.position - partToRotate.position;
			Quaternion lookRotation = Quaternion.LookRotation(dir);
			Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
			partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
		}
	}

	void UpdateTarget()
	{
		//Update Target
		float faerstDistance = 0f;
		float shortestDistance = Mathf.Infinity;
		float maiorVida = 0;
		float disPerPrimeiro = 0f;
		float disPerUltimo = Mathf.Infinity;
		if (gm.mm.L_Criaturas.Count > 0)
		{
			foreach (GameObject enemy in gm.mm.L_Criaturas)
			{
				if (enemy == null)
				{
					target = null;
					animator.SetBool("AtirandoPistola", false);
					atirando = false;
					return;
				}
				nme = enemy.GetComponent<Enemy>();
				float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);


				//Inimigo Primeiro
				if (EscolhaDeTarget == 1)
				{
					if (distanceToEnemy <= RangeFinal)
					{
						if (nme.distanciaPercorrida > disPerPrimeiro)
						{
							if (ocultoAcerta)
							{
								shortestDistance = distanceToEnemy;
								disPerPrimeiro = nme.distanciaPercorrida;
								TargetAtualT1 = enemy;
							}
							else if (ocultoAcertaTemporario)
							{
								shortestDistance = distanceToEnemy;
								disPerPrimeiro = nme.distanciaPercorrida;
								TargetAtualT1 = enemy;
							}
							else
							{
								if (!nme.Oculto)
								{
									shortestDistance = distanceToEnemy;
									disPerPrimeiro = nme.distanciaPercorrida;
									TargetAtualT1 = enemy;
								}
							}
						}

					}


					if (TargetAtualT1 != null && shortestDistance <= RangeFinal)
					{
						target = TargetAtualT1.transform;
						animator.SetBool("AtirandoPistola", true);
						atirando = true;
					}
					else
					{
						target = null;
						animator.SetBool("AtirandoPistola", false);
						atirando = false;
					}
				}

				//Inimigo Ultimo
				else if (EscolhaDeTarget == 2)
				{
					if (distanceToEnemy <= RangeFinal)
					{
						if (nme.distanciaPercorrida < disPerUltimo)
						{
							if (ocultoAcerta)
							{
								shortestDistance = distanceToEnemy;
								disPerUltimo = nme.distanciaPercorrida;
								TargetAtualT1 = enemy;
							}
							else if (ocultoAcertaTemporario)
							{
								shortestDistance = distanceToEnemy;
								disPerUltimo = nme.distanciaPercorrida;
								TargetAtualT1 = enemy;
							}
							else
							{
								if (!nme.Oculto)
								{
									shortestDistance = distanceToEnemy;
									disPerUltimo = nme.distanciaPercorrida;
									TargetAtualT1 = enemy;
								}
							}
						}

					}


					if (TargetAtualT1 != null && shortestDistance <= RangeFinal)
					{
						target = TargetAtualT1.transform;
						animator.SetBool("AtirandoPistola", true);
						atirando = true;
					}
					else
					{
						target = null;
						animator.SetBool("AtirandoPistola", false);
						atirando = false;
					}
				}

				//Inimigo mais forte
				else if (EscolhaDeTarget == 3)
				{

					if (distanceToEnemy < shortestDistance)
					{
						if (distanceToEnemy <= RangeFinal)
						{
							if (nme.HpMonstro >= maiorVida)
							{
								if (ocultoAcerta)
								{
									shortestDistance = distanceToEnemy;
									maiorVida = nme.HpMonstro;
									TargetAtualT1 = enemy;
								}
								else if (ocultoAcertaTemporario)
								{
									shortestDistance = distanceToEnemy;
									maiorVida = nme.HpMonstro;
									TargetAtualT1 = enemy;
								}
								else
								{
									if (!nme.Oculto)
									{
										shortestDistance = distanceToEnemy;
										maiorVida = nme.HpMonstro;
										TargetAtualT1 = enemy;
									}
								}
							}
						}
					}


					if (TargetAtualT1 != null && shortestDistance <= RangeFinal)
					{
						target = TargetAtualT1.transform;
						animator.SetBool("AtirandoPistola", true);
						atirando = true;
					}
					else
					{
						target = null;
						animator.SetBool("AtirandoPistola", false);
						atirando = false;
					}
				}

				//Inimigo mais perto
				else if (EscolhaDeTarget == 4)
				{
					if (distanceToEnemy < shortestDistance)
					{
						if (ocultoAcerta)
						{
							shortestDistance = distanceToEnemy;
							TargetAtualT1 = enemy;
						}
						else if (ocultoAcertaTemporario)
						{
							shortestDistance = distanceToEnemy;
							TargetAtualT1 = enemy;
						}
						else
						{
							if (!nme.Oculto)
							{
								shortestDistance = distanceToEnemy;
								TargetAtualT1 = enemy;
							}
						}

					}


					if (TargetAtualT1 != null && shortestDistance <= RangeFinal)
					{
						target = TargetAtualT1.transform;
						animator.SetBool("AtirandoPistola", true);
						atirando = true;
					}
					else
					{
						target = null;
						animator.SetBool("AtirandoPistola", false);
						atirando = false;
					}
				}

				//Inimigo mais Longe
				else if (EscolhaDeTarget == 5)
				{
					if (distanceToEnemy > faerstDistance)
					{
						if (distanceToEnemy <= RangeFinal)
						{
							if (ocultoAcerta)
							{
								faerstDistance = distanceToEnemy;
								TargetAtualT1 = enemy;
							}
							else if (ocultoAcertaTemporario)
							{
								faerstDistance = distanceToEnemy;
								TargetAtualT1 = enemy;
							}
							else
							{
								if (!nme.Oculto)
								{
									faerstDistance = distanceToEnemy;
									TargetAtualT1 = enemy;
								}
							}
						}
					}


					if (TargetAtualT1 != null && faerstDistance <= RangeFinal)
					{
						target = TargetAtualT1.transform;
						animator.SetBool("AtirandoPistola", true);
						atirando = true;
					}
					else
					{
						target = null;
						animator.SetBool("AtirandoPistola", false);
						atirando = false;
					}
				}

			}
		}
		else
        {
			target = null;
			animator.SetBool("AtirandoPistola", false);
			atirando = false;
		}
	}

    // ç = �	ã = �

    // Update is called once per frame
    void Update()
	{
		menuU.DanoCausado.text = "Dano: " + DanoAplicadoTotal;
		DanoInfligido = DanoTotal + BonusDano;
		RangeFinal = range + BonusRange;



		if (wawaw.RoundRolando || wawaw.ComeçoRoundRolando)
		{
			timerHabilidade1 -= Time.deltaTime;
			timerHabilidade2 -= Time.deltaTime;
		}

		if (AreaAtiravel.activeSelf == true)
		{
			AreaAtiravel.transform.localScale = new Vector3(RangeFinal * 2f, AreaAtiravel.transform.localScale.y, RangeFinal * 2f);
		}


        Agente_Base[] AgentesProximos = gm.mm.L_Agentes.ToArray();

        //Dante Coisas
        if (menuU.agenteDante)
		{			
			foreach (Agente_Base agenteProximo in AgentesProximos)
			{
				float distanciaAgente = Vector3.Distance(transform.position, agenteProximo.transform.position);
				if (distanciaAgente <= range + BonusRange)
				{
					if (!vendeuCORTAREFEITOS)
					{
						//2x0x0
						if (gameObject.GetComponent<ScriptUpgrades>().Upgrade1 == 2)//D� Range Temporaria a outros
						{
							if (!agenteProximo.GetComponent<Torre1>().rangeTemporario)
							{
								agenteProximo.GetComponent<Torre1>().rangeTemporario = true;
							}
						}

						//3x0x0
						if (gameObject.GetComponent<ScriptUpgrades>().Upgrade1 == 3) //D� Cadencia Temporaria a outros
						{
							if (!agenteProximo.GetComponent<Torre1>().cadenciaTemporaria)
							{
								agenteProximo.GetComponent<Torre1>().cadenciaTemporaria = true;
							}
						}

						//0x0x2
						if (UpScript.Upgrade3 >= 2) //Faz os outros verem Ocultos
						{
							if (!agenteProximo.GetComponent<Torre1>().ocultoAcerta)
							{
								agenteProximo.GetComponent<Torre1>().ocultoAcertaTemporario = true;
							}
						}

					}
					else
					{
						agenteProximo.GetComponent<Torre1>().rangeTemporario = false;
						agenteProximo.GetComponent<Torre1>().ocultoAcertaTemporario = false;
						agenteProximo.GetComponent<Torre1>().cadenciaTemporaria = false;
					}
				}
			}

			//Habilidade 0x0x3
			if (UpScript.Upgrade3 >= 3)
			{
				if (timerHabilidade1 <= 0)
				{					
					cell.Habilidade1.GetComponent<Button>().enabled = true;
					cell.Habilidade1.GetComponent<Image>().color = H_sim;
					cell.Habilidade1Sprite.SetActive(true);
					cell.txtH_1.SetActive(false);

				}
				else
				{
					cell.Habilidade1.GetComponent<Button>().enabled = false;
					cell.Habilidade1.GetComponent<Image>().color = H_nao;
					cell.Habilidade1Sprite.SetActive(false);
					cell.txtH_1.SetActive(true);
					cell.txtH_1.GetComponent<Text>().text = Mathf.Round(timerHabilidade1).ToString();
				}
			}
		}


		//Samuel Coisas
		if (menuU.agenteSamuel)
		{
			if (!vendeuCORTAREFEITOS)
			{
				if (!samuelDinBonus)
				{
					BonusDinheiro = 15;
					samuelDinBonus = true;
				}

				if (gameObject.GetComponent<ScriptUpgrades>().Upgrade1 == 1)
				{
					if (!x1)
					{
						BonusDinheiro += 15;
						x1 = true;
					}
				}
				if (gameObject.GetComponent<ScriptUpgrades>().Upgrade1 == 2)
				{
					if (!x2)
					{
						BonusDinheiro += 30;
						x2 = true;
					}
				}
				if (gameObject.GetComponent<ScriptUpgrades>().Upgrade1 == 3)
				{
					if (!x3)
					{
						BonusDinheiro += 30;
						x3 = true;
					}
				}

				if (gameObject.GetComponent<ScriptUpgrades>().Upgrade3 == 1)
				{
					if (!y1)
					{

						y1 = true;
					}
				}
				if (gameObject.GetComponent<ScriptUpgrades>().Upgrade3 == 2)
				{
					if (!y2)
					{
						GameObject[] aliados = GameObject.FindGameObjectsWithTag("Agente");
						foreach (GameObject aliado in aliados)
						{
							float distanciaAgente = Vector3.Distance(transform.position, aliado.transform.position);
							if (distanciaAgente <= range + BonusRange)
							{
								aliado.GetComponent<ScriptUpgrades>().up1Preço -= (aliado.GetComponent<ScriptUpgrades>().up1Preço / (int)1.15);
								aliado.GetComponent<ScriptUpgrades>().up2Preço -= (aliado.GetComponent<ScriptUpgrades>().up2Preço / (int)1.15);
								aliado.GetComponent<ScriptUpgrades>().up3Preço -= (aliado.GetComponent<ScriptUpgrades>().up3Preço / (int)1.15);


								//criar valores fixos de desconto no Script Updsaij desse jeito n da
							}
						}
						y2 = true;
					}
				}
				if (gameObject.GetComponent<ScriptUpgrades>().Upgrade3 == 3)
				{
					if (!y2)
					{

						y2 = true;
					}
				}

			}
			else
			{
				BonusDinheiro = 0;
				BonusEnumeratorDinheiro = 0;
			}
		}


		//Balu Coisas
		if (menuU.agenteBalu)
		{


		}


		//Rubens Coisas
		if (menuU.agenteRubens) //bugado
		{
			foreach (Agente_Base agenteProximo in AgentesProximos)
            {
				if (agenteProximo != gameObject)
                {
					if (agenteProximo != null)
					{
						if (agenteProximo.GetComponent<MenuUpgd>().agenteBalu)
						{
							Agente_Base BaluMaisProximo;
							if (!vendeuCORTAREFEITOS)
                            {
								float distanciaAgente = Vector3.Distance(transform.position, agenteProximo.transform.position);
								float shortestDistance = Mathf.Infinity;
								if (distanciaAgente <= range + BonusRange)
								{
									//0x1x0
									if (UpScript.Upgrade2 >= 1)
									{
										if (!x1)
										{
											BonusSanidade += 2;
											BonusVida += 3;
											x1 = true;
										}
										if (agenteProximo.GetComponent<Torre1>().vendeuCORTAREFEITOS)
										{
                                            BonusSanidade -= 2;
                                            BonusVida -= 3;
											x1 = false;
                                        }
									}

									if (distanciaAgente < shortestDistance)
									{
										shortestDistance = distanciaAgente;
										BaluMaisProximo = agenteProximo;
										//0x2x0
										if (UpScript.Upgrade2 >= 2)
										{
											agenteProximo.GetComponent<Torre1>().danoTemporario1 = true;
										}
									}
								}
							}
							else
							{
								BaluMaisProximo = null;
								agenteProximo.GetComponent<Torre1>().danoTemporario1 = false;
							}
						}
					}
					
				}				
            }

			//Habilidade 0x3x0
			if (UpScript.Upgrade2 >= 3)
			{
				if (timerHabilidade1 <= 0)
				{
					cell.Habilidade1.GetComponent<Button>().enabled = true;
					cell.Habilidade1.GetComponent<Image>().color = H_sim;
					cell.Habilidade1Sprite.SetActive(true);
					cell.txtH_1.SetActive(false);

				}
				else
				{
					cell.Habilidade1.GetComponent<Button>().enabled = false;
					cell.Habilidade1.GetComponent<Image>().color = H_nao;
					cell.Habilidade1Sprite.SetActive(false);
					cell.txtH_1.SetActive(true);
					cell.txtH_1.GetComponent<Text>().text = Mathf.Round(timerHabilidade1).ToString();
				}
			}
		}




		//Bonus Coisas

		if (rangeTemporario)
		{
			BonusRange = 2f;
		} else { BonusRange = 0f; }

		if (cadenciaTemporaria)
		{
			BonusCadencia = 1.5f;
		} else { BonusCadencia = 0f; }

		if (danoTemporario1)
		{
			BonusDano = 1;
		}
		if (danoTemporario2)
		{
			BonusDano = 2;
		}
		if (danoTemporario3)
		{
			BonusDano = 3;
		}
		if (danoTemporario4)
		{
			BonusDano = 3;
		}
		else
		{
			BonusDano = 0;
		}



		if (atirando)
		{
			animator.speed = SpeedInicial + fireRate;
			somTiro1.GetComponent<AudioSource>().pitch = 1;
		}
		else
		{
			animator.speed = SpeedInicial;
			somTiro1.GetComponent<AudioSource>().pitch = 1;
		}
		AnimatorSpeed = animator.speed;
	}

	public IEnumerator Começo()
    {
		if (BonusDinheiro >= 1)
		{
			for (int i = 0; i < BonusEnumeratorDinheiro; i++)
			{
                gm.mm.ValorDinheiro += BonusDinheiro;
				yield return new WaitForSeconds(1f);
			}
		}
		if (BonusVida >= 1)
        {
			if (gm.mm.ValorVida < 100)
			{
				if (gm.mm.ValorVida + BonusVida >= 100)
				{
                    gm.mm.ValorVida = 100;
                }
				else
				{
                    gm.mm.ValorVida += BonusVida;
                }
			}
		}
        if (BonusSanidade >= 1)
        {
            if (gm.mm.ValorSanidade < 100)
            {
                if (gm.mm.ValorSanidade + BonusSanidade >= 100)
                {
                    gm.mm.ValorSanidade = 100;
                }
                else
                {
                    gm.mm.ValorSanidade += BonusSanidade;
                }
            }
        }
	}

	public void Habilidade1()
    {
		if (menuU.agenteDante)
        {
            gm.mm.ValorVida += 5;
            gm.mm.ValorSanidade += 5;
		}
        if (menuU.agenteRubens)
        {
			Debug.Log("Habilidade Rubens");

        }

		timerHabilidade1 = CooldownH_1;
	}

	public void Habilidade2()
	{
		timerHabilidade2 = CooldownH_2;
	}

	public void DanoCausado()
	{
		if (nme.HpMonstro < DanoInfligido)
        {
			DanoAplicado = (int)nme.HpMonstro;
        }
		else
        {
			DanoAplicado = DanoInfligido;
        }

		nme.HpMonstro -= DanoInfligido;
        gm.mm.ValorDinheiro += DanoAplicado;
		DanoAplicadoTotal += DanoAplicado;
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}


}