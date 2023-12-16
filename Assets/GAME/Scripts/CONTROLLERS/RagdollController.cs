using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;

public class RagdollController : MonoBehaviour
{
    private GameObject objParent;
    [SerializeField] private float rbMasses = 5;

    [Space] 
    [SerializeField] private PhysicMaterial standardPhysicMaterial;
    
    [Space]
    [SerializeField] private Material[] defaultMaterial;
    [SerializeField] private Material[] blackMaterial;
    
    [Header("DEBUG (click 'Write default'):")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject[] rbObjects;
    [SerializeField] private MeshRenderer[] meshes;
    [SerializeField] private Rigidbody[] rbs;
    [SerializeField] private Collider[] cols;
    [SerializeField] private Transform[] DefaultParents;
    [SerializeField] private Vector3[] DefaultPoses;
    [SerializeField] private Quaternion[] DefaultRotation;

    // write manual on editor
    [ContextMenu("Write default")]
    public void WriteDefault()
    {
        objParent = transform.GetChild(0).gameObject;
        rb = GetComponent<Rigidbody>();

        meshes = objParent.GetComponentsInChildren<MeshRenderer>();
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
        
        DefaultParents = new Transform[rbObjects.Length];
        DefaultPoses = new Vector3[rbObjects.Length];
        DefaultRotation = new Quaternion[rbObjects.Length];
        
        for(int i = 0; i < rbObjects.Length; i++)
        {
            if(rbs[i]) SetRb(rbs[i], false);
            cols[i].material = standardPhysicMaterial;

            DefaultParents[i] = rbObjects[i].transform.parent;
            DefaultPoses[i] = rbObjects[i].transform.localPosition;
            DefaultRotation[i] = rbObjects[i].transform.localRotation;
        }
        
        SetDefaultMaterials();
    }

    public void SetDefault()
    {
        SetMain(true);
        Rigidbody orb;

        SetDefaultMaterials();
        
        for(int i = 0; i < rbObjects.Length; i++)
        {
            if(rbs[i]) SetRb(rbs[i], false);
            
            cols[i].material = standardPhysicMaterial;
            
            if(rbObjects[i].TryGetComponent(out orb)) Destroy(orb);
            
            rbObjects[i].transform.SetParent(DefaultParents[i]);
            rbObjects[i].transform.localPosition = DefaultPoses[i];
            rbObjects[i].transform.localRotation = DefaultRotation[i];
        }
    }

    void SetDefaultMaterials() => SetMaterials(defaultMaterial);
    void SetBlackMaterials() => SetMaterials(blackMaterial);

    void SetMaterials(Material[] mat)
    {
        foreach (var VARIABLE in meshes)
        {
            VARIABLE.materials = mat;
        }
    }

    void SetRBArray()
    {
        for (int i = 0; i < rbObjects.Length; i++)
        {
            if(!rbObjects[i].TryGetComponent(out rbs[i])) rbs[i] = rbObjects[i].AddComponent<Rigidbody>();
            
            rbs[i].mass = rbMasses;
            SetRb(rbs[i], false);
        }
    }

    public void ForceFromDot(Vector3 center, float force)
    {
        Vector3 dir;
        SetMain(false);
        
        SetRBArray();
        SetBlackMaterials();
        
        for (int i = 0; i < rbs.Length; i++)
        {
            cols[i].material = null;
            
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
        SetBlackMaterials();
        
        for (int i = 0; i < rbs.Length; i++)
        {
            cols[i].material = null;
            
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
        SetBlackMaterials();
        
        for (int i = 0; i < rbs.Length; i++)
        {
            cols[i].material = null;
            ForceRb(rbs[i], dir, force);
        }
    }

    void SetRb(Rigidbody rbc, bool state)
    {
        rbc.isKinematic = !state;
        rbc.mass = rbMasses;
        rbc.useGravity = state;
        rbc.constraints = RigidbodyConstraints.None;
    }

    void SetMain(bool state)
    {
        if (rb) SetRb(rb, state);
        // if (col) col.enabled = state;
    }

    void ForceRb(Rigidbody rbc, Vector3 dir, float force)
    {
        SetRb(rbc, true);
        rbc.transform.SetParent(null);
        
        rbc.AddForce(dir * force, ForceMode.Force);
        rbc.angularVelocity = dir * Random.Range(5f, 15f);
    }
}
