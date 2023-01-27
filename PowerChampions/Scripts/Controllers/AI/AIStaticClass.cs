using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStaticClass : MonoBehaviour
{
    #region Singleton 

    public static AIStaticClass instance;

    public List<Opponent> opponents;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            opponents = new List<Opponent>();
        }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }
}
