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
        
        float slotSize = 200f;

        float startingPositionX;

        int positionsCounter = 0;   // tracking number of position generated


        public ObstacleMap(float _startingPositionX)
        {
            startingPositionX = _startingPositionX;

        }

        public void Reset()
        {
            // call on Death
            ObstacleList.Clear();
            positionsCounter = 0;
        }


        int[] SelectRandomMapData()
        {
            // random generator
            Random randomGen = new Random();

            // create number 0 >= num < 5
            int randomNum = randomGen.Next(5);


            // selcting acording MapData from Singleton
            int[] selectedMapData = { };

            switch (randomNum)
            {
                case 0:
                    selectedMapData = Singleton.Instance.MapDataA;
                    break;
                case 1:
                    selectedMapData = Singleton.Instance.MapDataB;
                    break;
                case 2:
                    selectedMapData = Singleton.Instance.MapDataC;
                    break;
                case 3:
                    selectedMapData = Singleton.Instance.MapDataD;
                    break;
                case 4:
                    selectedMapData = Singleton.Instance.MapDataE;
                    break;
                default:
                    break;
            }

            return selectedMapData;
        }
        

        void FillObstacleQueue(int [] _MapData)
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
                FillObstacleQueue(SelectRandomMapData());
            }

            // remove obstacles which have been passed by player
            // with a buffer of 300 and addScore to HighScore      
            if (ObstacleList.First.Value.ObstaclePosition.X
                < Singleton.Instance.playerCube.PlayerPosition.X - 300f)
            {
                ObstacleList.RemoveFirst();

                // add score for one passed obstacle
                Singleton.Instance.highScoreSystem.AddPoints();
            }

            // updating up to 10 obstacles
            for(int updateCounter = 0; updateCounter < 10; updateCounter++)
            {
                if (updateCounter >= ObstacleList.Count)
                    break;
                ObstacleList.ElementAt<ObstaclePyramid>(updateCounter).Update();
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
