using UnityEngine;
using System.Collections;

using AcansUtils;
using System.Text;

namespace ACANS
{
	public class PCGBasic : PCG
	{

		public int layout_type;

		//  Layout type
		public int room_type;

		//  Room type
		public int room_max;

		//  Max room size
		public int room_min;

		//  Min room size
		public int room_num;

		//  Number of rooms
		public int room_base;

		public int room_radix;

		public bool room_blocked = false;

		//  If room is blocked
		public int redo = 1000;

		//  Recursion limit
		public ArrayList rooms;

		//  Room arraylist
		public int corridor_num;

		public int corridor_weight;

		public int turning_weight;

		public PCGBasic()
		{

		}



		public void updateParam(int g_width, int g_height, int r_type, int r_min, int r_max, int c_num, int c_weight, int t_weight)
		{
			base.updateParam(g_width, g_height);
			this.room_type = r_type;
			//  Room type
			this.room_min = r_min;
			//  Default 9
			this.room_max = r_max;
			//  Default 16
			this.room_base = (int)(((this.room_min + 1)* 0.5));
			this.room_radix = (int)((((this.room_max - this.room_min)* 0.5)+ 1));
			switch (this.room_type)
			{
				case 0:
					this.room_num = (((pcgrid_width * pcgrid_height)
									  / (int)((Random.Range(this.room_min, this.room_max) * this.room_max)))
								 + 1);
					break;
				case 1:
					this.room_num = (((pcgrid_width * pcgrid_height)
									  / (int)((Random.Range(this.room_min, this.room_max)
												* (this.room_max * 2))))
								 + 1);
					break;
				case 2:
					this.room_num = (((pcgrid_width * pcgrid_height)
									  / (int)((Random.Range(this.room_min, this.room_max)
												* (this.room_min * 0.5))))
								 + 1);
					break;
				default:
					this.room_num = (((pcgrid_width * pcgrid_height)
									  / (int)((Random.Range(this.room_min, this.room_max) * this.room_max)))
								 + 1);
					break;
			}
			this.corridor_num = c_num;
			this.corridor_weight = c_weight;
			this.turning_weight = t_weight;

			Log.info("room_num=", room_num);
		}


		public void generatePCGBasic(byte[,] g) {
			base.generatePCG(g);
			//  Init grid 
			this.initRooms();
			//  Initialize rooms
			//this.initCorridors();
			//  Initialize corridors
		}

		public void initRooms()
		{
			this.rooms = new ArrayList();
			//room_num = 1;
			//  New room arraylist

			//Log.info("room_num",this.room_num);
			for (int n = 0; n < this.room_num; n++)
			{
				this.room_blocked = false;
				//  Unblock
				Room rm = new Room(pcgrid_width, pcgrid_height, this.room_base, this.room_radix, this.corridor_num);
				//  Create new room
				this.room_blocked = this.blockRoom(rm);


				//  Check if room is blocked
				if (this.room_blocked)
				{
					n--;
					//  Remake room
					this.redo--;
					//  Stops if taking too long
					if ((this.redo == 0))
					{
						this.room_num--;
						this.redo = 1000;
						//  Recursion limit
					}

				}
				else {

					//Log.info("room_blocked",this.room_blocked);
					this.rooms.Add(rm);
					//  Create room
					for (int j = rm.room_y1; (j <= rm.room_y2); j++)
					{
						for (int i = rm.room_x1; (i <= rm.room_x2); i++)
						{
							pcgrid[i, j] = 1;
						}

					}

					//  Create room walls
					for (int i = rm.wall_x1; (i <= rm.wall_x2); i++)
					{
						if (pcgrid[i, rm.wall_y1] != 1)
						{
							pcgrid[i, rm.wall_y1] = 2;
						}

						if (pcgrid[i, rm.wall_y2] != 1)
						{
							pcgrid[i, rm.wall_y2] = 2;
						}

					}

					for (int j = rm.wall_y1; (j <= rm.wall_y2); j++)
					{
						if (pcgrid[rm.wall_x1, j] != 1)
						{
							pcgrid[rm.wall_x1, j] = 2;
						}

						if (pcgrid[rm.wall_x2, j] != 1)
						{
							pcgrid[rm.wall_x2, j] = 2;
						}

					}

					//  Place openings
					for (int k = 0; (k < rm.opening_num); k++)
					{
						if ((pcgrid[rm.opening[k, 0], rm.opening[k, 1]] != 1))
						{
							pcgrid[rm.opening[k, 0], rm.opening[k, 1]] = 3;
						}

					}

					/*
					var sb = new StringBuilder(); 
					sb.Append("\n");
					for (int i = 0; i < pcgrid.GetLength(0); i++)
					{
						for (int j = 0; j < pcgrid.GetLength(1); j++)
						{
							sb.Append( pcgrid[i, j] );
						}
						sb.Append("\n");
					}
					Log.info(sb.ToString());
*/
				}

			}

		}

