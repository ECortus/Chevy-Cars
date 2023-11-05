using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;

public class RagdollController : MonoBehaviour
{
    [SerializeField] private GameObject objParent;
    [SerializeField] private float rbMasses = 5;
    
    [Header("DEBUG (click 'Write default'):")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject[] rbObjects;
    [SerializeField] private Rigidbody[] rbs;
    [SerializeField] private Collider[] cols;
    [SerializeField] private Vector3[] DefaultPoses;
    [SerializeField] private Quaternion[] DefaultRotation;

    // write manual on editor
    [ContextMenu("Write default")]
    public void WriteDefault()
    {
        rb = GetComponent<Rigidbody>();

        cols = objParent.GetComponentsInChildren<Collider>();
        rbObjects = new GameObject[cols.Length];
        
        for(int i = 0; i < rbObjects.Length; i++)
        {
            rbObjects[i] = cols[i].gameObject;
            
        }

        Rigidbody orb;

        for(int i = 0; i < rbObjects.Length; i++)
        {
            if(rbObjects[i].TryGetComponent(out orb)) DestroyImmediate(orb, true);
        }

        rbs = new Rigidbody[rbObjects.Length];
        
        DefaultPoses = new Vector3[rbObjects.Length];
        DefaultRotation = new Quaternion[rbObjects.Length];
        
        for(int i = 0; i < rbObjects.Length; i++)
        {
            // SetRB(rbs[i], false);
            cols[i].enabled = false;
            
            DefaultPoses[i] = rbObjects[i].transform.localPosition;
            DefaultRotation[i] = rbObjects[i].transform.localRotation;
        }
    }

    public async UniTask SetDefault()
    {
        SetMain(true);
        Rigidbody orb;
        
        for(int i = 0; i < rbObjects.Length; i++)
        {
            // SetRB(rbs[i], false);
            // cols[i].enabled = true;
            
            cols[i].enabled = false;
            
            if(rbObjects[i].TryGetComponent(out orb)) Destroy(orb);
            
            rbObjects[i].transform.localPosition = DefaultPoses[i];
            rbObjects[i].transform.localRotation = DefaultRotation[i];
        }

        await UniTask.Delay(100);
    }

    void SetRBArray()
    {
        for (int i = 0; i < rbObjects.Length; i++)
        {
            rbs[i] = rbObjects[i].AddComponent<Rigidbody>();
            rbs[i].mass = rbMasses;
            SetRB(rbs[i], false);
        }
    }

    public void ForceFromDot(Vector3 center, float force)
    {
        Vector3 dir;
        SetMain(false);
        
        SetRBArray();
        
        for (int i = 0; i < rbs.Length; i++)
        {
            cols[i].enabled = true;
            
            dir = (center - rbs[i].transform.position).normalized;
            dir.y = Random.Range(0.5f, 1f);
            
            ForceRb(rbs[i], dir, force);
        }
    }

    public void ForceRandom(float force)
    {
        Vector3 dir;
        float divive = 2f;
        
        SetMain(false);
        
        SetRBArray();
        
        for (int i = 0; i < rbs.Length; i++)
        {
            cols[i].enabled = true;
            
            dir = new Vector3(
                Random.Range(-1f, 1f) / divive,
                Random.Range(0.25f, 1f) / divive,
                Random.Range(-1f, 1f) / divive
                );
            
            ForceRb(rbs[i], dir, force);
        }
    }

    public void ForceRigidbodies(Vector3 dir, float force)
    {
        SetMain(false);
        
        SetRBArray();
        for (int i = 0; i < rbs.Length; i++)
        {
            cols[i].enabled = true;
            ForceRb(rbs[i], dir, force);
        }
    }

    void SetRB(Rigidbody rbc, bool state)
    {
        rbc.isKinematic = !state;
        rbc.detectCollisions = state;
        rbc.useGravity = state;
        rbc.constraints = RigidbodyConstraints.None;
    }

    void SetMain(bool state)
    {
        if (rb) SetRB(rb, state);
        // if (col) col.enabled = state;
    }

    void ForceRb(Rigidbody rbc, Vector3 dir, float force)
    {
        SetRB(rbc, true);
        rbc.AddForce(dir * force, ForceMode.Force);
    }
}
