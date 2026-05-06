using UnityEngine;

public class Object_CheckPoint : MonoBehaviour, ISaveable
{
    private Player player;
    private Object_CheckPoint[] allCheckpoints;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        allCheckpoints = FindObjectsByType<Object_CheckPoint>(FindObjectsSortMode.None);
    }

    public void ActivateCheckPoint(bool activate)
    {
        anim.SetBool("isActive", activate);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var checkpoint in allCheckpoints)
        {
            checkpoint.ActivateCheckPoint(false);
        }

        SaveManager.instance.GetGameData().savedCheckpoint = transform.position;
        ActivateCheckPoint(true);
    }

    public void LoadData(GameData data)
    {
        bool active = data.savedCheckpoint == transform.position;
        ActivateCheckPoint(active);

        if (active)
        {
            Player.instance.TeleportPlayer(transform.position);
        }
    }

    public void SaveData(ref GameData data)
    {

    }
}