		public bool blockRoom(Room rm)
		{
			//  If outside of grid
			if ((!bounded(rm.wall_x1, rm.wall_y1)
			 || (!bounded(rm.wall_x2, rm.wall_y1)
						|| (!bounded(rm.wall_x1, rm.wall_y2)
						|| !bounded(rm.wall_x2, rm.wall_y2)))))
			{
				return true;
			}

			//  If blocked by another room
			if ((this.room_type != 3))
			{
				for (int i = (rm.wall_x1 - 1); (i
											< (rm.wall_x2 + 1)); i++)
				{
					//  Check upper and lower bound
					if ((bounded(i, (rm.wall_y1 - 1))
					 && !blocked(i, (rm.wall_y1 - 1), 0)))
					{
						return true;
					}

					if ((bounded(i, (rm.wall_y2 + 1))
					 && !blocked(i, (rm.wall_y2 + 1), 0)))
					{
						return true;
					}

				}

				for (int j = (rm.wall_y1 - 1); (j
											< (rm.wall_y2 + 1)); j++)
				{
					//  Check left and right bound
					if ((bounded((rm.wall_x1 - 1), j)
					 && !blocked((rm.wall_x1 - 1), j, 0)))
					{
						return true;
					}

					if ((bounded((rm.wall_x2 + 1), j)
					 && !blocked((rm.wall_x2 + 1), j, 0)))
					{
						return true;
					}

				}

			}

			return false;
		}


		public void initCorridors() {
			if ((this.room_type != 3)) {
				for (int i = 0; (i < this.rooms.Count); i++) {
					//  Go through each room and connect its first opening to the first opening of the next room
					Room rm1 = ((Room)(this.rooms[i]));
					Room rm2;
					if ((i 
					     == (this.rooms.Count - 1))) {
						rm2 = ((Room)(this.rooms[0]));
					}
					else {
						rm2 = ((Room)(this.rooms[(i + 1)]));
					}

					//  If not last room
					//  Connect rooms
					//basicAStar(pcgrid, rm1.opening[0,0], rm1.opening[0,1], rm2.opening[0,0], rm2.opening[0,1], this.corridor_weight, this.turning_weight);
					//  Random tunneling
					for (int j = 1; (j < rm1.opening_num); j++) {
						this.tunnelRandom(rm1.opening[j,0], rm1.opening[j,1], rm1.opening[j,2], 3);
					}

				}

			}
			else {
				//  If complex
				Room rm1 = ((Room)(this.rooms[0]));
				for (int i = 1; (i < this.rooms.Count); i++) {
					//  Go through each room and connect its first opening to the first opening of the first room
					Room rm2 = ((Room)(this.rooms[i]));
					//  Connect rooms
					//basicAStar(pcgrid, rm1.opening[0,0], rm1.opening[0,1], rm2.opening[0,0], rm2.opening[0,1], this.corridor_weight, this.turning_weight);
				}

				//  Random tunneling
				for (int i = 0; (i < this.rooms.Count); i++) {
					Room rm3 = ((Room)(this.rooms[i]));
					for (int j = 1; (j < rm3.opening_num); j++) {
						this.tunnelRandom(rm3.opening[j,0], rm3.opening[j,1], rm3.opening[j,2], 3);
					}

				}

			}

		}

		public void tunnel(int x, int y, int dir)
		{
			if (((pcgrid[x, y] == 2)
			 || (pcgrid[x, y] == 3)))
			{
				pcgrid[x, y] = 3;
			}

			//  If on top of wall or door
			pcgrid[x, y] = 4;
			//  Set cell to corridor
			this.tunnelRandom(x, y, this.shuffleDir(dir, 85), 3);
			//  Randomly choose next cell to go to
		}

		public void tunnelRandom(int x, int y, int dir, int iteration)
		{
			if ((iteration == 0))
			{
				return;
			}

			//  End of recursion iteration
			//  Choose a random direction and check to see if that cell is occupied, if not, head in that direction
			switch (dir)
			{
				case 0:
					if (!this.blockCorridor(x, (y - 1), 0))
					{
						this.tunnel(x, (y - 1), dir);
					}

					//  North
					this.tunnelRandom(x, y, this.shuffleDir(dir, 0), (iteration - 1));
					//  Try again
					break;
				case 1:
					if (!this.blockCorridor((x + 1), y, 1))
					{
						this.tunnel((x + 1), y, dir);
					}

					//  East
					this.tunnelRandom(x, y, this.shuffleDir(dir, 0), (iteration - 1));
					//  Try again
					break;
				case 2:
					if (!this.blockCorridor(x, (y + 1), 0))
					{
						this.tunnel(x, (y + 1), dir);
					}

					//  South
					this.tunnelRandom(x, y, this.shuffleDir(dir, 0), (iteration - 1));
					//  Try again
					break;
				case 3:
					if (!this.blockCorridor((x - 1), y, 1))
					{
						this.tunnel((x - 1), y, dir);
					}

					//  West
					this.tunnelRandom(x, y, this.shuffleDir(dir, 0), (iteration - 1));
					//  Try again
					break;
			}
		}


