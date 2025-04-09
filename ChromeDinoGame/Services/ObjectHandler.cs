
using System.Windows.Controls;
using ChromeDinoGame.Entities;

namespace ChromeDinoGame.Services
{
    class ObjectHandler
    {
        private Canvas _canvas;
        private List<GameEntity> _objects;
        public ObjectHandler(Canvas canvas)
        {
            _canvas = canvas;
            _objects = new List<GameEntity>();  
        }

        public void AddObject(GameEntity entity) => _objects.Add(entity);

    }
}
