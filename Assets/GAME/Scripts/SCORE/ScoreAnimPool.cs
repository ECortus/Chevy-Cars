using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAnimPool : MonoBehaviour
{
    public static ScoreAnimPool Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }
    
    [SerializeField] private ScoreAnim prefab;

    private List<ScoreAnim> Pool = new List<ScoreAnim>();
    
    public void Insert(Vector3 pos, int score)
    {
        ScoreAnim anim = null;
        
        if (Pool.Count > 0)
        {
            for(int i = 0; i < Pool.Count; i++)
            {
                if (!Pool[i].Active)
                {
                    anim = Pool[i];
                    break;
                }
            }
        }

        if (!anim)
        {
            anim = Instantiate(prefab);
            Pool.Add(anim);
        }
        
        anim.On(pos, score);
    }
}
