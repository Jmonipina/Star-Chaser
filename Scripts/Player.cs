using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDataPersistence
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;

    Vector2 rawInput;

    Vector2 minBound;
    Vector2 maxBound;

    Shooter shooter;

    void Awake() 
    {
        shooter = GetComponent<Shooter>();    
    }
    void Start()
    {
        InitBound();
    }

    void Update()
    {
        Move();
    }

    void InitBound()
    {
        Camera mainCamera = Camera.main;
        minBound = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
        maxBound = mainCamera.ViewportToWorldPoint(new Vector2(1,1));
    }
    void Move()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBound.x + paddingLeft, maxBound.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBound.y + paddingBottom, maxBound.y - paddingTop);
        transform.position = newPos;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if (shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }

    public void LoadData(GameData data){
        this.transform.position = data.playerPosition;
    }
    public void SaveData(ref GameData data){
        data.playerPosition = this.transform.position;
    }
}
