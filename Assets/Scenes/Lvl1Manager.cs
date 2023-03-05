using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lvl1Manager : MonoBehaviour
{

    [SerializeField]
    GameObject m_PanelValues;
    [SerializeField]
    Spawner m_Spawner;
    [SerializeField]
    int m_TimeBtwnWeaves;
    int m_TimeLeftBtwnWeave;
    [Serializable]
    public class Waves
    {
        public int EnemiesPerWave;
        public Enemy TypeEnemy;
        public int Resources;
    }

    public List<Waves> m_Waves = new List<Waves>();
    int Cycle = 0;
    bool m_Started = false;
    int m_EnemiesLeft;
    int m_Timer = 0;
    Coroutine m_Coroutine;
    public void StartLvl()
    {
        if (!m_Started)
        {
            m_Started = true;
            m_Coroutine = StartCoroutine(Tempo());
        }
    }

    IEnumerator Tempo()
    {
        while (Cycle < m_Waves.Count)
        {
            m_PanelValues.SetActive(false);
            m_TimeLeftBtwnWeave = m_TimeBtwnWeaves;
            m_Spawner.EnemyType = m_Waves[Cycle].TypeEnemy; // Tipo de enmigo a invocar
            m_Spawner.EnemiesPerWeave = m_Waves[Cycle].EnemiesPerWave; // Cuantos invocamos cada segundo
            m_EnemiesLeft += m_Waves[Cycle].EnemiesPerWave; //Saber cuantos enemigos nos quedan.
            m_PanelValues.GetComponent<SetStuffValues>().ChangeSliderValue(m_Waves[Cycle].Resources); // Recursos que se tendrán despues de cada oleada.
            m_Spawner.StartSpawning(); // Comienzan las instancias
            while (m_TimeLeftBtwnWeave-- > 0)
            {
                yield return new WaitForSeconds(1);
                m_Timer++;
            }
            Cycle++;
        }
        Cycle = 0;
        StopCoroutine(m_Coroutine);
    }

    public void EnemyDied()
    {
        m_EnemiesLeft--;
        if (m_EnemiesLeft == 0) //cuando no hayan se muestra el panel de valores.
            m_PanelValues.SetActive(true);
    }
}
