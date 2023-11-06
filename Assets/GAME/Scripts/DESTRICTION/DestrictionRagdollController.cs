using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.Events;

public class DestrictionRagdollController : MonoBehaviour
{
    [SerializeField] private GameObject objParent;
    [SerializeField] private float rbMasses = 1;

    [Space] 
    [SerializeField] private GameObject additionalObjectToOn;
    [SerializeField] private GameObject additionalObjectToOff;
    
    [Space]
    [SerializeField] private UnityEvent additionalActionOnOn;
    [SerializeField] private UnityEvent additionalActionOnOff;
    void AdditionalOnAction() => additionalActionOnOn?.Invoke();
    void AdditionalOffAction() => additionalActionOnOn?.Invoke();
    
    [Header("DEBUG (click 'Write default'):")]
    [SerializeField] private Rigidbody[] rbs;
    [SerializeField] private Vector3[] DefaultPoses;
    [SerializeField] private Quaternion[] DefaultRotation;

    // write manual on editor
    [ContextMenu("Write default")]
    public void WriteDefault()
    {
        rbs = objParent.GetComponentsInChildren<Rigidbody>();
        
        DefaultPoses = new Vector3[rbs.Length];
        DefaultRotation = new Quaternion[rbs.Length];
        
        AdditionalObjectsState(false);
        AdditionalOffAction();
        
        for(int i = 0; i < rbs.Length; i++)
        {
            SetRb(rbs[i], false);
            
            DefaultPoses[i] = rbs[i].transform.localPosition;
            DefaultRotation[i] = rbs[i].transform.localRotation;
        }
    }

    public void SetDefault()
    {
        AdditionalObjectsState(false);
        AdditionalOffAction();
        
        for(int i = 0; i < rbs.Length; i++)
        {
            SetRb(rbs[i], false);
            
            rbs[i].transform.localPosition = DefaultPoses[i];
            rbs[i].transform.localRotation = DefaultRotation[i];
        }
    }

    public void ForceFromDot(Vector3 center, float force)
    {
        Vector3 dir;
        
        AdditionalObjectsState(true);
        AdditionalOnAction();
        
        for (int i = 0; i < rbs.Length; i++)
        {
            SetRb(rbs[i], true);
            
            dir = (center - rbs[i].transform.position).normalized;
            dir.y = Random.Range(0.5f, 1f);

            dir.x += Random.Range(-1f, 1f) / 4f;
            dir.z += Random.Range(-1f, 1f) / 4f;
            
            ForceRb(rbs[i], dir, force);
        }
    }

    public void ForceRandom(float force)
    {
        Vector3 dir;
        float divive = 2f;
        
        AdditionalObjectsState(true);
        AdditionalOnAction();
        
        for (int i = 0; i < rbs.Length; i++)
        {
            SetRb(rbs[i], true);
            
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
        AdditionalObjectsState(true);
        AdditionalOnAction();
        
        for (int i = 0; i < rbs.Length; i++)
        {
            SetRb(rbs[i], true);
            ForceRb(rbs[i], dir, force);
        }
    }

    void SetRb(Rigidbody rbc, bool state)
    {
        rbc.isKinematic = !state;
        rbc.detectCollisions = state;
        rbc.useGravity = state;
        rbc.constraints = RigidbodyConstraints.None;
    }

    void AdditionalObjectsState(bool state)
    {
        if(additionalObjectToOn) additionalObjectToOn.SetActive(state);
        if(additionalObjectToOff) additionalObjectToOff.SetActive(!state);
    }

    void ForceRb(Rigidbody rbc, Vector3 dir, float force)
    {
        SetRb(rbc, true);
        rbc.AddForce(dir * force, ForceMode.Force);
    }
}
