using UnityEngine;
using Pool;

public class OutbuildingSpawner : MonoBehaviour
{
    [SerializeField] private Transform _playerCollision;

    public static OutbuildingSpawner Instance { get; private set; }

    private OutbuildingPreview _outbuildingPreview;
    private OutbuildingConfig _outbuildingConfig;
    private Renderer _renderer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _outbuildingPreview != null && _outbuildingPreview.IsAvailable())
        {
            GrowBuilding(_outbuildingPreview.transform.localPosition);
            DestroyPlantPreview();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && _outbuildingPreview != null)
        {
            DestroyPlantPreview();
            Coin.Instance.GetCoin(_outbuildingConfig.ShopCost);
        }
    }
    
    public Outbuilding SpawnBuilding(OutbuildingConfig config)
    {
        var biulding = PoolObject.Get<Outbuilding>();
        biulding.SetConfig(config);
        _renderer = biulding.GetBuildingRenderer();
        
        return biulding;
    }

    private void GrowBuilding(Vector3 position)
    {
        var plant = SpawnBuilding(_outbuildingConfig);
        plant.transform.position = position;
    }
    
    public void CreateBuildingPreview(OutbuildingConfig outbuildingConfig)
    {
        var preview = PoolObject.Get<OutbuildingPreview>();
        preview.transform.position = _playerCollision.position;
        preview.SetBuildSprite(outbuildingConfig.BuildingSprite);
        _outbuildingPreview = preview;
        _outbuildingConfig = outbuildingConfig;
    }

    private void DestroyPlantPreview()
    {
        if(_outbuildingPreview != null)
            PoolObject.Release(_outbuildingPreview);
        
        _outbuildingPreview = null;
    }
}
