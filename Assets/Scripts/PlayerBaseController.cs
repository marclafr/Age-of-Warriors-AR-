using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBaseController : MonoBehaviour
{
    public enum SOLDIER_TYPE
    {
        S_NONE = 0,
        S_MELEE,
        S_RANGED,
        S_CAVALRY
    }

    public GameObject melee_soldier;
	public GameObject ranged_soldier;
    public GameObject cavalry_soldier;

    public Text gold_txt;

    public float training_delay = 1.0f;
    private float training_delay_timer = 0.0f;
    private bool training = false;
    private SOLDIER_TYPE training_type;

    [Header("Costs")]
    public int gold = 200;
    public int melee_soldier_cost = 25;
    public int ranged_soldier_cost = 75;
    public int cavalry_soldier_cost = 125;

    void Start ()
    {
        training = false;
        training_delay_timer = 0.0f;
        training_type = SOLDIER_TYPE.S_NONE;
        UpdateGoldText();
    }
	
	void Update ()
    {
		if (training)
        {
            training_delay_timer += Time.deltaTime;
            if(training_delay_timer >= training_delay)
            {
                training = false;
                training_delay_timer = 0.0f;
                switch (training_type)
                {
                    case SOLDIER_TYPE.S_NONE:
                        break;
                    case SOLDIER_TYPE.S_MELEE:
                        GameObject copy_melee = Instantiate(melee_soldier, null);
                        copy_melee.GetComponent<SoldiersManager>().enabled = true;
                        copy_melee.transform.position = gameObject.transform.position;
                        GetComponent<BaseManager>().SetStates(copy_melee, training_type);
                        break;
                    case SOLDIER_TYPE.S_RANGED:
						GameObject copy_ranged = Instantiate(ranged_soldier, null);
                        copy_ranged.GetComponent<SoldiersManager>().enabled = true;
                        copy_ranged.transform.position = gameObject.transform.position;
                        GetComponent<BaseManager>().SetStates(copy_ranged, training_type);
                        break;
                    case SOLDIER_TYPE.S_CAVALRY:
						GameObject copy_cavalry = Instantiate(cavalry_soldier, null);
                        copy_cavalry.GetComponent<SoldiersManager>().enabled = true;
                        copy_cavalry.transform.position = gameObject.transform.position;
                        GetComponent<BaseManager>().SetStates(copy_cavalry, training_type);
                        break;
                    default:
                        break;
                }
                
            }
        }
	}

    public void CreateSoldier(int type)
    {
        if (!training)
        {
            switch ((SOLDIER_TYPE)type)
            {
                case SOLDIER_TYPE.S_NONE:
                    break;
                case SOLDIER_TYPE.S_MELEE:
                    if (gold >= melee_soldier_cost)
                    {
                        training = true;
                        training_type = SOLDIER_TYPE.S_MELEE;
                        gold -= melee_soldier_cost;
                        UpdateGoldText();
                    }
                    break;
                case SOLDIER_TYPE.S_RANGED:
                    if (gold >= ranged_soldier_cost)
                    {
                        training = true;
                        training_type = SOLDIER_TYPE.S_RANGED;
                        gold -= ranged_soldier_cost;
                        UpdateGoldText();
                    }
                    break;
                case SOLDIER_TYPE.S_CAVALRY:
                    if (gold >= cavalry_soldier_cost)
                    {
                        training = true;
                        training_type = SOLDIER_TYPE.S_CAVALRY;
                        gold -= cavalry_soldier_cost;
                        UpdateGoldText();
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateGoldText();
    }

    private void UpdateGoldText()
    {
        gold_txt.text = gold.ToString();
    }
}
