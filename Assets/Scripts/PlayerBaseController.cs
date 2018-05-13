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
    public GameObject update_btn;
    public Image max_update_img;
    public float gold_gain_speed = 20.0f;
    public int gold_update_cost = 50;
    public int current_gold_update = 1;
    public int max_gold_updates = 5;
    public float gold_update_multiplier = 1.2f;
    public float gold_update_cost_multiplier = 2.0f;

    public float training_delay = 1.0f;
    private float training_delay_timer = 0.0f;
    private bool training = false;
    private SOLDIER_TYPE training_type;

    [Header("Costs")]
    public float gold = 200;
    public int melee_soldier_cost = 25;
    public int ranged_soldier_cost = 75;
    public int cavalry_soldier_cost = 125;

    void Start()
    {
        training = false;
        training_delay_timer = 0.0f;
        training_type = SOLDIER_TYPE.S_NONE;
        UpdateGoldText();
    }

    void Update()
    {
        gold += (Time.deltaTime * gold_gain_speed);
        UpdateGoldText();

        if (training)
        {
            training_delay_timer += Time.deltaTime;
            if (training_delay_timer >= training_delay)
            {
                training = false;
                training_delay_timer = 0.0f;
                switch (training_type)
                {
                    case SOLDIER_TYPE.S_NONE:
                        break;
                    case SOLDIER_TYPE.S_MELEE:
                        GameObject copy_melee = Instantiate(melee_soldier, null);
						copy_melee.SetActive(true);
						copy_melee.GetComponent<SoldiersManager>().enabled = true;
                        copy_melee.transform.position = gameObject.transform.position;
						copy_melee.transform.position.Set (copy_melee.transform.position.x, 0.0f, copy_melee.transform.position.z);
						GetComponent<BaseManager>().SetStates(copy_melee, training_type);
                        break;
                    case SOLDIER_TYPE.S_RANGED:
                        GameObject copy_ranged = Instantiate(ranged_soldier, null);
						copy_ranged.SetActive(true);
						copy_ranged.GetComponent<SoldiersManager>().enabled = true;
                        copy_ranged.transform.position = gameObject.transform.position;
						copy_ranged.transform.position.Set (copy_ranged.transform.position.x, 0.0f, copy_ranged.transform.position.z);
						GetComponent<BaseManager>().SetStates(copy_ranged, training_type);
                        break;
                    case SOLDIER_TYPE.S_CAVALRY:
                        GameObject copy_cavalry = Instantiate(cavalry_soldier, null);
						copy_cavalry.SetActive(true);
						copy_cavalry.GetComponent<SoldiersManager>().enabled = true;
                        copy_cavalry.transform.position = gameObject.transform.position;
						copy_cavalry.transform.position.Set (copy_cavalry.transform.position.x, 0.0f, copy_cavalry.transform.position.z);
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
        gold_txt.text = "Gold: " + ((int)gold).ToString();
    }

    public void IncrementGoldSpeed()
    {
        if (gold > gold_update_cost)
        {
            if (current_gold_update <= max_gold_updates)
            {
                current_gold_update++;
                gold -= gold_update_cost;
                gold_update_cost = (int)(gold_update_cost * gold_update_cost_multiplier);
                gold_gain_speed *= gold_update_multiplier;
            }

            if (current_gold_update > max_gold_updates)
            {
                update_btn.SetActive(false);
                max_update_img.enabled = true;
            }
        }
    }
}