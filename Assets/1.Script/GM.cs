using UnityEngine;

public class GM : MonoBehaviour
{
    public static GM I;
    public int gold;
    [HideInInspector] public CSVReadeer csvReadeer;
    [HideInInspector] public Player player;
    public ItemSlotPopUp itemSlotPopUp;
    public int refillMoneyInterval;
    public float refillMoneyCount;
    public int defaultMoneyCount;
    public int requireGachaPrice;
    public int maxMoneyLimit;
    private void Awake()
    {
        if (I == null)
        {
            I = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        csvReadeer = GetComponent<CSVReadeer>();
        player = GetComponent<Player>();
    }
}
