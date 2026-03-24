using UnityEngine;

public class BoxLevelManager : MonoBehaviour
{
    public static BoxLevelManager current;
    public int levelNum=0;

    void Awake()
    {
        current = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
