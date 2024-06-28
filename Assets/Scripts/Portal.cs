using System.Collections;
using UnityEngine;

[SelectionBase]
public class Portal : MonoBehaviour
{
    [SerializeField] TriggerDelegator _portalEnterDelegator;
    [SerializeField] Transform _portalExit;
    [SerializeField] SoundData _enterSound;

    public SpriteRenderer EnterPortal { get; private set; }
    public SpriteRenderer ExitPortal { get; private set; }

    private void Awake()
    {
        EnterPortal = _portalEnterDelegator.GetComponent<SpriteRenderer>();
        ExitPortal = _portalExit.gameObject.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        PortalsUndoHandler.Instance.AddPortal(this);
    }

    private void OnDestroy()
    {
        if(PortalsUndoHandler.Instance != null)
            PortalsUndoHandler.Instance.RemovePortal(this);
    }

    void OnEnable()
    {
        _portalEnterDelegator.OnTriggerEnter += OnCollEnter;
    }

    void OnDisable()
    {
        _portalEnterDelegator.OnTriggerEnter -= OnCollEnter;
    }
    
    void OnCollEnter(Collider2D obj)
    {
        if(obj.TryGetComponent<PlayerMovement>(out var pm))
        {
            GlobalSounds.Instance.PlaySound(_enterSound);
            var particles = pm.GetComponent<ParticleSystem>();
            particles.Play();
            if (pm == null) return;
            pm.transform.position = _portalExit.position;
            pm.Break = true;
            Destroy(gameObject);
        }
    }

    IEnumerator WaitTillIdleOrFall(PlayerMovement pm)
    {
        //yield return new WaitUntil(()=>  pm.CurrentState is Idle || pm.CurrentState is Fall);
        
        yield return null;
    }

}
