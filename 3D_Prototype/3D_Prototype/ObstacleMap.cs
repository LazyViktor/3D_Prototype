using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _3D_Prototype
{
    class ObstacleMap
    {
        public LinkedList<ObstaclePyramid> ObstacleList { get; private set; }
            = new LinkedList<ObstaclePyramid>();

        public int[] MapData { private get; set; }

        float slotSize = 150f;

        float startingPositionX;

        int positionsCounter = 0;   // tracking number of position generated


        public ObstacleMap(float _startingPositionX, int[] _MapData)
        {
            startingPositionX = _startingPositionX;
            MapData = _MapData;

        }
        

        public void FillObstacleQueue(int [] _MapData)
        {
            Model obstacleModel = Singleton.Instance.obstaclePyramidModel;

            // iterating through _MapData
            foreach(int data in _MapData)
            {
                // evaluating _MapData and filling ObstacleList
                switch (data)
                {
                    case 0: // empty Space
                        break;
                    case 1: // Pyramid
                        {
                            ObstacleList.AddLast(new ObstaclePyramid(
                                new Vector3(positionsCounter * slotSize + startingPositionX, 0, 0)
                                , obstacleModel));
                            break;
                        }
                    default:
                        break;
                }

                // tracking number of position generated
                positionsCounter++;
            }
        }

        
        public void Update()
        {
            // filling/refilling ObstacleList
            if (ObstacleList.Count < 10)
            {
                FillObstacleQueue(MapData);
            }

            // remove obstacles which have been passed by player
            // with a buffer of 300            
            if (ObstacleList.First.Value.ObstaclePosition.X
                < Singleton.Instance.playerCube.PlayerPosition.X - 300f)
            {
                ObstacleList.RemoveFirst();
            }

            // updating first 10 obstacles
            int updateCounter = 0;
            foreach (ObstaclePyramid obstacle in ObstacleList)
            {
                if (updateCounter == 10)
                {
                    break;
                }

                obstacle.Update();
                updateCounter++;
            }

        }


        public void Draw()
        {
            // draw next 10 obstacles
            int drawCounter = 0;
            foreach(ObstaclePyramid obstacle in ObstacleList)
            {
                if(drawCounter == 10)
                {
                    break;
                }

                obstacle.Draw();
                drawCounter++;
            }
        }

    }
}
