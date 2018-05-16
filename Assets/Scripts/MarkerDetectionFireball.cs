using UnityEngine;
using UnityEngine.UI;
using Vuforia;


public class MarkerDetectionFireball : MonoBehaviour,	ITrackableEventHandler {
		
	private TrackableBehaviour mTrackableBehaviour;
	bool markerFound = false;

    public GameObject vuforia_main_target;
    public GameObject player_base;
    public GameObject ai_base;

    public GameObject particle_system;
    public Text cd_text;
    public UnityEngine.UI.Image cd_front_img;
    public float cooldown = 30.0f;
    private float cooldown_counter = 30.0f;

    void Start()
	{
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}

        cooldown_counter = cooldown;
        particle_system.SetActive(false);
    }

    void Update()
    {
        cooldown_counter += Time.deltaTime;
        int cd_time = (int)(cooldown - cooldown_counter);
        if (cd_time < 0)
            cd_time = 0;
        cd_text.text = (cd_time).ToString();
        cd_front_img.fillAmount = (1.0f - (cooldown_counter / cooldown));

        if (markerFound && cooldown_counter >= cooldown)
        {
            cooldown_counter = 0.0f;
            particle_system.SetActive(true);

            foreach (GameObject soldier in player_base.GetComponent<BaseManager>().soldiers)
                soldier.GetComponent<SoldiersManager>().ApplyDamage(215.0f, false);

            foreach (GameObject soldier in ai_base.GetComponent<BaseManager>().soldiers)
                soldier.GetComponent<SoldiersManager>().ApplyDamage(215.0f, false);
        }

        if (particle_system.activeSelf && cooldown_counter >= 2.0f)
            particle_system.SetActive(false);
    }


    public void OnTrackableStateChanged( TrackableBehaviour.Status previousStatus,
										 TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			OnTrackingFound();
		}
		else
		{
			OnTrackingLost();
		}
	}


	private void OnTrackingFound()
	{
        markerFound = true;
        if (cooldown_counter >= cooldown)
        {
            cooldown_counter = 0.0f;
            particle_system.SetActive(true);

            foreach (GameObject soldier in player_base.GetComponent<BaseManager>().soldiers)
                soldier.GetComponent<SoldiersManager>().ApplyDamage(215.0f, false);

            foreach (GameObject soldier in ai_base.GetComponent<BaseManager>().soldiers)
                soldier.GetComponent<SoldiersManager>().ApplyDamage(215.0f, false);

            //particle_system.transform.position.Set(transform.position.x, 0.0f, transform.position.z);
            //particle_system.transform.position.Set(0.0f, 0.0f, 0.0f);
        }
	}


	private void OnTrackingLost()
	{
        markerFound = false;
	}

	public bool markerDetected()
	{
		return markerFound;
	}
}
