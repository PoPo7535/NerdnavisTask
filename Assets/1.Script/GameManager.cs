using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager I;
    public int gold;
    public CSVReadeer csvReadeer;
    public Player player;
    
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
