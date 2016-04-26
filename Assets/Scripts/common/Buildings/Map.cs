
using System.Collections.Generic;
using UnityEngine;

//http://jsfiddle.net/bigbadwaffle/YeazH/
namespace Acans
{
    public struct IntVector2
    {
        public int x, y;

        public IntVector2(int _x, int _y)
        {
            x = _x; y = _y;
        }
    }

    public class Room
    {

        public int x;
        public int w;
        public int y;
        public int h;
        public Room()
        {
        }


    }

    public class Map
    {

        List<Room> rooms;

        int[,] map;
        int map_size = 64;

        public Map()
        {

        }



        public void Generate()
        {
            rooms = new List<Room>();

            for (var x = 0; x < this.map_size; x++)
            {

                for (var y = 0; y < this.map_size; y++)
                {
                    this.map[x, y] = 0;
                }
            }

            var room_count = Random.Range(10, 20);
            var min_size = 5;
            var max_size = 15;

            for (var i = 0; i < room_count; i++)
            {
                var room = new Room();

                room.x = Random.Range(1, this.map_size - max_size - 1);
                room.y = Random.Range(1, this.map_size - max_size - 1);
                room.w = Random.Range(min_size, max_size);
                room.h = Random.Range(min_size, max_size);

                if (this.DoesCollide(room))
                {
                    i--;
                    continue;
                }
                room.w--;
                room.h--;

                this.rooms.Add(room);
            }

            this.SquashRooms();

            for (int i = 0; i < room_count; i++)
            {
                var roomA = this.rooms[i];
                var roomB = this.FindClosestRoom(roomA);

                var pointA = new IntVector2(Random.Range(roomA.x, roomA.x + roomA.w),
                        Random.Range(roomA.y, roomA.y + roomA.h)
                        );
                var pointB = new IntVector2(
                    Random.Range(roomB.x, roomB.x + roomB.w),
                    Random.Range(roomB.y, roomB.y + roomB.h)
                  );

                while ((pointB.x != pointA.x) || (pointB.y != pointA.y))
                {
                    if (pointB.x != pointA.x)
                    {
                        if (pointB.x > pointA.x)
                        {
                            pointB.x--;
                        }
                        else {
                            pointB.x++;
                        }
                    }
                    else if (pointB.y != pointA.y)
                    {
                        if (pointB.y > pointA.y)
                        {
                            pointB.y--;
                        }
                        else {
                            pointB.y++;
                        }
                    }

                    this.map[pointB.x, pointB.y] = 1;
                }

                for ( i = 0; i < room_count; i++)
                {
                    var room = this.rooms[i];
                    for (var x = room.x; x < room.x + room.w; x++)
                    {
                        for (var y = room.y; y < room.y + room.h; y++)
                        {
                            this.map[x, y] = 1;
                        }
                    }
                }

                for (var x = 0; x < this.map_size; x++)
                {
                    for (var y = 0; y < this.map_size; y++)
                    {
                        if (this.map[x, y] == 1)
                        {
                            for (var xx = x - 1; xx <= x + 1; xx++)
                            {
                                for (var yy = y - 1; yy <= y + 1; yy++)
                                {
                                    if (this.map[xx, yy] == 0) this.map[xx, yy] = 2;
                                }
                            }
                        }
                    }
                }

            }




        }



        public bool DoesCollide(Room room, int ignore = -1)
        {
            for (var i = 0; i < this.rooms.Count; i++)
            {
                if (i == ignore) continue;
                var check = rooms[i];
                if (!((room.x + room.w < check.x) || (room.x > check.x + check.w) || (room.y + room.h < check.y) || (room.y > check.y + check.h))) return true;
            }

            return false;
        }


        public void SquashRooms()
        {
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < this.rooms.Count; j++)
                {
                    var room = this.rooms[j];
                    while (true)
                    {
                        var old_position = new IntVector2(room.x, room.y);
                        if (room.x > 1) room.x--;
                        if (room.y > 1) room.y--;
                        if ((room.x == 1) && (room.y == 1)) break;
                        if (this.DoesCollide(room, j))
                        {
                            room.x = old_position.x;
                            room.y = old_position.y;
                            break;
                        }
                    }
                }
            }
        }

        public Room FindClosestRoom(Room room)
        {
            var mid = new IntVector2(
              room.x + (room.w / 2), room.y + (room.h / 2)
            );
            Room closest = null;
            var closest_distance = 1000;
            for (var i = 0; i < this.rooms.Count; i++)
            {
                var check = this.rooms[i];
                if (check == room) continue;
                var check_mid = new IntVector2(
                  check.x + (check.w / 2),
                  check.y + (check.h / 2)
                );
                var distance = Mathf.Min(Mathf.Abs(mid.x - check_mid.x) - (room.w / 2) - (check.w / 2), Mathf.Abs(mid.y - check_mid.y) - (room.h / 2) - (check.h / 2));
                if (distance < closest_distance)
                {
                    closest_distance = distance;
                    closest = check;
                }
            }
            return closest;
        }


    }



}

