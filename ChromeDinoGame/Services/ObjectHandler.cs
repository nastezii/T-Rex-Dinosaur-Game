using ChromeDinoGame.Entities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ChromeDinoGame.Services
{
    class ObjectHandler
    {
        private ObstaclesGenerator _obstaclesGenerator;
        private Canvas _canvas;
        private Random _random;
        private double _backGroundObjSpeed;
        private List<Road> _roads = new List<Road>();
        private List<Obstacle> _obstacles = new List<Obstacle>();
        private List<Cloud> _clouds = new List<Cloud>();
        private DateTime _lastCloudTime = DateTime.Now;
        private DateTime _lastObstacleTime = DateTime.Now;
        private Dino _dino;
        public ObjectHandler(Random random, Canvas canvas, Dino dino, double backGroundObjSpeed, double lineOfGround)
        {
            _canvas = canvas;
            _random = random;
            _dino = dino;
            _backGroundObjSpeed = backGroundObjSpeed;
            _obstaclesGenerator = new ObstaclesGenerator(_random, _backGroundObjSpeed, _canvas.Width, _canvas.Height, lineOfGround);
        }

        public void UpdateEntitites()
        {
            UpdateClouds();
            UpdateRoads();
            UpdateObstacles();
        }

        public void UpdateObstacles()
        {
            foreach (Obstacle obstacle in _obstacles)
            {
                if (!_canvas.Children.Contains(obstacle.Sprite))
                    RenderEntity(obstacle);

                obstacle.MoveObject();

                if (obstacle.IsInWindow())
                    UpdatePosition(obstacle);
                else
                    _obstacles.Remove(obstacle);

                if ((DateTime.Now - _lastObstacleTime).TotalMilliseconds % 3000 == 0)
                {
                    AddObstacle();
                    _lastObstacleTime = DateTime.Now;
                }
            }
        }

        public void UpdateClouds()
        {
            foreach (Cloud cloud in _clouds)
            {
                if (!_canvas.Children.Contains(cloud.Sprite))
                    RenderEntity(cloud);

                cloud.MoveObject();

                if (cloud.IsInWindow())
                    UpdatePosition(cloud);
                else
                    _clouds.Remove(cloud);

                if ((DateTime.Now - _lastCloudTime).TotalMilliseconds % 3000 == 0)
                {
                    AddCloud();
                    _lastCloudTime = DateTime.Now;
                }
            }
        }

        public void UpdateRoads()
        {
            foreach (Road road in _roads)
            {
                if (!_canvas.Children.Contains(road.Sprite))
                    RenderEntity(road);

                road.MoveObject();

                if (road.IsInWindow())
                {
                    UpdatePosition(road);
                }
                else
                {
                    _roads.Remove(road);
                    AddRoad();
                }
            }
        }

        public bool CheckCollision()
        {
            Rect dinoRect = new Rect(_dino.PosX, _dino.PosY, _dino.Width, _dino.Height);

            foreach (var obstacle in _obstacles)
            {
                if (obstacle.CheckCollision(dinoRect, obstacle))
                    return true;
            }
            return false;
        }

        private void RenderEntity(Entity entity)
        {
            _canvas.Children.Add(entity.Sprite);
            Canvas.SetLeft(entity.Sprite, entity.PosX);
            Canvas.SetTop(entity.Sprite, entity.PosY);
        }

        public void AddObstacle() => _obstacles.Add(_obstaclesGenerator.GenerateObstacle());
        public void AddRoad() => _roads.Add(new Road(_random, _backGroundObjSpeed, 0, 305));
        public void AddCloud() => _clouds.Add(new Cloud(_random, _random.Next(200,400), _random.Next(100, 130), _backGroundObjSpeed));

        private void UpdatePosition(Entity entity)
        {
            entity.MoveObject();

            if (entity is Dino)
                Canvas.SetTop(entity.Sprite, entity.PosY);
            else
                Canvas.SetLeft(entity.Sprite, entity.PosX);
        }
    }
}
