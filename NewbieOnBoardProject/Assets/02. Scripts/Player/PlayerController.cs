using Manager.UI;
using UnityEngine;

namespace Player.PlayerController
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Animator _animator;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        void Update()
        {
            ToggleInventory();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void ToggleInventory()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                UIManager.Instance.ToggleInventoryUI();
            }
        }

        private void Move()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            Vector2 direction = new Vector2(x, y);

            _animator.SetFloat("X", x);
            _animator.SetFloat("Y", y);

            _rigidbody.MovePosition((Vector2)transform.position + (direction * _speed * Time.fixedDeltaTime));
        }

    }
}
