using UnityEngine;
using System.Collections;

public class ShowMap : MonoBehaviour {

	Grid grid;
	AStarTest astar;

	public Texture2D[] tex ;

	public static int COL = (int)Mathf.Floor(Screen.width/GRIDWIDTH);
	public static int ROW = (int)Mathf.Floor(Screen.height/GRIDWIDTH);
	public static float OffsetX = (Screen.width - COL*GRIDWIDTH)>>1;
	public static float OffsetY = (Screen.height - ROW*GRIDWIDTH)>>1;
	public const int GRIDWIDTH = 50;

	// Use this for initialization
	void Start () {

		grid = new Grid(COL, ROW);
		grid.SetStartNode(0,0);
		grid.SetEndNode(COL - 1, ROW - 1);

		astar = new AStarTest();
		astar.FindPath(grid);
	
	}



	void OnGUI()
	{
		if(grid == null) return;
		for (int i = 0; i < grid.Col; i++) 
		{
			for (int j = 0; j < grid.Row; j++) {
				if(grid.GetNode(i,j).walkable)
				{
					GUI.DrawTexture(new Rect(i*GRIDWIDTH + OffsetX, j*GRIDWIDTH + OffsetY, GRIDWIDTH, GRIDWIDTH), tex[0]);

				}
				else
				{
					GUI.DrawTexture(new Rect(i*GRIDWIDTH + OffsetX, j*GRIDWIDTH + OffsetY, GRIDWIDTH, GRIDWIDTH), tex[1]);
				}

			}
				
		}


		if(astar.ShowPath)
		{
			for(int i = 0; i<astar.path.Count;i++)
			{
				GUI.DrawTexture(new Rect(astar.path[i].x*GRIDWIDTH + OffsetX, astar.path[i].y*GRIDWIDTH + OffsetY, GRIDWIDTH, GRIDWIDTH),tex[2]);			
			}

			GUI.DrawTexture(new Rect(grid.StartNode.x*GRIDWIDTH + OffsetX, grid.StartNode.y*GRIDWIDTH + OffsetY, GRIDWIDTH, GRIDWIDTH), tex[3]);
			GUI.DrawTexture(new Rect(grid.EndNode.x*GRIDWIDTH + OffsetX, grid.EndNode.y*GRIDWIDTH + OffsetY, GRIDWIDTH, GRIDWIDTH), tex[3]);

		}



	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(0))
		{
			Vector2 pos = Input.mousePosition;
			int x = (int)Mathf.Floor(pos.x / GRIDWIDTH);
			int y = (int)Mathf.Floor((Screen.height - pos.y)/GRIDWIDTH);
			if(x<0||x>grid.Col ||y<0 || y>grid.Row)	return;
			grid.SetWalkable(x,y, !grid.GetNode(x,y).walkable);
			astar.FindPath(grid);

		}





	
	}
}
