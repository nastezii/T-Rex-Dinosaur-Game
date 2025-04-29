using ChromeDinoGame.Entities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChromeDinoGame.Services
{
    class ObjectHandler
    {
        public Dino Dino { get; private set; }
        private Canvas _canvas;
        private Random _random;
        private double _speedOfEntities;
        private double _lineOfGround;
        private ObstacleSpawner _obstaclesGenerator;
        private List<Obstacle> _obstacles = new List<Obstacle>();
        private List<Cloud> _clouds = new List<Cloud>();
        private List<Road> _roads = new List<Road>();
        private TextBlock _scoreBlock;
        private TextBlock _highestScoreBlock;
        private TextBlock _instructionBlock;
        private TextBlock _replayBlock;

        public ObjectHandler(Random random, Canvas canvas, double speedOfEntities, double lineOfGround)
        {
            _canvas = canvas;
            _random = random;
            _lineOfGround = lineOfGround;
            _speedOfEntities = speedOfEntities;
            Dino = new Dino(lineOfGround, _speedOfEntities);
            _obstaclesGenerator = new ObstacleSpawner(_speedOfEntities, _canvas.Width, _canvas.Height, lineOfGround);

            _instructionBlock = new TextBlock
            {
                Text = "Reach 100000 points to complete the game\nPress ENTER to start",
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Gray,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(150, 140, 0, 0)
            };

            _replayBlock = new TextBlock
            {
                Text = "Press ENTER to replay",
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Gray,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(240, 170, 0, 0)
            };
        }

        public void InitializeStartWindow(double score)
        {
            RenderEntity(Dino);
            AddRoad(0);
            AddRoad();
            AddCloud(_random.Next(200, 500));

            DisplayScore(score);
            DisplayHighestScore(score);
            _canvas.Children.Add(_instructionBlock);
            Panel.SetZIndex(_instructionBlock, 10);
        }

        public void RestartGame(double score, double highestScore)
        {
            _replayBlock.Visibility = Visibility.Hidden;
            _obstacles.Clear();
            _clouds.Clear();
            _roads.Clear();
            _canvas.Children.Clear();
            _speedOfEntities = 8;

            AddRoad(0);
            AddRoad();
            AddCloud(_random.Next(200, 500));
            Dino.ReviveDino();
            RenderEntity(Dino);
            DisplayScore(score);
            DisplayHighestScore(highestScore);
        }

        public void UpdateGameScores(double score, double highestScore = -1)
        {
            _scoreBlock.Text = $"score: {(int)score}";
            if (highestScore <= score)
                _highestScoreBlock.Text = $"HI {(int)score}";
        } 

        private void DisplayScore(double score)
        {
            _scoreBlock = new TextBlock
            {
                Text = $"score: {(int)score}",
                FontSize = 16,
                Foreground = Brushes.DarkSlateGray,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(520, 20, 0, 0)
            };

            _canvas.Children.Add(_scoreBlock);
            Panel.SetZIndex(_scoreBlock, 10);
        }

        private void DisplayHighestScore(double highestScore)
        {
            _highestScoreBlock = new TextBlock
            {
                Text = $"HI {(int)highestScore}",
                FontSize = 16,
                Foreground = Brushes.DarkSlateGray,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(420, 21, 0, 0)
            };

            _canvas.Children.Add(_highestScoreBlock);
            Panel.SetZIndex(_highestScoreBlock, 10);
        }

        public void UpdateSpeed(double speed) => _speedOfEntities = speed;

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
            Rect dinoRect = new Rect(Dino.PosX, Dino.PosY, Dino.Width - 20, Dino.Height - 20);

            foreach (Obstacle obstacle in _obstacles)
            {
                if (obstacle.CheckCollision(dinoRect , obstacle))
                    return true;
            }
            return false;
        }

        public void Jump()
        {
            if (!Dino.IsJumping && Dino.IsAlive)
            {
                _canvas.Children.Remove(Dino.Sprite);
                Dino.Jump();
                RenderEntity(Dino);
            }
        }

        public void Crouch()
        {
            if (!Dino.IsCrouching && Dino.IsAlive)
            {
                _canvas.Children.Remove(Dino.Sprite);
                Dino.Crouch();
                RenderEntity(Dino);
            }
        }

        public void Run()
        {
            if (!Dino.IsRunning && Dino.IsAlive)
            {
                _canvas.Children.Remove(Dino.Sprite);
                Dino.Run();
                RenderEntity(Dino);
            }
        }

        public void RenderGameOverElements()
        { 
            _canvas.Children.Remove( Dino.Sprite);
            Dino.SetDinoDead();
            RenderEntity(Dino);

            if (!_canvas.Children.Contains(_replayBlock))
            {
                _canvas.Children.Add(_replayBlock);
                Canvas.SetZIndex(_replayBlock, 10);
            }
            
            _replayBlock.Visibility = Visibility.Visible;
        }

        private void UpdateDino()
        {
            if (Dino.IsJumping)
            {
                Dino.MoveObject();
                UpdatePosition(Dino);
            }
            if (!Dino.IsCrouching && !Dino.IsRunning && !Dino.IsJumping)
            {
                _canvas.Children.Remove(Dino.Sprite);
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
