namespace io.lockedroom.Games.SuperMario
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.U2D;
    using TMPro;
    using System.Runtime.InteropServices.ComTypes;
    using UnityEngine.SceneManagement;

    public class Mario : MonoBehaviour
    {
        public static Mario Instance;
        /// <summary>
        /// Tốc độ của player theo phương X
        /// </summary>
        public float SpeedX;
        /// <summary>
        /// Tốc độ nhảy
        /// </summary>
        public float JumpSpeed;
        /// <summary>
        /// Đồng xu quay
        /// </summary>
        public GameObject RotatedCoin;
        /// <summary>
        /// Hiện điểm
        /// </summary>
        public TextMeshProUGUI PoinText;
        /// <summary>
        /// Hiện dòng text "Game over"
        /// </summary>
        public GameObject LoseText;
        /// <summary>
        /// Vật thể cờ
        /// </summary>
        public GameObject Flag;
        /// <summary>
        /// Kiểm tra xem player có ở dưới đất không
        /// </summary>
        private bool IsGrounded;
        private Animator Player;
        /// <summary>
        /// gán biến cho rigidbody
        /// </summary>
        private Rigidbody2D rb;
        /// <summary>
        /// điểm khi ăn xu
        /// </summary>
        public int points;
        /// <summary>
        /// đếm số lần va chạm
        /// </summary>
        private int CollisionCount;
        /// <summary>
        /// số lần va chạm tối đa
        /// </summary>
        private int MaxCollision = 3;
        /// <summary>
        /// Cổng
        /// </summary>
        public GameObject Gate;
        public int Level;
        /// <summary>
        /// mario biến hình
        /// </summary>
        public bool Transform = false;
        // Start is called before the first frame update
        private void Awake() {
            Instance = this;    
        }
        void Start()
        {
            Player = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            Player.SetBool("IsRun", false);
            Player.SetBool("IsStand", true);
            RotatedCoin.SetActive(false);
            PoinText.text = "Point: " + points.ToString();
            LoseText.SetActive(false);
            Gate.SetActive(false);
        }
       
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.D))
            {
                Player.SetBool("IsRun", true);
                Player.SetBool("IsStand", false);
                gameObject.transform.Translate(Vector2.right * SpeedX * Time.deltaTime);
                if (gameObject.transform.localScale.x < 0)
                {
                    gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                }
            }
            else if (Input.GetKey(KeyCode.A))
            {
                Player.SetBool("IsRun", true);
                Player.SetBool("IsStand", false);
                gameObject.transform.Translate(Vector2.left * SpeedX * Time.deltaTime);
                if (gameObject.transform.localScale.x > 0)
                {
                    gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                }
            }
            else
            {
                Player.SetBool("IsRun", false);
                Player.SetBool("IsStand", true);
            }
            if (IsGrounded)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    Player.SetBool("IsRun", false);
                    Player.SetBool("IsStand", true);
                    rb.AddForce(Vector2.up * JumpSpeed, ForceMode2D.Impulse);
                    IsGrounded = false;
                }
            }
            if (Transform == true)
            {
                switch (Level)
                {
                    case 0:
                        {
                            StartCoroutine(TinyMario());
                            Transform = false;
                            break;
                        }
                    case 1:
                        {
                            StartCoroutine(GrownMario());
                            Transform = false;
                            break;
                        }
                    case 2:
                        {
                            StartCoroutine(SuperMario());
                            Transform = false;
                            break;
                        }
                    default: Transform = false; break;
                }
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Platform"))
            {
                IsGrounded = true;
            }
            else if (collision.gameObject.CompareTag("MysteryBox"))
            {
                RotatedCoin.SetActive(true);
            }
            else if (collision.gameObject.CompareTag("Mushroom"))
            {
                points -= 100;
                CollisionCount++;
                if (CollisionCount >= MaxCollision)
                {
                    this.gameObject.SetActive(false);
                    LoseText.SetActive(true);
                    //yield return new WaitForSeconds(2);
                    SceneManager.LoadScene("Level 1");
                }
            }
            else if (collision.gameObject.CompareTag("Flag"))
            {
                //yield return new WaitForSeconds(2);
                Flag.gameObject.SetActive(false);
            }
            else if (collision.gameObject.CompareTag("Gate"))
            {
                this.gameObject.SetActive(false);
                //yield return new WaitForSeconds(2);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("RotatedCoin"))
            {
                collision.gameObject.SetActive(false);
                SetPoint();
            }
            else if (collision.gameObject.CompareTag("Coin"))
            {
                collision.gameObject.SetActive(false);
                SetPoint();
            }
        }
        public void SetPoint()
        {
            points += 600;
            PoinText.text = "Point: " + points.ToString();
            if (points >= 500)
            {
                Gate.SetActive(true);
            }
        }
        private IEnumerator GrownMario()
        {
            float delay = 0.1f;
            Player.SetLayerWeight(Player.GetLayerIndex("SmallMario"), 0);
            Player.SetLayerWeight(Player.GetLayerIndex("BigMario"), 1);
            Player.SetLayerWeight(Player.GetLayerIndex("BigRedMario"), 0);
            yield return new WaitForSeconds(delay);
            Player.SetLayerWeight(Player.GetLayerIndex("SmallMario"), 1);
            Player.SetLayerWeight(Player.GetLayerIndex("BigMario"), 0);
            Player.SetLayerWeight(Player.GetLayerIndex("BigRedMario"), 0);
            yield return new WaitForSeconds(delay);
            Player.SetLayerWeight(Player.GetLayerIndex("SmallMario"), 0);
            Player.SetLayerWeight(Player.GetLayerIndex("BigMario"), 1);
            Player.SetLayerWeight(Player.GetLayerIndex("BigRedMario"), 0);
            yield return new WaitForSeconds(delay);
            Player.SetLayerWeight(Player.GetLayerIndex("SmallMario"), 1);
            Player.SetLayerWeight(Player.GetLayerIndex("BigMario"), 0);
            Player.SetLayerWeight(Player.GetLayerIndex("BigRedMario"), 0);
            yield return new WaitForSeconds(delay);
            Player.SetLayerWeight(Player.GetLayerIndex("SmallMario"), 0);
            Player.SetLayerWeight(Player.GetLayerIndex("BigMario"), 1);
            Player.SetLayerWeight(Player.GetLayerIndex("BigRedMario"), 0);
            yield return new WaitForSeconds(delay);
            Player.SetLayerWeight(Player.GetLayerIndex("SmallMario"), 1);
            Player.SetLayerWeight(Player.GetLayerIndex("BigMario"), 0);
            Player.SetLayerWeight(Player.GetLayerIndex("BigRedMario"), 0);
            yield return new WaitForSeconds(delay);
        }
        private IEnumerator SuperMario()
        {
            float delay = 0.1f;
            Player.SetLayerWeight(Player.GetLayerIndex("SmallMario"), 0);
            Player.SetLayerWeight(Player.GetLayerIndex("BigMario"), 0);
            Player.SetLayerWeight(Player.GetLayerIndex("BigRedMario"), 1);
            yield return new WaitForSeconds(delay);
            Player.SetLayerWeight(Player.GetLayerIndex("SmallMario"), 0);
            Player.SetLayerWeight(Player.GetLayerIndex("BigMario"), 1);
            Player.SetLayerWeight(Player.GetLayerIndex("BigRedMario"), 0);
            yield return new WaitForSeconds(delay);
            Player.SetLayerWeight(Player.GetLayerIndex("SmallMario"), 0);
            Player.SetLayerWeight(Player.GetLayerIndex("BigMario"), 0);
            Player.SetLayerWeight(Player.GetLayerIndex("BigRedMario"), 1);
            yield return new WaitForSeconds(delay);
            Player.SetLayerWeight(Player.GetLayerIndex("SmallMario"), 0);
            Player.SetLayerWeight(Player.GetLayerIndex("BigMario"), 1);
            Player.SetLayerWeight(Player.GetLayerIndex("BigRedMario"), 0);
            yield return new WaitForSeconds(delay);
            Player.SetLayerWeight(Player.GetLayerIndex("SmallMario"), 0);
            Player.SetLayerWeight(Player.GetLayerIndex("BigMario"), 0);
            Player.SetLayerWeight(Player.GetLayerIndex("BigRedMario"), 1);
            yield return new WaitForSeconds(delay);
            Player.SetLayerWeight(Player.GetLayerIndex("SmallMario"), 0);
            Player.SetLayerWeight(Player.GetLayerIndex("BigMario"), 1);
            Player.SetLayerWeight(Player.GetLayerIndex("BigRedMario"), 0);
            yield return new WaitForSeconds(delay);
        }
        private IEnumerator TinyMario()
        {
            float delay = 0.1f;
            Player.SetLayerWeight(Player.GetLayerIndex("SmallMario"), 0);
            Player.SetLayerWeight(Player.GetLayerIndex("BigMario"), 1);
            Player.SetLayerWeight(Player.GetLayerIndex("BigRedMario"), 0);
            yield return new WaitForSeconds(delay);
            Player.SetLayerWeight(Player.GetLayerIndex("SmallMario"), 1);
            Player.SetLayerWeight(Player.GetLayerIndex("BigMario"), 0);
            Player.SetLayerWeight(Player.GetLayerIndex("BigRedMario"), 0);
            yield return new WaitForSeconds(delay);
            Player.SetLayerWeight(Player.GetLayerIndex("SmallMario"), 0);
            Player.SetLayerWeight(Player.GetLayerIndex("BigMario"), 1);
            Player.SetLayerWeight(Player.GetLayerIndex("BigRedMario"), 0);
            yield return new WaitForSeconds(delay);
            Player.SetLayerWeight(Player.GetLayerIndex("SmallMario"), 1);
            Player.SetLayerWeight(Player.GetLayerIndex("BigMario"), 0);
            Player.SetLayerWeight(Player.GetLayerIndex("BigRedMario"), 0);
            yield return new WaitForSeconds(delay);
            Player.SetLayerWeight(Player.GetLayerIndex("SmallMario"), 0);
            Player.SetLayerWeight(Player.GetLayerIndex("BigMario"), 1);
            Player.SetLayerWeight(Player.GetLayerIndex("BigRedMario"), 0);
            yield return new WaitForSeconds(delay);
            Player.SetLayerWeight(Player.GetLayerIndex("SmallMario"), 1);
            Player.SetLayerWeight(Player.GetLayerIndex("BigMario"), 0);
            Player.SetLayerWeight(Player.GetLayerIndex("BigRedMario"), 0);
            yield return new WaitForSeconds(delay);
        }
    }
}
