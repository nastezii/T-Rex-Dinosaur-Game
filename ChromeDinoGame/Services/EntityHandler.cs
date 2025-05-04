using ChromeDinoGame.Entities;
using System.Windows.Controls;

namespace ChromeDinoGame.Services
{
    class EntityHandler
    {
        private Canvas _canvas;
        private Random _random;
        private double _speedOfEntities;
        private double _lineOfGround;
        private double _speedInc;
        private ObstacleSpawner _obstaclesSpawner;
        private Action _onCollisionCallback;
        private List<Obstacle> _obstacles = new List<Obstacle>();
        private List<Cloud> _clouds = new List<Cloud>();
        private List<Road> _roads = new List<Road>();

        public Dino Dino { get; private set; }

        public EntityHandler(Random random, Canvas canvas, Action onCollisionCallback, double speedOfEntities, double lineOfGround, double sppedInc)
        {
            _canvas = canvas;
            _random = random;
            _onCollisionCallback = onCollisionCallback;
            _lineOfGround = lineOfGround;
            _speedOfEntities = speedOfEntities;
            _speedInc = sppedInc;

            _obstaclesSpawner = new ObstacleSpawner();
            Dino = new Dino(_canvas, _lineOfGround, _speedOfEntities);
        }

        public void InitializeStartWindow()
        {
            Dino.RenderEntity();
            _roads.Add(new Road(_canvas, _random, _speedOfEntities, 0, _lineOfGround / 1.5));
            _roads[0].RenderEntity();
            _roads.Add(new Road(_canvas, _random, _speedOfEntities, _canvas.Width, _lineOfGround / 1.5));
            _roads[1].RenderEntity();
            _clouds.Add(new Cloud(_canvas, _random.Next(200,400), _random.Next(180, 300), _speedOfEntities / 10));
            _clouds[0].RenderEntity();
        }

        public void UpdateEntities()
        {
            _speedOfEntities += _speedInc;

            Dino.MoveEntity();  
            UpdateObstacles();
            UpdateClouds();
            UpdateRoads();
        }

        private void CheckCollision(Obstacle obstacle)
        {
            if (obstacle.CheckCollision(Dino.CollisionBox, obstacle.CollisionBox))
            {
                _onCollisionCallback.Invoke();
            }
        }

        private void UpdateObstacles()
        {
            for (int i = _obstacles.Count - 1; i >= 0; i--)
            {
                if (_obstacles[i].IsInWindow())
                {
                    _obstacles[i].MoveEntity();
                    CheckCollision(_obstacles[i]);
                }
                else
                {
                    _obstacles[i].RemoveEntity();
                    _obstacles.RemoveAt(i);
                }
            }

            if (_obstacles.Count == 0 || _obstacles[_obstacles.Count - 1].PosX < _random.Next(50, 75))
            {
                _obstacles.Add(_obstaclesSpawner.GenerateObstacle(_canvas, _lineOfGround, _speedOfEntities));
                _obstacles[_obstacles.Count - 1].RenderEntity();
            }
        }

        private void UpdateClouds()
        {
            for (int i = _clouds.Count - 1; i >= 0; i--)
            {
                if (_clouds[i].IsInWindow())
                {
                    _clouds[i].MoveEntity();
                }
                else
                {
                    _clouds[i].RemoveEntity();
                    _clouds.RemoveAt(i);
                }
            }

            if (_clouds[_clouds.Count - 1].PosX < _random.Next(150, 300))
            {
                _clouds.Add(new Cloud(_canvas, _canvas.Width, _random.Next(180, 300), _speedOfEntities / 10));
                _clouds[_clouds.Count - 1].RenderEntity();
            }
        }

        private void UpdateRoads()
        {
            for (int i = _roads.Count - 1; i >= 0; i--)
            {
                if (_roads[i].IsInWindow())
                {
                    _roads[i].MoveEntity();
                }
                else
                {
                    _roads[i].RemoveEntity();
                    _roads.RemoveAt(i);
                    _roads.Add(new Road(_canvas, _random, _speedOfEntities, _canvas.ActualWidth, _lineOfGround / 1.5));
                    _roads[_roads.Count - 1].RenderEntity();
                }
            }
        }
    }
}
