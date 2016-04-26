using System;

//http://jsfiddle.net/bigbadwaffle/YeazH/
namespace  Acans
{

	public class Room
	{
		public Room()
		{
		}

	
	}

	public class Map
	{

		Room rooms;
		public Map()
		{
		}

		public Boolean DoesCollide(Room room, int ignore)
		{
			for (var i = 0; i < this.rooms.length; i++) {
				if (i == ignore) continue;
				var check = rooms[i];
				if (!((room.x + room.w < check.x) || (room.x > check.x + check.w) || (room.y + room.h < check.y) || (room.y > check.y + check.h))) return true;
			}

			return false;
		}



	}



}

