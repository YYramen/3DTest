using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの動きを制御するコンポーネント
/// </summary>
public class PlayerMove : MonoBehaviour
{
    [SerializeField, Tooltip("移動速度")] float _moveSpeed = 1.0f;

    [Header("視点移動")]
    [SerializeField, Tooltip("カメラの位置")] Transform _aimPos;
    [SerializeField] AxisState _vertical;
    [SerializeField] AxisState _horizontal;

    Rigidbody _rb;
    Animator _anim;
    int _hashFront = Animator.StringToHash("Front");
    int _hashSide = Animator.StringToHash("Side");

    bool _dashFlg;
    [Header("ダッシュ機能")]
    [SerializeField, Tooltip("Rayを飛ばす位置")] Transform _rayPos;
    [SerializeField, Tooltip("ダッシュ距離")] float _rayRange = 3.0f;
    [SerializeField] int _rayValue;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        InputMove();
    }

    /// <summary>
    /// 入力受け付け、移動速度、アニメーター関数
    /// 移動方法は Velocity で行う
    /// </summary>
    private void InputMove()
    {
        // 入力を受け付ける
        var x = Input.GetAxisRaw("Horizontal");
        var z = Input.GetAxisRaw("Vertical");
        var dir = new Vector3(x, 0, z).normalized;
        _horizontal.Update(Time.deltaTime);
        _vertical.Update(Time.deltaTime);

        // 移動速度
        var velocity = _moveSpeed * dir;

        // キャラクターの向きを更新
        var hRotation = Quaternion.AngleAxis(_horizontal.Value, Vector3.up);
        var vRotation = Quaternion.AngleAxis(_vertical.Value, Vector3.right);
        transform.rotation = hRotation;
        _aimPos.localRotation = vRotation;

        // Animatorに値を反映
        _anim.SetFloat(_hashFront, velocity.z, 0.1f, Time.deltaTime);
        _anim.SetFloat(_hashSide, velocity.x, 0.1f, Time.deltaTime);

        // ダッシュ
        Debug.DrawRay(_rayPos.transform.position, transform.forward * _rayRange, Color.blue);
        if (Input.GetButtonDown("Dash"))
        {
            dir = Camera.main.transform.TransformDirection(dir);
            dir.y = 0;
            Dash(dir.normalized);
        }

        if (_dashFlg)
        {
            _rb.velocity = Vector3.zero;
            _dashFlg = false;
        }
    }

    /// <summary>
    /// ダッシュ
    /// </summary>
    /// <param name="dir">ダッシュの方向</param>
    private void Dash(Vector3 dir)
    {
        _dashFlg = true;

        RaycastHit hitObj;
        bool hit = Physics.Raycast(_rayPos.transform.position, dir, out hitObj, _rayRange);

        if (hit && hitObj.collider.gameObject.layer == _rayValue)
        {
            Debug.Log(hitObj.distance);
            if (hitObj.distance < 0.8f)
            {
                return;
            }
            else
            {
                float b = _rayRange - hitObj.distance - 0.2f;
                transform.position = transform.position + dir * b;
            }
        }
        else
        {
            transform.position = transform.position + dir * _rayRange;
        }
    }
}
