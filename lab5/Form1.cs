using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpGL;

namespace lab5
{
    public partial class Form1 : Form
    {
        bool pause;
		float[] vertices;
		float[] ribs;

		float[] lightModel;
		float[] lightPosition;
		float[] lightDiffuse;
        float[] lightDirection;
        float[] lightSpecular;

		float[,] colors;
		float[,] cubeVertices;

        OpenGL gl;

		public Form1()
		{
			InitializeComponent();
			vertices = new float[24];
			ribs = new float[24];
			colors = new float[6, 3];
			cubeVertices = new float[24, 3];
            pause = false;
			
			InitializeObject();
			InitializeGl();
			SceneSetup();
		}


		void InitializeObject()
		{
			//top
			colors[0, 0] = 1;
			colors[0, 1] = 0;
			colors[0, 2] = 0;

			cubeVertices[0, 0] = 1.0f;
			cubeVertices[0, 1] = -1.0f;
			cubeVertices[0, 2] = -1.0f;

			cubeVertices[1, 0] = 1.0f;
			cubeVertices[1, 1] = -1.0f;
			cubeVertices[1, 2] = 1.0f;

			cubeVertices[2, 0] = 1.0f;
			cubeVertices[2, 1] = 1.0f;
			cubeVertices[2, 2] = 1.0f;

			cubeVertices[3, 0] = 1.0f;
			cubeVertices[3, 1] = 1.0f;
			cubeVertices[3, 2] = -1.0f;

			//bottom
			colors[1, 0] = 0;
			colors[1, 1] = 1;
			colors[1, 2] = 0;

			cubeVertices[4, 0] = -1.0f;
			cubeVertices[4, 1] = -1.0f;
			cubeVertices[4, 2] = -1.0f;

			cubeVertices[5, 0] = -1.0f;
			cubeVertices[5, 1] = -1.0f;
			cubeVertices[5, 2] = 1.0f;

			cubeVertices[6, 0] = -1.0f;
			cubeVertices[6, 1] = 1.0f;
			cubeVertices[6, 2] = 1.0f;

			cubeVertices[7, 0] = -1.0f;
			cubeVertices[7, 1] = 1.0f;
			cubeVertices[7, 2] = -1.0f;

			//back
			colors[2, 0] = 0;
			colors[2, 1] = 0;
			colors[2, 2] = 1;

			cubeVertices[8, 0] = -1.0f;
			cubeVertices[8, 1] = -1.0f;
			cubeVertices[8, 2] = -1.0f;

			cubeVertices[9, 0] = 1.0f;
			cubeVertices[9, 1] = -1.0f;
			cubeVertices[9, 2] = -1.0f;

			cubeVertices[10, 0] = 1.0f;
			cubeVertices[10, 1] = -1.0f;
			cubeVertices[10, 2] = 1.0f;

			cubeVertices[11, 0] = -1.0f;
			cubeVertices[11, 1] = -1.0f;
			cubeVertices[11, 2] = 1.0f;

			//left
			colors[3, 0] = 1;
			colors[3, 1] = 1;
			colors[3, 2] = 0;

			cubeVertices[12, 0] = 1.0f;
			cubeVertices[12, 1] = 1.0f;
			cubeVertices[12, 2] = -1.0f;

			cubeVertices[13, 0] = 1.0f;
			cubeVertices[13, 1] = -1.0f;
			cubeVertices[13, 2] = -1.0f;

			cubeVertices[14, 0] = -1.0f;
			cubeVertices[14, 1] = -1.0f;
			cubeVertices[14, 2] = -1.0f;

			cubeVertices[15, 0] = -1.0f;
			cubeVertices[15, 1] = 1.0f;
			cubeVertices[15, 2] = -1.0f;

			//right
			colors[4, 0] = 1;
			colors[4, 1] = 0;
			colors[4, 2] = 1;

			cubeVertices[16, 0] = -1.0f;
			cubeVertices[16, 1] = -1.0f;
			cubeVertices[16, 2] = 1.0f;

			cubeVertices[17, 0] = -1.0f;
			cubeVertices[17, 1] = 1.0f;
			cubeVertices[17, 2] = 1.0f;

			cubeVertices[18, 0] = 1.0f;
			cubeVertices[18, 1] = 1.0f;
			cubeVertices[18, 2] = 1.0f;

			cubeVertices[19, 0] = 1.0f;
			cubeVertices[19, 1] = -1.0f;
			cubeVertices[19, 2] = 1.0f;

			//front
			colors[5, 0] = 0;
			colors[5, 1] = 1;
			colors[5, 2] = 1;
			cubeVertices[20, 0] = 1.0f;
			cubeVertices[20, 1] = 1.0f;
			cubeVertices[20, 2] = 1.0f;

			cubeVertices[21, 0] = -1.0f;
			cubeVertices[21, 1] = 1.0f;
			cubeVertices[21, 2] = 1.0f;

			cubeVertices[22, 0] = -1.0f;
			cubeVertices[22, 1] = 1.0f;
			cubeVertices[22, 2] = -1.0f;

			cubeVertices[23, 0] = 1.0f;
			cubeVertices[23, 1] = 1.0f;
			cubeVertices[23, 2] = -1.0f;
		}

