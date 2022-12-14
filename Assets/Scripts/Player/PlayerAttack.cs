using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlayerΜν¬ΦW
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    [Header("ψΚΉ")]
    [SerializeField, Tooltip("UPΜψΚΉ")] AudioClip _atkOneSe;
    [SerializeField, Tooltip("UQΜψΚΉ")] AudioClip _atkTwoSe;

    [Header("UΜRC_[")]
    [SerializeField, Tooltip("UΜRC_[")] GameObject[] _colliders;

    bool _attackOne;
    bool _attackTwo;

    Animator _anim;
    AudioSource _audioSource;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        foreach (var collider in _colliders)
        {
            collider.SetActive(false);
        }
    }

    private void Update()
    {
        InputAttack();
    }

    /// <summary>
    /// όΝπσ―t―ι
    /// </summary>
    private void InputAttack()
    {
        if (Input.GetButtonDown("Fire1") && _attackOne == false)
        {
            _attackOne = true;
            _anim.SetTrigger("Attack1");
            _audioSource.PlayOneShot(_atkOneSe);
            Debug.Log("UP");
        }
        else if (Input.GetButtonDown("Fire2") && _attackTwo == false)
        {
            _attackTwo = true;
            _anim.SetTrigger("Attack2");
            _audioSource.PlayOneShot(_atkTwoSe);
            Debug.Log("UQ");
        }
        else
        {
            _attackOne = false;
            _attackTwo = false;
        }
    }

    //-----«AnimationEventΕg€ΦiU»θΜIItj«-----//
    public void OnAttackOneCollider()
    {
        _colliders[0].SetActive(true);
    }

    public void OffAttackOneCollider()
    {
        _colliders[0].SetActive(false);
    }

    public void OnAttackTwoCollider()
    {
        _colliders[1].SetActive(true);
    }

    public void OffAttackTwoCollider()
    {
        _colliders[1].SetActive(false);
    }
}
