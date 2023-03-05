using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnArcher : MonoBehaviour
{
    public GameObject ArcherGameObject;
    public Grid m_Grid;

    [SerializeField]
    Transform m_SpawnPoint;
    [SerializeField]
    GameObject m_PlacesForArchers;
    [SerializeField]
    Tilemap m_PositionsForArchers;
    [SerializeField]
    GameObject m_Floor;

    List<GameObject> m_Archers = new List<GameObject>();

    private void Start()
    {
        PutPlacesOnPositionsArchers();
    }

    public GameObject SpawnArcherOnPosition()
    {
        GameObject archer = Instantiate(ArcherGameObject, m_SpawnPoint.position, Quaternion.identity);
        //archer.GetComponent<MouseDrag>().grid = grid;
        archer.GetComponent<MouseDrag>().Clicked += ShowPLacesForArchers;
        archer.GetComponent<MouseDrag>().DroppedL += HidePLacesForArchers;
        archer.transform.parent = m_SpawnPoint.transform;
        m_Archers.Add(archer);
        return m_Archers.Last();
    }

    private void ShowPLacesForArchers()
    {
        m_PlacesForArchers.SetActive(true);
        //Animation to appear
        int Cycles = m_PlacesForArchers.transform.childCount;
        while (Cycles-- > 0)
        {
            m_PlacesForArchers.transform.GetChild(Cycles).GetComponent<Floor>().AppearAnimation();
        }
    }

    private void PutPlacesOnPositionsArchers() {

        //Si en el grid existe un tile, instancio "place" en la posición del tile y hago parent de "m_PlacesForArchers"
        foreach (var position in m_PositionsForArchers.cellBounds.allPositionsWithin)
        {
            if (m_PositionsForArchers.GetTile(position)!=null) {
                GameObject floor = Instantiate(m_Floor, m_Grid.CellToWorld(position), Quaternion.identity);
                floor.transform.parent = m_PlacesForArchers.transform;
                floor.transform.position += new Vector3(0, 0.25f, 0);
                //place.GetComponent<Floor>().TryUpgradeTower += EconomyManager.TryToBuild;
            }
        } 

    }

    private void HidePLacesForArchers()
    {
        //Animation to Disappear
        m_PlacesForArchers.SetActive(false);
    }

    public void DeleteLastArcher()
    {
        Destroy(m_Archers[m_Archers.Count - 1].gameObject);
        m_Archers.RemoveAt(m_Archers.Count - 1);
    }
}
