using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStarTest{

	private bool showPath = false;

	public List<Node> openList;
	public List<Node> closeList;
	public List<Node> path;

	private Grid grid;
	private Node startNode;
	private Node endNode;

	private float straightCost = 1.0f;
	private float diagonalCost = Mathf.Sqrt(2);
	private int col;
	private int row;

	public bool FindPath(Grid grid)
	{
		Init(grid);
		return Search();
	}

	private void Init(Grid grid)
	{
		this.grid = grid;
		startNode = grid.StartNode;
		endNode = grid.EndNode;
		col = grid.Col;
		row = grid.Row;

		openList = new List<Node>();
		closeList = new List<Node>();
		path = new List<Node>();

		startNode.g = 0;
		startNode.h = Manhattan(startNode);
		startNode.f = startNode.g + startNode.h;
	}


	private bool Search()
	{
		Node node = startNode;

		while(node != endNode)
		{
			int leftX = Mathf.Max(0, node.x - 1);
			int rightX = Mathf.Min(col-1, node.x + 1); 
			int upY = Mathf.Max(0, node.y - 1);
			int downY = Mathf.Min(row-1, node.y + 1);

			for (int i = leftX; i <= rightX; i++) 
			{
				for (int j = upY; j <= downY; j++) 
				{
					Node tmp = grid.GetNode(i,j);
					if(tmp == node || !tmp.walkable ||!grid.GetNode(node.x,tmp.y).walkable||!grid.GetNode(tmp.x,node.y).walkable) continue;

					float cost = straightCost;
					if( !(tmp.x == node.x || tmp.y == node.y))
					{
						cost = diagonalCost;
					}

					float g = node.g + cost;
					float h = Manhattan(tmp);
					float f = g + h;

					if(isOpen(tmp) || isClose(tmp))
					{
						if(tmp.f > f)
						{
							tmp.f = f;
							tmp.g = g;
							tmp.h = h;
							tmp.parent = node;
						}

					}
					else
					{
						tmp.f = f;
						tmp.g = g;
						tmp.h = h;
						tmp.parent = node;
						openList.Add(tmp);
						
					}
				}				
			}


			closeList.Add(node);

			if(openList.Count == 0)
			{
				showPath = false;
				Debug.Log("No path ! ! !");
				return false;
			}
			else
			{
				showPath = true;
			}

			SortOpenList();
			node = openList[0];
			openList.RemoveAt(0);

		}

		BuildPath();
		return true;


	}





	private void SortOpenList()
	{
		for (int i = 0; i < openList.Count - 1; i++) 
		{
			for (int j = 0; j < openList.Count-1-i; j++) 
			{
				if(openList[j].f > openList[j+1].f)
				{
					Node tmp = openList[j];
					openList[j] = openList[j+1];
					openList[j+1] = tmp;
				}
			}				
		}

	}




	private bool isOpen(Node node)
	{
		return openList.Contains(node);
	}

	private bool isClose(Node node)
	{
		return closeList.Contains(node);
	}



	private void BuildPath()
	{
		path = new List<Node>();
		Node node = endNode;
		path.Add (node);
		while(node != startNode)
		{
			node = node.parent;
			path.Add(node);

		}

		path.Reverse();

	}


	public bool ShowPath
	{
		get{return showPath;}
	}



	private float Manhattan(Node node)
	{
		return Mathf.Abs(node.x - endNode.x)*straightCost + Mathf.Abs(node.y - endNode.y)*straightCost;
	}

}







public class Node
{
	public int x;
	public int y;

	public float f;
	public float g;
	public float h;

	public bool walkable = true;
	public Node parent;

	public Node(int x, int y)
	{
		this.x = x;
		this.y = y;
	}


}


public class Grid
{
	private Node startNode;
	private Node endNode;
	private Node[,] nodes;

	private int col;
	private int row;

	public Grid(int col, int row)
	{
		this.col = col;
		this.row = row;

		nodes = new Node[col, row];

		for(int i = 0; i < col; i++)
		{
			for (int j = 0; j < row; j++) 
			{
				nodes[i,j] = new Node(i,j);
			}
		}
	}

	public void SetWalkable(int x, int y, bool value)
	{
		nodes[x,y].walkable = value;
	}

	public void SetStartNode(int x, int y)
	{
		startNode = nodes[x,y];
	}

	public void SetEndNode(int x, int y)
	{
		endNode = nodes[x,y];
	}

	public Node GetNode(int x, int y)
	{
		return nodes[x,y];
	}

	public Node StartNode
	{
		get{return startNode;}
	}

	
	public Node EndNode
	{
		get{return endNode;}
	}

	public int Col
	{
		get{return col;}
	}

	public int Row
	{
		get{return row;}
	}





}