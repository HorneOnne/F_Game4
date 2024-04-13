using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllers : MonoBehaviour
{
    public static GameControllers Instance { get; private set; }
    public static event System.Action<MahjongSO> OnMatched;
    public static event System.Action OnCompleteRequest;
    public Transform Center;

    private List<MahjongSO> _allMahjongData;
    [SerializeField] private LayerMask _mahjongLayer;


    private Vector2 _mahjongOffset = new Vector2(1.5f, 1.75f);
    [HideInInspector] public List<Mahjong> Mahjongs = new();
    private List<MahjongSO> _initializeData;
    private int _currentGetDataIndex = 0;


    private Vector4 layer1;
    private Vector4 layer2;
    private Vector4 layer3;



    public int Layer1Size { get => (int)layer1.x + (int)layer1.y + (int)layer1.z + (int)layer1.w; }
    public int Layer2Size { get => (int)layer2.x + (int)layer2.y + (int)layer2.z + (int)layer2.w; }
    public int Layer3Size { get => (int)layer3.x + (int)layer3.y + (int)layer3.z + (int)layer3.w; }

    private System.Random rng;

    // Win effect
    private Flash _flashEffectInstance;

    public enum PlayState
    {
        CheckingCanplayable,
        Default,
        Checking,
    }
    public PlayState State;
    private float _checkTimer = 0.0f;

    // Selection
    public Mahjong SelectionA;
    public Mahjong SelectionB;



    private void Awake()
    {
        Instance = this;
        long seed = System.DateTime.Now.Ticks;
        rng = new System.Random((int)seed);

        _allMahjongData = new();
        for (int i = 0; i < GameManager.Instance.GalleryCollections.Count; i++)
        {
            _allMahjongData.Add(GameManager.Instance.GalleryCollections[i]);
        }


    }

    private void Start()
    {
        layer1 = GameManager.Instance.CurrentLevel.Layer1;
        layer2 = GameManager.Instance.CurrentLevel.Layer2;
        layer3 = GameManager.Instance.CurrentLevel.Layer3;
        Center.position = GameManager.Instance.CurrentLevel.CenterPosition;

        State = PlayState.CheckingCanplayable;
        int size = Layer1Size + Layer2Size + Layer3Size;
        if (size % 2 != 0)
        {
            Debug.Log($"Size not a even number.: {size}");
        }

        _initializeData = new();
        _currentGetDataIndex = 0;
        for (int i = 0; i < size; i++)
        {
            if (i < size / 2)
            {
                _initializeData.Add(_allMahjongData[Random.Range(0, _allMahjongData.Count)]);
            }
            else
            {
                _initializeData.Add(_initializeData[i % (size / 2)]);
            }
        }
        ShuffleList(_initializeData);

        GenerateLayerOfMahjong((int)layer1.x, (int)layer1.y, (int)layer1.z, (int)layer1.w, 0);
        GenerateLayerOfMahjong((int)layer2.x, (int)layer2.y, (int)layer2.z, (int)layer2.w, 1);
        GenerateLayerOfMahjong((int)layer3.x, (int)layer3.y, (int)layer3.z, (int)layer3.w, 2);

    }



    public void RecreateTable()
    {

        _currentGetDataIndex = 0;
        _initializeData.Clear();
        for (int i = 0; i < Mahjongs.Count; i++)
        {
            _initializeData.Add(Mahjongs[i].Data);
        }
        ShuffleList(_initializeData);
        for (int i = 0; i < Mahjongs.Count; i++)
        {
            Destroy(Mahjongs[i].gameObject);
        }
        Mahjongs.Clear();


        GenerateLayerOfMahjong((int)layer1.x, (int)layer1.y, (int)layer1.z, (int)layer1.w, 0);
        GenerateLayerOfMahjong((int)layer2.x, (int)layer2.y, (int)layer2.z, (int)layer2.w, 1);
        GenerateLayerOfMahjong((int)layer3.x, (int)layer3.y, (int)layer3.z, (int)layer3.w, 2);
    }


    private void Update()
    {
        if (GameplayManager.Instance.CurrentState == GameplayManager.GameState.PLAYING)
        {
            switch (State)
            {
                case PlayState.CheckingCanplayable:
                    if (CanPlayble())
                    {
                        State = PlayState.Default;
                    }
                    else
                    {
                        if (Mahjongs.Count <= 1) return;
                        RecreateTable();
                    }
                    break;
                default:
                case PlayState.Default:
                    if (Input.GetMouseButtonDown(0))
                    {
                        // Cast a ray from the mouse position
                        Vector2 raycastOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        RaycastHit2D[] hits = Physics2D.RaycastAll(raycastOrigin, Vector2.zero, Mathf.Infinity, _mahjongLayer);

                        // Check if any objects are hit
                        if (hits.Length > 0)
                        {
                            // Find the object with the highest sorting order
                            GameObject highestOrderObject = FindObjectWithHighestSortingOrder(hits);

                            // Handle the highest order object
                            if (highestOrderObject != null)
                            {
                                //Debug.Log("Object with highest sorting order hit: " + highestOrderObject.name);
                                // You can perform additional actions here, such as accessing properties or calling methods on the highest order object

                                if (highestOrderObject.gameObject.TryGetComponent<Mahjong>(out Mahjong selectedMahjong))
                                {
                                    if (selectedMahjong.CanSelect == false) break;
                                    Select(selectedMahjong);

                                    if (SelectionA != null && SelectionB != null)
                                    {
                                        State = PlayState.Checking;

                                    }
                                }


                            }
                        }
                        else
                        {
                            // If the ray doesn't hit any object
                            Debug.Log("No object hit.");
                        }

                    }
                    break;
                case PlayState.Checking:
                    _checkTimer += Time.deltaTime;
                    if (_checkTimer > 0.25f)
                    {
                        //Debug.Log(Mahjongs.Count);
                        _checkTimer = 0.0f;
                        if (CheckMatch(SelectionA, SelectionB))
                        {
                            SoundManager.Instance.PlaySound(SoundType.Button, false);
                            // Match
                            OnMatched?.Invoke(SelectionA.Data);

                            SelectionA.SetMatchPhysics();
                            SelectionB.SetMatchPhysics();


                            var a = SelectionA.gameObject;
                            var b = SelectionB.gameObject;



                            Mahjongs.Remove(SelectionA);
                            Mahjongs.Remove(SelectionB);

                            Destroy(a.gameObject, 2.0f);
                            Destroy(b.gameObject, 2.0f);

                            SelectionA = null;
                            SelectionB = null;
                        }
                        else
                        {
                            SoundManager.Instance.PlaySound(SoundType.HitBlock, false);
                            // Not match
                            //Debug.Log("No match");

                            // reset selection
                            SelectionA.SelectEffect(false);
                            SelectionB.SelectEffect(false);
                            SelectionA = null;
                            SelectionB = null;
                        }

                        State = PlayState.CheckingCanplayable;
                    }
                    break;
            }
        }
        else if (GameplayManager.Instance.CurrentState == GameplayManager.GameState.WIN)
        {
            Vector3 direction = Vector2.zero- (Vector2)Mahjongs[0].transform.position;
            Mahjongs[0].transform.position += direction * 2f * Time.deltaTime;

            if (Vector2.Distance(Mahjongs[0].transform.position, Vector2.zero) < 0.01f)
            {
                if (_flashEffectInstance != null)
                {
                    var flashPrefab = Resources.Load<Flash>("Flash");
                    if(flashPrefab != null)
                    {
                        _flashEffectInstance = Instantiate(flashPrefab, Vector2.zero, Quaternion.identity);
                    }
                 
                }
            }
        }


    }

    private void GenerateLayerOfMahjong(int line1Width, int line2Width, int line3Width, int line4Width, int layer)
    {
        float startLine1 = Center.position.x - ((line1Width - 1) / 2.0f * _mahjongOffset.x);
        for (int x = 0; x < line1Width; x++)
        {
            Vector2 position = new Vector2(startLine1 + x * _mahjongOffset.x, Center.position.y + _mahjongOffset.y) + new Vector2(0, layer * 0.75f);

            if (_currentGetDataIndex < _initializeData.Count)
            {
                var data = _initializeData[_currentGetDataIndex];
                _currentGetDataIndex++;
                Mahjongs.Add(CreateMahjong(data, position, layer));
            }
        }

        float startLine2 = Center.position.x - ((line2Width - 1) / 2.0f * _mahjongOffset.x);
        for (int x = 0; x < line2Width; x++)
        {
            Vector2 position = new Vector2(startLine2 + x * _mahjongOffset.x, Center.position.y) + new Vector2(0, layer * 0.75f);

            if (_currentGetDataIndex < _initializeData.Count)
            {
                var data = _initializeData[_currentGetDataIndex];
                _currentGetDataIndex++;
                Mahjongs.Add(CreateMahjong(data, position, layer));
            }
        }

        float startLine3 = Center.position.x - ((line3Width - 1) / 2.0f * _mahjongOffset.x);
        for (int x = 0; x < line3Width; x++)
        {
            Vector2 position = new Vector2(startLine3 + x * _mahjongOffset.x, Center.position.y - _mahjongOffset.y) + new Vector2(0, layer * 0.75f);
            if (_currentGetDataIndex < _initializeData.Count)
            {
                var data = _initializeData[_currentGetDataIndex];
                _currentGetDataIndex++;
                Mahjongs.Add(CreateMahjong(data, position, layer));
            }
        }

        float startLine4 = Center.position.x - ((line4Width - 1) / 2.0f * _mahjongOffset.x);
        for (int x = 0; x < line4Width; x++)
        {
            Vector2 position = new Vector2(startLine4 + x * _mahjongOffset.x, Center.position.y - (_mahjongOffset.y * 2)) + new Vector2(0, layer * 0.75f);
            if (_currentGetDataIndex < _initializeData.Count)
            {
                var data = _initializeData[_currentGetDataIndex];
                _currentGetDataIndex++;
                Mahjongs.Add(CreateMahjong(data, position, layer));
            }
        }
    }

    //public Vector2 GetPosition(int line, int lineWidth, int layer)
    //{
    //    float startLine = Center.position.x - ((lineWidth - 1) / 2.0f * _mahjongOffset.x);
    //    if (line == 1)
    //    {
    //        return new Vector2(line + Random.Range(0, startLine) * _mahjongOffset.x, Center.position.y + _mahjongOffset.y) + new Vector2(layer * 0.1f, layer * 0.1f);
    //    }
    //    else if (line == 2)
    //    {
    //        return new Vector2(line + Random.Range(0, startLine) * _mahjongOffset.x, Center.position.y) + new Vector2(layer * 0.1f, layer * 0.1f);
    //    }
    //    else
    //    {
    //        return new Vector2(line + Random.Range(0, startLine) * _mahjongOffset.x, Center.position.y - _mahjongOffset.y) + new Vector2(layer * 0.1f, layer * 0.1f);
    //    }
    //}

    public Mahjong CreateMahjong(MahjongSO data, Vector2 position, int layer)
    {
        var prefab = Resources.Load<Mahjong>("Mahjong");
        if (prefab != null)
        {
            var mahjongInstance = Instantiate(prefab, position, Quaternion.identity).GetComponent<Mahjong>();
            mahjongInstance.SetData(data, layer);
            return mahjongInstance;
        }
        return null;
    }




    GameObject FindObjectWithHighestSortingOrder(RaycastHit2D[] hits)
    {
        GameObject highestOrderObject = null;
        int highestSortingOrder = int.MinValue;

        foreach (RaycastHit2D hit in hits)
        {
            // Check if the hit object has a sprite renderer
            SpriteRenderer renderer = hit.collider.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                // Check if the sorting order of the sprite renderer is higher than the current highest sorting order
                if (renderer.sortingOrder > highestSortingOrder)
                {
                    highestSortingOrder = renderer.sortingOrder;
                    highestOrderObject = hit.collider.gameObject;
                }
            }
        }

        return highestOrderObject;
    }


    #region Gameplay
    public void Select(Mahjong mahjong)
    {
        if (mahjong == SelectionA) return;

        if (SelectionA == null)
        {
            SelectionA = mahjong;
            SelectionA.SelectEffect(true);

        }
        else if (SelectionB == null)
        {
            SelectionB = mahjong;
            SelectionB.SelectEffect(true);
        }
    }

    public bool CheckMatch(Mahjong a, Mahjong b)
    {
        return a.Data.ID == b.Data.ID;
    }



    private bool CanPlayble()
    {
        if (Mahjongs.Count == 1)
        {
            GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.WIN);
            for(int i = 0; i < GameManager.Instance.GalleryCollections.Count; i++)
            {
                if (GameManager.Instance.GalleryCollections[i].ID == Mahjongs[0].Data.ID)
                {
                    GameManager.Instance.GalleryCollections[i].Unlock = true;
                }
            }
            GameManager.Instance.TriggerGallaryUpdated();
            return false;
        }
        for (int i = 0; i < Mahjongs.Count; i++)
        {
            for (int j = 1; j < Mahjongs.Count; j++)
            {
                if (i == j) continue;

                if (Mahjongs[i].CanSelect && Mahjongs[j].CanSelect)
                {
                    if (Mahjongs[i].Data.ID == Mahjongs[j].Data.ID)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    #endregion
}
