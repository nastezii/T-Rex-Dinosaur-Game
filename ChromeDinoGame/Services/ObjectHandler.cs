using ChromeDinoGame.Entities;
using System.Windows;
using System.Windows.Controls;

namespace ChromeDinoGame.Services
{
    class ObjectHandler
    {
        private Canvas _canvas;
        private Random _random;
        private double _SpeedOfEntities;
        private Dino _dino;
        private ObstaclesGenerator _obstaclesGenerator;
        private List<Road> _roads = new List<Road>();
        private List<Obstacle> _obstacles = new List<Obstacle>();
        private List<Cloud> _clouds = new List<Cloud>();

        public ObjectHandler(Random random, Canvas canvas, double speedOfEntities, double lineOfGround)
        {
            _canvas = canvas;
            _random = random;
            _SpeedOfEntities = speedOfEntities;
            _dino = new Dino(lineOfGround, canvas.Width, canvas.Height);
            _obstaclesGenerator = new ObstaclesGenerator(_random, _SpeedOfEntities, _canvas.Width, _canvas.Height, lineOfGround);
        }
        public void InitializeStartWindow()
        {
            AddRoad(0);
            AddRoad();
            AddCloud(_random.Next(300, 500));
        }

        public void UpdateEntitites()
        {
            UpdateClouds();
            UpdateRoads();
            UpdateObstacles();
        }

        public bool CheckCollision()
        {
            Rect dinoRect = new Rect(_dino.PosX, _dino.PosY, _dino.Width, _dino.Height);

            foreach (Obstacle obstacle in _obstacles)
            {
                if (obstacle.CheckCollision(dinoRect, obstacle))
                    return true;
            }
            return false;
        }

        private void UpdateObstacles()
        {
            for (int i = _obstacles.Count - 1; i >= 0; i--)
            {
                if (!_canvas.Children.Contains(_obstacles[i].Sprite))
                    RenderEntity(_obstacles[i]);

                if (_obstacles[i].IsInWindow(_canvas.Width))
                    UpdatePosition(_obstacles[i]);
                else
                {
                    _canvas.Children.Remove(_obstacles[i].Sprite);
                    _obstacles.RemoveAt(i);
                }
            }

            if (_obstacles.Count == 0 || _obstacles[_obstacles.Count - 1].PosX < _random.Next(150, 200))
            {
                AddObstacle();
            }
        }

        private void UpdateClouds()
        {
            for (int i = _clouds.Count - 1; i >= 0; i--)
            {
                if (!_canvas.Children.Contains(_clouds[i].Sprite))
                    RenderEntity(_clouds[i]);

                if (_clouds[i].IsInWindow(_canvas.Width))
                    UpdatePosition(_clouds[i]);
                else
                {
                    _canvas.Children.Remove(_clouds[i].Sprite);
                    _clouds.RemoveAt(i);
                }
            }

            if (_clouds[_clouds.Count - 1].PosX < _random.Next(100, 250))
            {
                AddCloud();
            }
        }

        private void UpdateRoads()
        {
            for (int i = _roads.Count - 1; i >= 0; i--)
            {
                if (!_canvas.Children.Contains(_roads[i].Sprite))
                    RenderEntity(_roads[i]);

                if (_roads[i].IsInWindow(_canvas.Width))
                {
                    UpdatePosition(_roads[i]);
                }
                else
                {
                    _canvas.Children.Remove(_roads[i].Sprite);
                    _roads.RemoveAt(i);
                    AddRoad();
                }
            }
        }

        private void AddObstacle() => _obstacles.Add(_obstaclesGenerator.GenerateObstacle());
        private void AddRoad(double x = 600) => _roads.Add(new Road(_random, _SpeedOfEntities, x, 290));
        private void AddCloud(double x = 600) => _clouds.Add(new Cloud(x, _random.Next(10, 50), _SpeedOfEntities / 10));

        private void RenderEntity(Entity entity)
        {
            _canvas.Children.Add(entity.Sprite);
            Canvas.SetLeft(entity.Sprite, entity.PosX);
            Canvas.SetTop(entity.Sprite, entity.PosY);
        }   

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
