﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UniVer
{
    public class DemoCollision : Demo
    {
        protected override void Init(World world)
        {

        }

        protected override World CreateWorld()
        {
            World.gravity = 0.2f;
            return new World()
            {
                softIntegration = false
            };
        }

        public override void OnGUI()
        {
            base.OnGUI();
            DragHandle();
        }

        private void DragHandle()
        {
            model.dragPosition = GetWorldPosition(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                var body = world.GetBodyAt(model.dragPosition);
                if (body != null)
                {
                    model.draggedBody = body;
                    model.draggedVertex = Collision.GetClosestVertex(body.vertices, model.dragPosition);
                }
            }
            else if (Input.GetMouseButton(0) && model.draggedBody != null)
            {
                var s = model.draggedBody.mass * World.forceDrag;
                model.draggedVertex.position += (model.dragPosition - model.draggedVertex.position) / s;
            }
            else if (Input.GetMouseButtonUp(0) && model.draggedBody != null)
            {
                model.draggedBody = null;
                model.draggedVertex = null;
            }
        }

        #region Demos
        [Demo]
        public void CodePenDemo(World world)
        {
            var codepen = new[]
            {
                "             *                    ",
                "             *                    ",
                "**** **** **** **** **** **** ****",
                "*    *  * *  * *  * *  * *  * *  *",
                "*    *  * *  * **** *  * **** *  *",
                "*    *  * *  * *    *  * *    *  *",
                "**** **** **** **** **** **** *  *",
                "                    *             ",
                "                    *             "
            };
            CreateTextAndSquaresDemo(codepen);
        }

        [Demo(true)]
        public void CodeUnityDemo(World world)
        {
            var codepen = new[]
            {
                "             *                       ",
                "                                     ",
                "*  *  *   *  *  *****  *   *  ****   ",
                "*  *  **  *  *    *     * *   *  *   ",
                "*  *  * * *  *    *      *    *  *   ",
                "*  *  *  **  *    *      *    *  *   ",
                "****  *   *  *    *      *    ****   ",
                "                                     ",
                "                                     "
            };
            CreateTextAndSquaresDemo(codepen);
        }

        private void CreateTextAndSquaresDemo(string[] demoText)
        {
            var w = world.width / 35f;
            var h = world.height / 2 - 4.5f * w;
            var rectSize = w * 0.8f;
            for (var i = 0; i < demoText.Length; i++)
            {
                var line = demoText[i];
                for (var j = 0; j < line.Length; j++)
                {
                    var c = line[j];
                    if (c != ' ')
                        world.CreateRectangle(w * 0.5f + w * j, h + w * i, rectSize, rectSize);
                }
            }

            world.CreateTriangle(w * 7.5f, h + w * 11, w * 10, w * 2, 20);
            world.CreateTriangle(w * 27.5f, h + w * 11, w * 10, w * 2, 20);

            var triangleHeight = world.height - w * 1.8f + 0.4f * w * 0.5f;
            var triSize = w * 4;
            world.CreateTriangle(
                world.width / 2,
                triangleHeight,
                triSize,
                triSize,
                10
            );

            world.CreateTriangle(
                world.width / 4,
                triangleHeight,
                triSize,
                triSize,
                10
            );
        }
        #endregion

        #region Testing Demos
        public void SingleRectFalling(World world)
        {
            // world.CreateRectangle(0, 2, 1, 2, 1);
            world.CreateRectangle(0, 0, 10, 10, 1);
        }

        public void RectOnFloor(World world)
        {
            // world.CreateRectangle(0, 2, 1, 2, 1);
            world.CreateRectangle(100, 189, 10, 10, 1);
        }

        public void TwoBodies(World world)
        {
            world.CreateRectangle(50, 170, 4, 4);

            world.CreateRectangle(40, 180, 32, 4);
        }

        public void FourRectsColliding(World world)
        {
            var s = 4.57f;
            world.CreateRectangle(77f, 74f, s, s, 1);
            world.CreateRectangle(77f, 80f, s, s, 1);

            world.CreateRectangle(117f, 85, s, s, 1);
            world.CreateRectangle(117f, 91f, s, s, 1);
        }

        public void TriangleCollision(World world)
        {
            var w = world.width / 35f;
            var h = world.height / 2 - 4.5f * w;
            world.CreateTriangle(w * 7.5f, h + w * 11, w * 10, w * 2, 20);

            var triangleHeight = world.height - w * 1.8f + 0.4f * w * 0.5f;
            var triSize = w * 4;
            world.CreateTriangle(
                world.width / 4,
                triangleHeight,
                triSize,
                triSize,
                10
            );
        }
        #endregion
    }

}