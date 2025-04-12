using ChromeDinoGame.Entities;
using System.Windows;
using System.Windows.Controls;

namespace ChromeDinoGame.Services
{
    class ObjectHandler
    {
        private Canvas _canvas;
        private Dino _dino;
        private List<Entity> _objects;
        public ObjectHandler(Canvas canvas, Dino dino)
        {
            _canvas = canvas;
            _dino = dino;
            _objects = new List<Entity>();  
        }

        public void UpdateObjects()
        {
            foreach (Entity obj in _objects)
            {
                obj.MoveObject();

                if (obj.IsInWindow())
                    UpdatePosition(obj);
                else
                    RemoveObject(obj);
            }
        }

        public bool CheckCollision()
        {
            Rect dinoRect = new Rect(_dino.PosX, _dino.PosY, _dino.Width, _dino.Height);

            foreach (var obj in _objects)
            {
                if (obj.isObstacle)
                {
                    Rect objRect = new Rect(obj.PosX, obj.PosY, obj.Width, obj.Height);

                    if (dinoRect.IntersectsWith(objRect))
                    {
                        return true;
                    }
                }
            }
                return false;
        }   

        public void AddObject(Entity obj)
        {
            _objects.Add(obj);
            _canvas.Children.Add(obj.Sprite);
            Canvas.SetLeft(obj.Sprite, obj.PosX);
            Canvas.SetTop(obj.Sprite, obj.PosY);
        }

        public void RemoveObject(Entity obj)
        {
            _objects.Remove(obj);
            _canvas.Children.Remove(obj.Sprite);
        }

        public void UpdatePosition(Entity obj)
        {
            obj.MoveObject();
            Canvas.SetLeft(obj.Sprite, obj.PosX);
        }
    }
}
