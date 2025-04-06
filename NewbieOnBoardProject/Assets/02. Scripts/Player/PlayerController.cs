using UnityEngine;
using Manager.UI;
using Manager.Inventory;

namespace Player.PlayerController
{
    public class PlayerController : MonoBehaviour
    {
        
        private enum NpcType
        {
            None,
            Auction,
            Stock,
            Exchange
        }

        [SerializeField] private GameObject _inventoryUI;
        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Animator _animator;
        [SerializeField] private NpcType _nearNpc = NpcType.None;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        void Update()
        {
            ToggleInventory();
            ContactNpc();
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

        private void ContactNpc() //f키 입력 감지
        {
            if((Input.GetKeyDown(KeyCode.F) || (Input.GetMouseButtonDown(1))) && _nearNpc != NpcType.None) //기획팀 의견 듣고 임시로 우클릭 기능도 만들었음
            {
                CheckNpcType(_nearNpc);
                UIManager.Instance.ToggleAuctionUI(); //매니저 임시로 인벤토리에 함수 만들었음 나중에 경매장 매니저로 옮기기
            }
            else if(_nearNpc == NpcType.None)
            {
                UIManager.Instance.DisableAuctionUI();
                //Debug.Log("[INFO] Shop UI deactivate");
                //TODO: UI 비활성화. (UI를 직접 끄지 않더라도 이동해서 멀어진 경우 자동으로 꺼지도록 하기 위함.)
            }
        }

        private void CheckNpcType(NpcType _nearNpc) //충돌 정보 넘겨받고 어떤 npc인지 식별
        {
            switch (_nearNpc)
            {
                case NpcType.Auction:
                    //TODO: 경매 UI 활성화
                    Debug.Log("[INFO] Aurcion UI activation");
                    break;
                default:
                    Debug.LogWarning("[WARNING] NpcType is undefined");
                    break;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision) //충돌 감지 후 가까운 npc 정보 저장
        {
            switch (collision.tag)
            {
                case "AuctionShop":
                    _nearNpc = NpcType.Auction;
                    Debug.Log("[INFO] Enter Shop. nearNpc: " + _nearNpc);
                    break;
                default:
                    //TODO: 예외상황 생기면 처리
                    break;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _nearNpc = NpcType.None;
            Debug.Log("[INFO] Exit Shop. nearNpc: " + _nearNpc);
        }
    }
}
