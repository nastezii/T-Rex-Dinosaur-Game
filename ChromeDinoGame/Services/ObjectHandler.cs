using ChromeDinoGame.Entities;
using System.Windows.Controls;

namespace ChromeDinoGame.Services
{
    class ObjectHandler
    {
        private Canvas _canvas;
        private List<Entity> _objects;
        public ObjectHandler(Canvas canvas)
        {
            _canvas = canvas;
            _objects = new List<Entity>();  
        }

        public void AddObject(Entity entity) => _objects.Add(entity);

    }
}
