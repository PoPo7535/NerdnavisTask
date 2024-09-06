using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager I;
    public int gold;
    public CSVReadeer csvReadeer;
    public Player player;

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

    // Update is called once per frame
    void Update()
    {
    }
}
