using UnityEngine;

public class DoorLogic : MonoBehaviour
{
    [SerializeField] private GameObject _button;
    private Rigidbody _rig;
    private ButtonLogic _buttonLogic;

    private void Awake()
    {
        _rig = gameObject.GetComponentInChildren<Rigidbody>();
        _buttonLogic = _button.GetComponentInChildren<ButtonLogic>();
    }

    private void Start()
    {
        _buttonLogic.ButtonHit += ReleaseDoor;
    }

    private void OnDestroy()
    {
        _buttonLogic.ButtonHit -= ReleaseDoor;
    }

    public void ReleaseDoor()
    {
        _rig.isKinematic = false;
    }
}
