using UnityEngine;
using System.Collections;

using AcansUtils;
namespace ACANS
{

	public class PCG
	{

		public byte[,] pcgrid;

		//  Grid array
		public int pcgrid_width;

		//  Grid width
		public int pcgrid_height;

		//  Grid height
		public PCG()
		{

		}

		public void updateParam(int g_width, int g_height)
		{
			this.pcgrid_width = g_width;
			//  Get grid length
			this.pcgrid_height = g_height;
			//  Get grid width
		}

		public void generatePCG(byte[,] g)
		{
			this.pcgrid = g;
			//  Copy grid
		}

		public bool bounded(int x, int y)
		{
			//  Check if cell is inside grid
			if (((x < 0)
				 || ((x >= this.pcgrid_width)
							|| ((y < 0)
							|| (y >= this.pcgrid_height)))))
			{
				return false;
			}

			return true;
		}

		public bool blocked(int x, int y, int type)
		{
			//  Check if cell is occupied
			if ((this.bounded(x, y)
				 && (this.pcgrid[x,y] == type)))
			{
				return true;
			}

			return false;
		}
	}
}