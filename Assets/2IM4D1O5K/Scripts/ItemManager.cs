using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private Vector3 _rbVec;

    private Rigidbody _rb;
    private int _rand;
    // Start is called before the first frame update
    void Start()
    {
        _rand = Random.Range(0, 6);
        _rb = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.Rotate(Vector3.up);
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PlayerCollider"))
        {
            switch (_rand)
            {
                case 0:
                    GameManager.sumCoin += 1000000;
                    break;  
                case 1:
                    GameManager.sumCoin += 5000000;
                    break;
                case 2:
                    GameManager.sumCoin += 10000000;
                    break;
                case 3:
                    _rb.AddForce(_rbVec * 2500, ForceMode.Impulse);
                    break;
                case 4:
                    _rb.AddForce(_rbVec * 5000, ForceMode.Impulse);
                    break;
                case 5:
                    GameManager.isShopOpen = true;
                    break;

            }
            Destroy(this.gameObject);
        }
    }
}
