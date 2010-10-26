using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace go_lan_frontend
{
    public class GameModel
    {
        public GameModel(Model model)
        {
            Model = model;
            Position = Vector3.Zero;
            Rotation = Vector3.Zero;
            Scale = new Vector3(1, 1, 1);
            DrawOnlyOneMesh = false;
            IndexDrawMesh = 0;
        }
        public Model Model { get; set; }
        public Texture2D Texture { get; set; }
        public Texture2D NormalTexture { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 Scale { get; set; }

        public bool DrawOnlyOneMesh { get; set; }

        public int IndexDrawMesh { get; set; }

        public void SetModelEffect(Effect effect)
        {
            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    part.Effect = effect;
                }
            }
        }

        public Matrix World
        {
            get
            {
                return Matrix.CreateFromYawPitchRoll(
                        Rotation.X,// + MathHelper.ToRadians(-90),
                        Rotation.Y,
                        Rotation.Z) *
                        Matrix.CreateScale(Scale) *
                        Matrix.CreateTranslation(Position); ;
            }
        }
    }
}
