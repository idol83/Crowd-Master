using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class EnemyStateMachine : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyState _firstState;
    [SerializeField] private BrokenState _brokenState;
    [SerializeField] private HealthContainer _healthContainer;
    private EnemyState _currentState;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private float _minDamage;
    public event UnityAction<EnemyStateMachine> Died;
    public PlayerStateMachine Player { get; private set; }
    private void OnEnable()
    {
        _healthContainer.Died += OnEnemyDied;
    }
    private void OnDisable()
    {
        _healthContainer.Died -= OnEnemyDied;
    }
    private void OnEnemyDied()
    {
        enabled = false;
        _rigidbody.constraints = RigidbodyConstraints.None;
        Died?.Invoke(this);
    }
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        Player = FindObjectOfType<PlayerStateMachine>();
    }
    private void Start()
    {
        _currentState = _firstState;
        _currentState.Enter(_rigidbody, _animator, Player);
    }
    private void Update()
    {
        if (_currentState == null) return;
        EnemyState nextState = _currentState.GetNextState();
        if (nextState != null)
        {
            Transit(nextState);
        }
    }
    private void Transit(EnemyState nextState)
    {
        if (_currentState != null) _currentState.Exit();
        _currentState = nextState;
        if (_currentState != null)
        {
            _currentState.Enter(_rigidbody, _animator, Player);
        }
    }

    public bool ApplyDamage(Rigidbody rigidbody, float damage)
    {
        if (damage > _minDamage && _currentState != _brokenState)
        {
            _healthContainer.TakeDamage((int)damage);
            Transit(_brokenState);
            _brokenState.ApplyDamage(rigidbody, damage);
            return true;
        }
        return false;
    }
}