		public int shuffleDir(int dir, int prob)
		{
			//  Randomly choose direction based on probability
			if (((Random.Range(0, 100)) > (100 - prob)))
			{
				return dir;
				//  Stay same direction
			}
			else {
				//  Change direction
				switch (dir)
				{
					case 0:
						if (((Random.Range(0, 100)) < 50))
						{
							return 1;
						}

						//  East
						if (Random.Range(0, 100) >= 50)
						{
							return 3;
						}

						//  West
						break;
					case 1:
						if (Random.Range(0, 100) < 50)
						{
							return 0;
						}

						//  North
						if (Random.Range(0, 100) >= 50)
						{
							return 2;
						}

						//  South
						break;
					case 2:
						if (Random.Range(0, 100) < 50)
						{
							return 1;
						}

						//  East
						if (Random.Range(0, 100) >= 50)
						{
							return 3;
						}

						//  West
						break;
					case 3:
						if (Random.Range(0, 100) < 50)
						{
							return 0;
						}

						//  North
						if (Random.Range(0, 100) >= 50)
						{
							return 2;
						}

						//  South
						break;
				}
			}

			return dir;
		}

		public bool blockCorridor(int x, int y, int orientation)
		{
			if (!bounded(x, y))
			{
				return true;
			}

			//  If outside of grid
			//  Check if current cell is available as corridor based on previous corridor cell location
			switch (orientation)
			{
				case 0:
					if ((blocked(x, y, 1)
					|| ((blocked((x - 1), y, 4) && blocked((x - 1), (y + 1), 4))
					|| ((blocked((x - 1), y, 4) && blocked((x - 1), (y - 1), 4))
					|| ((blocked((x + 1), y, 4) && blocked((x + 1), (y + 1), 4))
					|| ((blocked((x + 1), y, 4) && blocked((x + 1), (y - 1), 4))
					|| ((blocked(x, y, 2) || blocked(x, y, 3))
					&& (((blocked(x, (y - 1), 2) || blocked(x, (y - 1), 3))
					&& (blocked((x + 1), y, 2) || blocked((x + 1), y, 2)))
					|| (((blocked(x, (y - 1), 2) || blocked(x, (y - 1), 3))
					&& (blocked((x - 1), y, 2) || blocked((x - 1), y, 2)))
					|| (((blocked(x, (y + 1), 2) || blocked(x, (y + 1), 3))
					&& (blocked((x + 1), y, 2) || blocked((x + 1), y, 2)))
					|| ((blocked(x, (y + 1), 2) || blocked(x, (y + 1), 3))
					&& (blocked((x - 1), y, 2) || blocked((x - 1), y, 2)))))))))))))
					{
						return true;
					}

					break;
				case 1:
					if ((blocked(x, y, 1)
					|| ((blocked(x, (y - 1), 4) && blocked((x - 1), (y + 1), 4))
					|| ((blocked(x, (y - 1), 4) && blocked((x - 1), (y - 1), 4))
					|| ((blocked(x, (y + 1), 4) && blocked((x + 1), (y + 1), 4))
					|| ((blocked(x, (y + 1), 4) && blocked((x + 1), (y - 1), 4))
					|| ((blocked(x, y, 2) || blocked(x, y, 3))
					&& (((blocked(x, (y - 1), 2) || blocked(x, (y - 1), 3))
					&& (blocked((x + 1), y, 2) || blocked((x + 1), y, 2)))
					|| (((blocked(x, (y - 1), 2) || blocked(x, (y - 1), 3))
					&& (blocked((x - 1), y, 2) || blocked((x - 1), y, 2)))
					|| (((blocked(x, (y + 1), 2) || blocked(x, (y + 1), 3))
					&& (blocked((x + 1), y, 2) || blocked((x + 1), y, 2)))
					|| ((blocked(x, (y + 1), 2) || blocked(x, (y + 1), 3))
					&& (blocked((x - 1), y, 2) || blocked((x - 1), y, 2)))))))))))))
					{
						return true;
					}

					break;
			}
			return false;
		}


	}

}