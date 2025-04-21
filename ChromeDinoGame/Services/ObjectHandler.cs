using ChromeDinoGame.Entities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChromeDinoGame.Services
{
    class ObjectHandler
    {
        private Canvas _canvas;
        private Random _random;
        private double _speedOfEntities;
        private double _lineOfGround;
        private Dino _dino;
        private ObstacleSpawner _obstaclesGenerator;
        private List<Obstacle> _obstacles = new List<Obstacle>();
        private List<Cloud> _clouds = new List<Cloud>();
        private List<Road> _roads = new List<Road>();
        private TextBlock _scoreBlock;
        private TextBlock _highestScoreBlock;
        private TextBlock _instructionBlock;

        public ObjectHandler(Random random, Canvas canvas, double speedOfEntities, double lineOfGround)
        {
            _canvas = canvas;
            _random = random;
            _lineOfGround = lineOfGround;
            _speedOfEntities = speedOfEntities;
            _dino = new Dino(lineOfGround, _speedOfEntities);
            _obstaclesGenerator = new ObstacleSpawner(_speedOfEntities, _canvas.Width, _canvas.Height, lineOfGround);
            _instructionBlock = new TextBlock
            {
                Text = "Reach 100000 points to complete the game\nPress ENTER to start",
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Gray,
                TextAlignment = TextAlignment.Center,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(150, 140, 0, 0)
            };
        }

        public void InitializeStartWindow(double score, double highestScore = -1)
        {
            RenderEntity(_dino);
            AddRoad(0);
            AddRoad();
            AddCloud(_random.Next(200, 500));

            DisplayScore(score);
            if (highestScore == -1)
            {
                _canvas.Children.Add(_instructionBlock);
                Panel.SetZIndex(_instructionBlock, 10);
            }    
            else
                DisplayHighestScore(highestScore);
        }

        public void IncrementGameSpeed(double score) => _scoreBlock.Text = $"score: {score}";

        private void DisplayScore(double score)
        {
            _scoreBlock = new TextBlock
            {
                Text = $"score: {score}",
                FontSize = 16,
                Foreground = Brushes.DarkSlateGray,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(500, 20, 0, 0)
            };

            _canvas.Children.Add(_scoreBlock);
        }

        private void DisplayHighestScore(double highestScore)
        {
            _highestScoreBlock = new TextBlock
            {
                Text = $"HI {highestScore}",
                FontSize = 16,
                Foreground = Brushes.DarkSlateGray,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(480, 20, 0, 0)
            };

            _canvas.Children.Add(_highestScoreBlock);
        }

        public void IncreaseSpeed(double speedIncrement) => _speedOfEntities += speedIncrement;

        public void UpdateEntities()
        {
            if (_canvas.Children.Contains(_instructionBlock))
                _canvas.Children.Remove(_instructionBlock);
            
            UpdateClouds();
            UpdateRoads();
            UpdateObstacles();
            UpdateDino();
        }

        public bool CheckCollision()
        {
            Rect dinoRect = new Rect(_dino.PosX, _dino.PosY, _dino.Width - 20, _dino.Height - 20);

            foreach (Obstacle obstacle in _obstacles)
            {
                if (obstacle.CheckCollision(dinoRect , obstacle))
                    return true;
            }
            return false;
        }

        public void Jump()
        {
            if (!_dino.IsJumping && _dino.IsAlive)
            {
                _canvas.Children.Remove(_dino.Sprite);
                _dino.Jump();
                RenderEntity(_dino);
            }
        }

        public void Crouch()
        {
            if (!_dino.IsCrouching && _dino.IsAlive)
            {
                _canvas.Children.Remove(_dino.Sprite);
                _dino.Crouch();
                RenderEntity(_dino);
            }
        }

        public void Run()
        {
            if (!_dino.IsRunning && _dino.IsAlive)
            {
                _canvas.Children.Remove(_dino.Sprite);
                _dino.Run();
                RenderEntity(_dino);
            }
        }

        public void HandleDinoDeath()
        { 
            _canvas.Children.Remove( _dino.Sprite);
            _dino.SetDinoDead();
            RenderEntity(_dino);
        }

        private void UpdateDino()
        {
            if (_dino.IsJumping)
            {
                _dino.MoveObject();
                UpdatePosition(_dino);
            }
            if (!_dino.IsCrouching && !_dino.IsRunning && !_dino.IsJumping)
            {
                _canvas.Children.Remove(_dino.Sprite);
                Run();
            }
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

            if (_obstacles.Count == 0 || _obstacles[_obstacles.Count - 1].PosX < _random.Next(50, 75))
            {
                AddObstacle();
            }
        }

        private void UpdateClouds()
        {
            for (int i = _clouds.Count - 1; i >= 0; i--)
            {

                if (_clouds[i].IsInWindow(_canvas.Width))
                    UpdatePosition(_clouds[i]);
                else
                {
                    _canvas.Children.Remove(_clouds[i].Sprite);
                    _clouds.RemoveAt(i);
                }
            }

            if (_clouds[_clouds.Count - 1].PosX < _random.Next(150, 350))
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

        private void AddObstacle()
        {
            _obstacles.Add(_obstaclesGenerator.GenerateObstacle());
            RenderEntity(_obstacles[_obstacles.Count - 1]);
        }
        private void AddRoad(double x = 650)
        {
            _roads.Add(new Road(_random, _speedOfEntities, x, _lineOfGround));
            RenderEntity(_roads[_roads.Count - 1]);
        }

        private void AddCloud(double x = 650)
        {
            _clouds.Add(new Cloud(x, _random.Next(200, 340), _speedOfEntities / 10));
            RenderEntity(_clouds[_clouds.Count - 1]);
        } 

        private void RenderEntity(Entity entity)
        {
            _canvas.Children.Add(entity.Sprite);
            Canvas.SetLeft(entity.Sprite, entity.PosX);
            Canvas.SetBottom(entity.Sprite, entity.PosY);

            if (entity is Dino)
                Canvas.SetZIndex(entity.Sprite, 4);
            else if (entity is Obstacle)
                Canvas.SetZIndex(entity.Sprite, 3);
            else if (entity is Road)
                Canvas.SetZIndex(entity.Sprite, 2);
            else
                Canvas.SetZIndex(entity.Sprite, 1);
        }   

        private void UpdatePosition(Entity entity)
        {
            entity.MoveObject();

            if (entity is Dino)
                Canvas.SetBottom(entity.Sprite, entity.PosY);
            else
                Canvas.SetLeft(entity.Sprite, entity.PosX);
        }
    }
}
