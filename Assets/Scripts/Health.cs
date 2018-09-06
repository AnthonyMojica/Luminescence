using UnityEngine;
using System.Collections;
public class Health : MonoBehaviour
{

    public float maxHP;
    public float currentHP;
    public float healthPercent;
    public bool isInfected = false;
    GameObject healthBar;

    GameObject dungeon;
    RoomGeneration generator;
    public GameObject lifeEmberSpawn;
    GameObject player;
   public  GameObject explosion;
   public  GameObject lightRemains;
    PlayerEquipment equipment;
    public GameObject LoseText;
   public bool playerDead;
    Animator anim;
    Health playerHealth;
    float hitTimer;
    public GameObject corpse;
    public GameObject BossDeathParticles;

    Options theoptions;

    public AudioClip loseSound;
    public AudioClip GetHit;
    public AudioSource PutSourceHere;


    void Start()
    {
        theoptions = GameObject.Find("TheOptions").GetComponent<Options>();
        hitTimer = 0;
        playerDead = false;
        player = GameObject.FindGameObjectWithTag("Player");
        anim = gameObject.GetComponent<Animator>();
        playerHealth = player.GetComponent<Health>();
        equipment = player.GetComponent<PlayerEquipment>();
        if (this.tag == "Player")
        {
            healthBar = GameObject.FindGameObjectWithTag("HealthBar");
        }
        healthPercent = currentHP / maxHP;

        dungeon = GameObject.FindGameObjectWithTag("Dungeon");
        if (dungeon != null)
        {
            generator = dungeon.GetComponent<RoomGeneration>();
        }


    }

    void Update()
    {
        hitTimer -= Time.deltaTime;

    }
    public void GainHealth(float Amount)
    {
        if (equipment.paused == false)
        {
            currentHP += Amount;
            if (currentHP > maxHP)
                currentHP = maxHP;

            healthPercent = currentHP / maxHP;
            if (this.tag == "Player")
            {
                healthBar.transform.localScale = new Vector3(healthPercent, 1, 1);
           
            }
        }
    }

    public void LoseHealth(float Amount)
    {
        if (equipment.paused == false)
        {
            if(GetHit != null && PutSourceHere != null && hitTimer < 0)
            {
                PutSourceHere.PlayOneShot(GetHit);
                hitTimer = .5f;
            }
            currentHP -= Amount;
            if (currentHP <= 0)
            {
                currentHP = 0;
                playerDead = true;
                Die();
            }
            healthPercent = currentHP / maxHP;
            if (this.tag == "Player")
            {

                healthBar.transform.localScale = new Vector3(healthPercent, 1, 1);
                anim.CrossFade("TakingDamage", 0.01f);

            }
            else if (equipment.equippedEmber == ember.Life)
            {
                GameObject instance = (GameObject)Instantiate(lifeEmberSpawn, transform.position, transform.rotation);
                playerHealth.GainHealth(1);

            }
			else
			{
                   if(anim!=null)
				{
			  if (this.GetComponent<AISkeletonArcher>()!=null)
					anim.CrossFade("SkeletonATakingDamage",0.01f);
				else if (this.GetComponent<AIShadowCloud>()!=null)
					anim.CrossFade("CloudTakingDamage",0.01f);
				else if (this.GetComponent<AICommander>()!=null)
					anim.CrossFade("CommanderTakingDamage",0.01f);
				}
		 

				
				

			}
        }
    }

    void Die()
    {
        healthPercent = currentHP / maxHP;
        if (this.name == "Dethros(Clone)" || this.name == "Dethros" || this.name == "Lorne(Clone)" || this.name == "Lorne" || this.name == "Morrius(Clone)" || this.name == "Morrius")
        {
            Instantiate(BossDeathParticles, this.transform.position, new Quaternion(0, 0, 0, 0));
            this.SendMessage("DestroyHealthBar", SendMessageOptions.DontRequireReceiver);

            if (this.name == "Morrius(Clone)" || this.name == "Morrius")
            {
                theoptions.MorriusDie();
                if (playerHealth.currentHP >= playerHealth.maxHP)
                {
                    theoptions.FullHp();
                }
            }
            else if (this.name == "Lorne(Clone)" || this.name == "Lorne")
            {
                theoptions.LorneDie();
                if (playerHealth.currentHP >= playerHealth.maxHP)
                {
                    theoptions.FullHp();
                }
            }
            else if (this.name == "Dethros(Clone)" || this.name == "Dethros")
            {
                theoptions.DethrosDie();
                if (playerHealth.currentHP >= playerHealth.maxHP)
                {
                    theoptions.FullHp();
                }
            }

        }


        if (this.tag != "Player")
        {
            theoptions.AddToEnemy();
			if(this.GetComponent<AICommander>()!=null)
			{
				this.GetComponent<AICommander>().commandercount-=1;
			//	this.GetComponent<AICommander>().UnReinforcing();
			}
            Instantiate(lightRemains, transform.position, transform.rotation);
            Instantiate(corpse, new Vector3(transform.position.x, transform.position.y, -0.5f), Quaternion.identity);
            gameObject.GetComponent<GenerateLoot>().Generateloot();
            if (isInfected)
            {
                Explode();
            }

			if(GetComponent<Reinforced>()!=null)
				Destroy(GetComponent<Reinforced>().temp);
            Destroy(this.gameObject);
            if(generator != null)
            --generator.finalRoomInfoArray[generator.currentRoom].numEnemies;
        }
        else
        {
            if (playerDead)
            {
                GameObject.FindObjectOfType<Options>().GetComponent<Options>().DieAchv();
                anim.CrossFade("Dying", 0.01f);
                GameObject.FindObjectOfType<BGM>().SendMessage("SetToMenu", SendMessageOptions.DontRequireReceiver);
               // GameObject.FindObjectOfType<BGM>().audioPlayer.Stop();
				GameObject.FindObjectOfType<Options>().GetComponent<SaveTest>().YOUDIED();
                player.GetComponent<AudioSource>().PlayOneShot(loseSound);
                equipment.paused = true;
                playerDead = true;
                Instantiate(LoseText);
            }
        }
       
      

    }
    void GetInfected()
    {
        isInfected = true;
    }
    void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);  
    }
}
