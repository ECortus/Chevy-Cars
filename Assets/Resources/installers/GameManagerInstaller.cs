using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManagerInstaller : MonoInstaller
{
    [SerializeField] private GameManager pool;

    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromInstance(pool).AsSingle();
        Container.QueueForInject(pool);
    }
}