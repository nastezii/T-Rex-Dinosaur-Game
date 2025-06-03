using ChromeDinoGame.Entities;
using ChromeDinoGame.Globals;

namespace ChromeDinoGame.Services
{
    class EntityHandler
    {
        private Dino _dino = Dino.Instance;
        private const double LineOfGround = Characteristics.LineOfGround;
        private const double InitialSpeed = Characteristics.SpeedOfEntities;
        private const double SpeedInc = Characteristics.SpeedInc;
        private double _currentSpeed = InitialSpeed;

        private Action _onCollisionCallback;
        private ObstacleSpawner _obstaclesSpawner = new ObstacleSpawner();
        private List<Entity> _entities = new List<Entity>();

        public EntityHandler(Action onCollisionCallback)
        {
            _onCollisionCallback = onCollisionCallback;
        }

        public void InitializeEntities()
        {
            _dino.RenderEntity();
            _entities.Add(new Road(_currentSpeed, 0, LineOfGround / 2));
            _entities.Add(new Cloud(_currentSpeed / 10));
        }

        public void SetReplayCharacteristics()
        {
            _currentSpeed = InitialSpeed;
            _entities.Clear();
        }

        public void UpdateEntities()
        {
            _currentSpeed += SpeedInc;

            RefreshEntities();
        }

        private void RefreshEntities() 
        {
            _dino.MoveEntity();

            for (int i = _entities.Count - 1; i >= 0; i --)
            { 
                if (_entities[i].IsInWindow())
                {
                    _entities[i].MoveEntity();
                }
                else
                {
                    _entities[i].RemoveEntity();
                    _entities.RemoveAt(i);
                }

                Road firstRoad = _entities.OfType<Road>().LastOrDefault();
                Cloud lastCloud = _entities.OfType<Cloud>().LastOrDefault();
                Obstacle lastObstacle = _entities.OfType<Obstacle>().LastOrDefault();

                if (firstRoad.IsNearWindowEnd())
                {
                    _entities.Add(new Road(_currentSpeed, GlobalCanvas.GameArea.Width, LineOfGround));
                }

                if (lastCloud == null || lastCloud.PosX < GlobalRandom.Instance.Next(150, 300))
                {
                    _entities.Add(new Cloud(_currentSpeed / 10));
                }

                if (lastObstacle == null || lastObstacle.PosX < GlobalRandom.Instance.Next(30, 75))
                {
                    _entities.Add(_obstaclesSpawner.GenerateObstacle(LineOfGround, _currentSpeed));
                }
            }
        }

        private void CheckCollision(Obstacle obstacle)
        {
            if (obstacle.CheckCollision())
            {
                _onCollisionCallback.Invoke();
            }
        }
    }
}
