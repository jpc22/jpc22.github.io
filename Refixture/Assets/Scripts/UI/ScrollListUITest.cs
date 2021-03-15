using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrollListUITest : MonoBehaviour
{
    [SerializeField] private GameObject _fixturePrefab;
    [SerializeField] private FixtureContainerSO _selectedFixturesListSO;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _resetButton;
    private List<Fixture> _fixtureList = new List<Fixture>();

    public bool closeCanvas;

    void Start()
    {

    }

    public void StartRun()
    {
        if (closeCanvas)
            _canvas.SetActive(false);
        _resetButton.SetActive(true);
        List<FixtureSO> spawnList = _selectedFixturesListSO.Fixtures;
        float x = 0;
        foreach (FixtureSO so in spawnList)
        {
            GameObject fixture = Instantiate(_fixturePrefab);
            Fixture fixtureScript = fixture.GetComponent<Fixture>();
            fixtureScript.FixtureSO = so;
            fixtureScript.Spawn2dFixture();
            fixtureScript.Spawn3dFixture();
            _fixtureList.Add(fixtureScript);

            fixture.transform.position = new Vector3(x, 0);
            x += 2;
            fixtureScript.Transform2d.eulerAngles = new Vector3(90, 0);
        }
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EditRoomPressed()
    {
        SceneManager.LoadScene("EditRoomScene");
    }
}
