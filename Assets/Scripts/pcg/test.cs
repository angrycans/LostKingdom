using System;
using System.Text;
using AcansUtils;

namespace ACANS{
	public class test
	{

		 
		public test()
		{

			var grid_width = 40;
			var grid_height = 30;

		    byte[,] grid;
			grid = new byte[grid_width,grid_height]; 

			for (int j = 0; j < grid_height; j++) {
				for (int i = 0; i < grid_width; i++) {
					grid[i,j] = 0; // Initialize all cell as empty
				}
			}

			PCGBasic pcg_b = new PCGBasic();
			pcg_b.updateParam(40, 30, 0 ,9, 16 ,1,4 ,4 );
			pcg_b.generatePCGBasic(grid);


			var sb = new StringBuilder(); 
			sb.Append("\n");
			for (int i = 0; i < grid.GetLength(0); i++)
			{
				for (int j = 0; j < grid.GetLength(1); j++)
				{
					sb.Append( grid[i, j] );
				}
				sb.Append("\n");
			}
			Log.info(sb.ToString());
			//Room r = new Room(40,30,5,4,2);




		}
	}
}