        void InitializeGl()
        {
            gl = openGLControl1.OpenGL;
            gl.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.ShadeModel(OpenGL.GL_SMOOTH);
            gl.Enable(OpenGL.GL_COLOR_MATERIAL);
            gl.Enable(OpenGL.GL_LIGHTING);
            gl.Enable(OpenGL.GL_LIGHT0);
            gl.LightModel(OpenGL.GL_LIGHT_MODEL_LOCAL_VIEWER, OpenGL.GL_TRUE);
        }

		void SceneSetup()
		{
            lightModel = new float[] { 0.1f, 0.1f, 0.1f, 1.0f };
            lightPosition = new float[] { 1.0f, 1.0f, 0.0f, 1.0f };
            lightDiffuse = new float[] { 0.5f, 0.5f, 0.5f, 1.0f };
            lightSpecular = new float[] { 0.8f, 0.8f, 0.8f, 1.0f };
            lightDirection = new float[] { 0.0f, 5.0f, 0.0f, 3.0f };


            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
			gl.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, lightModel);
			gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, lightPosition);
			gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT, lightModel);
			gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, lightDiffuse);
			gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPOT_DIRECTION, lightDirection);
			gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPECULAR, lightSpecular);
			gl.Enable(OpenGL.GL_LIGHTING);
			gl.Enable(OpenGL.GL_LIGHT0);

			Draw();
		}

        void Draw()
		{
			float minZ = 5;
			if (!pause)
			{
				for (int i = 0; i < 24; ++i)
				{
					if (cubeVertices[i, 2] < minZ)
					{
						minZ = cubeVertices[i, 2];
					}
				}
				for (int i = 0, vertex = 0; i < 24; i += 4, ++vertex)
				{
					if ((cubeVertices[i, 2] == minZ) || (cubeVertices[i + 1, 2] == minZ) || (cubeVertices[i + 2, 2] == minZ) || (cubeVertices[i + 3, 2] == minZ))
						vertices[vertex] = 0;
					else
						vertices[vertex] = 1;
				}
				for (int i = 0, ribs = 0; i < 24; i += 2, ++ribs)
				{
					if ((cubeVertices[i, 2] == minZ) || (cubeVertices[i + 1, 2] == minZ))
						this.ribs[ribs] = 0;
					else
						this.ribs[ribs] = 1;
				}
			}
			gl.LineWidth(1);


			gl.LineWidth(4);
			for (int i = 0, vertex = 0; i < 24; i += 4, vertex++)

			{
				if (vertices[vertex] == 1)
				{
					gl.Color(colors[vertex, 0], colors[vertex, 1], colors[vertex, 2]);
					gl.Begin(OpenGL.GL_QUADS);
					gl.Vertex(cubeVertices[i, 0], cubeVertices[i, 1], cubeVertices[i, 2]);
					gl.Vertex(cubeVertices[i + 1, 0], cubeVertices[i + 1, 1], cubeVertices[i + 1, 2]);
					gl.Vertex(cubeVertices[i + 2, 0], cubeVertices[i + 2, 1], cubeVertices[i + 2, 2]);
					gl.Vertex(cubeVertices[i + 3, 0], cubeVertices[i + 3, 1], cubeVertices[i + 3, 2]);
					gl.End();
				}
			}

			gl.Color(0.0, 0.0, 0.0);
			for (int i = 0, ribs = 0; i < 24; i += 2, ribs++)
			{
				if (this.ribs[ribs] == 1)
				{
					gl.Begin(OpenGL.GL_LINES);
					gl.Vertex(cubeVertices[i, 0], cubeVertices[i, 1], cubeVertices[i, 2]);
					gl.Vertex(cubeVertices[i + 1, 0], cubeVertices[i + 1, 1], cubeVertices[i + 1, 2]);
					gl.End();
				}
			}


			gl.Color(1.0, 1.0, 1.0);
			gl.Begin(OpenGL.GL_QUADS);
			gl.Vertex(6.0, 6.0, -2.0);
			gl.Vertex(6.0, -6.0, -2.0);
			gl.Vertex(-6.0, -6.0, -2.0);
			gl.Vertex(-6.0, 6.0, -2.0);
			gl.End();
		}

        void TransformX(float angle)
        {
            float[,] mult = {
                { 1.0f, 0.0f, 0.0f },
                { 0.0f, (float)Math.Cos(angle), -(float)Math.Sin(angle) },
                { 0.0f, (float)Math.Sin(angle), (float)Math.Cos(angle) }
            };
            MatrixMultiplication(cubeVertices, mult);
        }

        void TransformY(float angle)
        {
            float[,] mult = {
                { (float)Math.Cos(angle), 0.0f, (float)Math.Sin(angle) },
                { 0.0f, 1.0f, 0.0f },
                { -(float)Math.Sin(angle), 0.0f, (float)Math.Cos(angle) }
            };
            MatrixMultiplication(cubeVertices, mult);
        }

        void TransformZ(float angle)
        {
            float[,] mult = {
                { (float)Math.Cos(angle), -(float)Math.Sin(angle), 0.0f, },
                { (float)Math.Sin(angle), (float)Math.Cos(angle), 0.0f },
                { 0.0f, 0.0f, 1.0f }
            };
            MatrixMultiplication(cubeVertices, mult);
        }

		void MatrixMultiplication(float[,] matrix, float[,] bMatrix)
		{
			float[,] product = new float[24, 3];
            for (int i = 0; i < 24; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    product[i, j] = 0;
                    for (int k = 0; k < 3; k++)
                        product[i, j] += matrix[i, k] * bMatrix[k, j];
                }
            }

            for (int i = 0; i < 24; ++i)
				for (int j = 0; j < 3; ++j)
				{
					matrix[i, j] = product[i, j];
				}

				float[] temp = { 0, 0, 0 };
				for (int j = 0; j < 3; j++)
				{
					for (int k = 0; k < 3; k++)
						temp[j] += bMatrix[j, k] * lightPosition[k];
				}
				for (int i = 0; i < 3; ++i)
				{
					lightPosition[i] = temp[i];
					temp[i] = 0;
				}
				for (int j = 0; j < 3; j++)
				{
					for (int k = 0; k < 3; k++)
						temp[j] += bMatrix[j, k] * lightDirection[k];
				}
				for (int i = 0; i < 3; ++i)
					lightDirection[i] = temp[i];
        }

        private void openGLControl1_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();
            gl.Translate(-1.5f, 0.0f, -6.0f);
            gl.Begin(OpenGL.GL_QUADS);
            Draw();
        }

		private void openGLControl1_KeyDown(object sender, KeyEventArgs e)
		{
            switch (e.KeyCode)
			{
				case Keys.D: TransformY(-0.05f); break;
				case Keys.A: TransformY(+0.05f); break;
				case Keys.W: TransformX(+0.05f); break;
				case Keys.S: TransformX(-0.05f); break;
				case Keys.E: TransformZ(+0.05f); break;
				case Keys.Q: TransformZ(-0.05f); break;
			}
		}

        private void LineButton_Click(object sender, EventArgs e)
		{
			if (pause == false)
			{
				LineButton.Text = "Возобновить выявление граней";
				pause = true;
			}
			else
			{
				LineButton.Text = "Прервать выявление граней";
				pause = false;
			}
		}
    }
}
