using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] private GameObject currentHitObject; // игровой объект, которого коснулся враг
    [SerializeField] private float circleRadius; //радиус окружности
    [SerializeField] private float maxDistance; //предел видимости
    [SerializeField] private LayerMask layerMask;  //

    private EnemyController _enemyController;
    private Vector2 _origin; //где создается окружность (находится противник)
    private Vector2 _direction; //направление от точки до окружности

    private float currentHitDistance;

    public void Start()
    {
        _enemyController = GetComponent<EnemyController>();
    }

    private void Update()
    {
        _origin = transform.position;

        if (_enemyController.IsFacingRifht)
        {
            _direction = Vector2.right;
        }
        else
        {
            _direction = Vector2.left;
        }
        

        RaycastHit2D hit = Physics2D.CircleCast(_origin, circleRadius, _direction, maxDistance, layerMask); //создание невидимого объекта
                                                                                                          //в виде окружности

        if (hit)
        {
            currentHitObject = hit.transform.gameObject; //сохраняем объет, который ударился о коллайдер
            currentHitDistance = hit.distance;

            if (currentHitObject.CompareTag("Player"))
            {
                _enemyController.StartChasingPlayer();
            }

        }
        else
        {
            currentHitObject = null;
            currentHitDistance = maxDistance;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_origin, _origin + _direction * currentHitDistance);
        Gizmos.DrawWireSphere(_origin + _direction * currentHitDistance, circleRadius);

    }
}
