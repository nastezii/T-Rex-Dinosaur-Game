﻿using ChromeDinoGame.Entities;
using System.Windows;

namespace ChromeDinoGame.Services
{
    static class CollisionChecker
    {
        private static List<Action> _collisionReactions = new()
            {
                () => Dino.Instance.SetDeadCharacteristics(),
                () => SoundManager.PlaySound(SoundManager.SoundType.Death)
            };
        private static Dino _dino = Dino.Instance;
        private static int hitboxReduction = 20;

        public static void AddReaction(Action reaction) =>_collisionReactions.Add(reaction);

        public static void ClearReactions() => _collisionReactions.Clear();

        public static void CheckAndReact(Obstacle obstacle)
        {
            Rect dinoRect = new Rect(_dino.PosX, _dino.PosY, _dino.Width - hitboxReduction, _dino.Height - hitboxReduction);
            Rect obstacleRect = new Rect(obstacle.PosX, obstacle.PosY, obstacle.Width - hitboxReduction, obstacle.Height - hitboxReduction);

            if (dinoRect.IntersectsWith(obstacleRect))
            {
                foreach (var reaction in _collisionReactions)
                {
                    reaction.Invoke();
                }
            }
        }
    }
}
