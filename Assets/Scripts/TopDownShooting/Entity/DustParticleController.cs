using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustParticleController : MonoBehaviour
{
    [SerializeField] private bool _createDustOnWalk = true;
    [SerializeField] private ParticleSystem _dustParticleSystem;

    // 애니메이션 이벤트를 통해 호출되는 메소드
    public void CreateDustParticle()
    {
        if(_createDustOnWalk)
        {
            _dustParticleSystem.Stop();
            _dustParticleSystem.Play();
        }
    }
}
