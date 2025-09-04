using UnityEngine;
using UnityEditor;

public class BulletPrefabLoader : MonoBehaviour
{
    [MenuItem("Tools/Load Bullet Prefab to Player")]
    public static void LoadBulletPrefabToPlayer()
    {
        // PlayerShip 찾기
        GameObject playerShip = GameObject.Find("PlayerShip");
        if (playerShip == null)
        {
            Debug.LogError("PlayerShip not found!");
            return;
        }
        
        // PlayerController 컴포넌트 찾기
        PlayerController playerController = playerShip.GetComponent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController component not found on PlayerShip!");
            return;
        }
        
        // Bullet 프리팹 로드
        GameObject bulletPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Bullet.prefab");
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet prefab not found at Assets/Prefabs/Bullet.prefab!");
            return;
        }
        
        // PlayerController에 프리팹 할당
        playerController.bulletPrefab = bulletPrefab;
        
        Debug.Log("Bullet prefab successfully loaded to PlayerController!");
    }
}