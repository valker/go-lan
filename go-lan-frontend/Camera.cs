using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace go_lan_frontend
{
    public class Camera
    {
        private Matrix projection;
        public Camera(Vector3 position, Viewport viewport)
        {
            Position = position;
            LookAt = Vector3.Zero;
            CameraUp = Vector3.Up;
            Rotation = Vector3.Zero;
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), viewport.AspectRatio, 1f, 1000f);
        }

        public Vector3 Position { get; set; }
        public Vector3 LookAt { get; set; }
        public Vector3 CameraUp { get; set; }
        public Vector3 Rotation { get; set; }

        public Matrix View
        {
            get
            {
                return Matrix.CreateRotationY(Rotation.Y) * 
                       Matrix.CreateRotationX(Rotation.X) *
                       Matrix.CreateRotationZ(Rotation.Z) *
                       Matrix.CreateLookAt(Position ,LookAt, CameraUp);
            }
        }

        public Matrix Projection
        {
            get
            {
                return projection;
            }
            set
            {
                projection = value;
            }
        }
    }
}
