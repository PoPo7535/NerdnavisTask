using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager I;
    public int gold;
    public CSVReadeer csvReadeer;

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
    }

    // Update is called once per frame
    void Update()
    {
    }
}
