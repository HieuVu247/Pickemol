using UnityEngine;

public class BeerSpawner : MonoBehaviour
{
    public GameObject beerPrefab; // Tham chiếu đến Prefab của bia
    public float maxSpawnInterval = 2f; // Thời gian sinh tối đa của bia
    public float minSpawnInterval = 0.3f; // Thời gian sinh tối thiểu của bia
    public float totalReductionTime = 300f; // Tổng thời gian giảm (5 phút = 300 giây)
    public float spawnY = 5f; // Độ y cố định để spawn bia
    public float spawnMinX = -8f; // Giới hạn x nhỏ nhất để spawn bia
    public float spawnMaxX = 8f; // Giới hạn x lớn nhất để spawn bia
    public Color gizmoColor = Color.green; // Màu sắc của gizmo

    private float elapsedTime = 0f;

    private void Start()
    {
        Invoke("SpawnBeer", maxSpawnInterval);
    }

    private void SpawnBeer()
    {
        // Tạo vị trí spawn ngẫu nhiên trong khoảng x định sẵn
        float spawnX = Random.Range(spawnMinX, spawnMaxX);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);
        Instantiate(beerPrefab, spawnPosition, Quaternion.identity);

        // Tăng thời gian đã trôi qua
        elapsedTime += Mathf.Min(maxSpawnInterval, elapsedTime + Time.deltaTime);

        // Tính toán thời gian sinh mới
        float t = Mathf.Clamp01(elapsedTime / totalReductionTime);
        float currentSpawnInterval = Mathf.Lerp(maxSpawnInterval, minSpawnInterval, t);

        // Gọi lại hàm SpawnBeer với thời gian sinh mới
        Invoke("SpawnBeer", currentSpawnInterval);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        // Vẽ một đường nối giữa hai điểm spawn min và max
        Gizmos.DrawLine(new Vector3(spawnMinX, spawnY, 0f), new Vector3(spawnMaxX, spawnY, 0f));
        // Vẽ các khối nhỏ tại các điểm min và max để dễ nhận diện
        Gizmos.DrawSphere(new Vector3(spawnMinX, spawnY, 0f), 0.2f);
        Gizmos.DrawSphere(new Vector3(spawnMaxX, spawnY, 0f), 0.2f);
    }
}