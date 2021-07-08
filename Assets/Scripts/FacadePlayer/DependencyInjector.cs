using UnityEngine;

public class DependencyInjector : MonoBehaviour
{
    public static T Bind<T>(T typeParametr, T dependentEntity)
    {
        return dependentEntity;
    }
}