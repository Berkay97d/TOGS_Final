using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : IEnumerable<Vector3>
{
    private readonly List<Vector3> m_Positions;


    public Grid(Vector3 origin, int x, int y, float size)
    {
        m_Positions = new List<Vector3>();
        for (int ix = 0; ix < x; ix++)
        {
            for (int iy = 0; iy < y; iy++)
            {
                var position = origin + new Vector3(ix * size, 0f, iy * size);
                m_Positions.Add(position);
            }
        }
    }

    public IEnumerator<Vector3> GetEnumerator()
    {
        return m_Positions.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
