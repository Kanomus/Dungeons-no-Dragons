using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.Scripting;

public class PathNode
{
    private Grid grid = GameObject.Find("Grid").GetComponent<Grid>();
    private int x,y;
    private int gcost, hcost, fcost;
    public PathNode parentNode;
    public PathNode(Grid grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;

    }
    
}
