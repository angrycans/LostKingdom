using System.Collections;
using UnityEngine;
using AcansUtils;

namespace ACANS
{
	public class Room
	{

		public int pcgrid_width;

		public int pcgrid_height;

		public int room_x;

		public int room_y;

		public int room_width;

		public int room_height;

		public int room_x1;

		public int room_x2;

		public int room_y1;

		public int room_y2;

		public int wall_x1;

		public int wall_x2;

		public int wall_y1;

		public int wall_y2;

		public int[,] opening;

		//  Doors
		public int opening_num;

		//  Number of doors
		public Room(int w, int h, int _base, int radix, int c_num)
		{

			//Random.seed = 1;
			this.pcgrid_width = w;
			this.pcgrid_height = h;
			this.room_width = Random.Range(0,radix) + _base;
			this.room_height = Random.Range(0,radix) + _base;
			this.room_x1 = Random.Range(0, (this.pcgrid_width - this.room_width));
			this.room_y1 = Random.Range(0, (this.pcgrid_height - this.room_height));
			this.room_x2 = this.room_x1+ (this.room_width - 1);
			this.room_y2 = this.room_y1+ (this.room_height - 1);
			this.room_x = this.room_x1 + (int)((this.room_x2 - this.room_x1) * 0.5);
			this.room_y = this.room_y1 + (int)((this.room_y2 - this.room_y1) * 0.5);
			this.wall_x1 = this.room_x1 - 1;
			this.wall_x2 = this.room_x2 + 1;
			this.wall_y1 = this.room_y1 - 1;
			this.wall_y2 = this.room_y2 + 1;
			this.opening_num = Random.Range(1, (c_num + 1));
			//  Open up doorway
			this.opening = new int[this.opening_num,3];
		



			Log.info(pcgrid_width,pcgrid_height,room_width,room_height,room_x1,room_y2,room_x2,room_y2,room_x,room_y,wall_x1,wall_x2,wall_y1,wall_y2,opening_num);
		
			initDoors();
		}

		public void initDoors()
		{
			int count = this.opening_num;
			while ((count != 0))
			{
				this.opening[(count - 1),2] = Random.Range(0, 4);
				//  Door orientation
				//  Make sure door is not on corner or facing wall
				switch (this.opening[(count - 1),2])
				{
					case 0:
						//  North wall
						int x1 = Random.Range(this.wall_x1, this.wall_x2);
						if (((x1 != this.wall_x1)
							 && ((x1 != this.wall_x2)
										&& (this.wall_y1 >= 1))))
						{
							this.opening[(count - 1),0] = x1;
							this.opening[(count - 1),1] = this.wall_y1;
							this.opening[(count - 1),2] = 0;
							count--;
						}

						break;
					case 1:
						//  East wall
						int y2 = Random.Range(this.wall_y1, this.wall_y2);
						if (((y2 != this.wall_y1)
							 && ((y2 != this.wall_y2)
										&& (this.wall_x2
										< (this.pcgrid_width - 1)))))
						{
							this.opening[(count - 1),0] = this.wall_x2;
							this.opening[(count - 1),1] = y2;
							this.opening[(count - 1),2] = 1;
							count--;
						}

						break;
					case 2:
						//  South wall
						int x2 = Random.Range(this.wall_x1, this.wall_x2);
						if (((x2 != this.wall_x1)
							 && ((x2 != this.wall_x2)
										&& (this.wall_y2
										< (this.pcgrid_height - 1)))))
						{
							this.opening[(count - 1),0] = x2;
							this.opening[(count - 1),1] = this.wall_y2;
							this.opening[(count - 1),2] = 2;
							count--;
						}

						break;
					case 3:
						//  West wall
						int y1 = Random.Range(this.wall_y1, this.wall_y2);
						if (((y1 != this.wall_y1)
							 && ((y1 != this.wall_y2)
										&& (this.wall_x1 >= 1))))
						{
							this.opening[(count - 1),0] = this.wall_x1;
							this.opening[(count - 1),1] = y1;
							this.opening[(count - 1),2] = 3;
							count--;
						}

						break;
				}
			}

		}
	}
}