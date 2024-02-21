using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPostionPlayer : MonoBehaviour
{
    private void Update()
    {
        // Lấy vị trí của nhân vật và gửi nó cho quái vật.
        Vector3 characterPosition = transform.position;
        EnemiesAIController.Instance.UpdateCharacterPosition(characterPosition);
    }
}
