﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniVer
{
    public class WorldGLRenderer : WorldRenderer
    {
        private InteractiveModel model;
        private World world;

        static Material lineMaterial;
        static void CreateLineMaterial()
        {
            if (!lineMaterial)
            {
                // Unity has a built-in shader that is useful for drawing
                // simple colored things.
                Shader shader = Shader.Find("Hidden/Internal-Colored");
                lineMaterial = new Material(shader);
                lineMaterial.hideFlags = HideFlags.HideAndDontSave;
                // Turn on alpha blending
                lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                // Turn backface culling off
                lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
                // Turn off depth writes
                lineMaterial.SetInt("_ZWrite", 0);
            }
        }

        public override void BindModel(InteractiveModel model)
        {
            this.model = model;
            this.world = model.world;
        }

        // Will be called after all regular rendering is done
        public void OnRenderObject()
        {
            CreateLineMaterial();
            // Apply the line material
            lineMaterial.SetPass(0);

            GL.PushMatrix();
            // Set transformation matrix for drawing to
            // match our transform
            // GL.MultMatrix(transform.localToWorldMatrix);

            DrawWorldBounds();

            foreach (var body in world.bodies)
                DrawBody(body);

            DrawDragConstraint();

            GL.PopMatrix();
        }

        private void DrawWorldBounds()
        {
            GL.Begin(GL.LINE_STRIP);

            GL.Color(new Color(0, 255, 0));
            
            GL.Vertex3(0, 0, 0);

            GL.Vertex3(world.width, 0, 0);

            GL.Vertex3(world.width, world.height, 0);

            GL.Vertex3(0, world.height, 0);

            GL.Vertex3(0, 0, 0);

            GL.End();
        }

        private void DrawBody(Body body)
        {
            GL.Begin(GL.LINE_STRIP);

            GL.Color(new Color(255, 0, 0));
            foreach (var v in body.vertices)
            {
                GL.Vertex3(v.position.x, world.height - v.position.y, 0);
            }

            GL.Vertex3(body.vertices[0].position.x, world.height - body.vertices[0].position.y, 0);
            GL.End();
        }

        private void DrawDragConstraint()
        {
            if (model.draggedBody == null)
                return;

            GL.Begin(GL.LINE_STRIP);

            GL.Color(new Color(0, 255, 0));

            GL.Vertex3(model.draggedVertex.position.x, world.height - model.draggedVertex.position.y, 0);
            GL.Vertex3(model.dragPosition.x, world.height - model.dragPosition.y, 0);

            GL.End();
        }
    }

}