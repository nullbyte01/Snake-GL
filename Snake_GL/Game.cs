using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
namespace Snake_GL
{
    public enum Direction
    {
        NONE,
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    public class Game
    {
        int gridX = 0;
        int gridY = 0;
        int maxLenght = 60;
        int[] posX;
        int[] posY;
        int foodX;
        int foodY;
        bool food = true;
        int snakeLength = 5;
        public int score = 0;
        public Direction direction = new Direction();
        public bool gameOver = false;
        public void initGrid(int x, int y)
        {
            gridX = x;
            gridY = y;
            posX = new int[maxLenght];
            posY = new int[maxLenght];
            for (int i = 0; i < snakeLength; i++)
            {
                posX[i] =20;
                posY[i] =20 - i;
            }
        }
        public void drawGrid()
        {
            for (int x = 0; x < gridX; x++)
            {
                for (int y = 0; y < gridY; y++)
                {
                    unit(x, y);
                }
            }
            unit(20, 20);
        }
        void unit(int x, int y)
        {
            if (x == 0 || y == 0 || x == gridX - 1 || y == gridY - 1)
            {
                GL.LineWidth((float)3);
                GL.Color3(System.Drawing.Color.Red);
                GL.Rect(x, y, x + 1, y + 1);
            }
            else
            {
                GL.LineWidth((float)1);
                GL.Color3(System.Drawing.Color.White);
                GL.Begin(PrimitiveType.LineLoop);
                GL.Vertex2(x, y);
                GL.Vertex2(x + 1, y);
                GL.Vertex2(x + 1, y + 1);
                GL.Vertex2(x, y + 1);
                GL.End();
            }

        }
        public void drawFood()
        {

            if (food)
            {
                Random rand = new Random();
                foodX = rand.Next(1, gridX - 2);
                foodY = rand.Next(1, gridY - 2);
            }
            food = false;
            GL.Color3(System.Drawing.Color.Orange);
            GL.Rect(foodX, foodY, foodX + 1, foodY + 1);
        }
        public void drawSnake()
        {
            for (int i = snakeLength - 1; i > 0; i--)
            {
                posX[i] = posX[i - 1];
                posY[i] = posY[i - 1];
            }
            if (direction == Direction.UP)
            {
                posY[0]++;
                //direction = Direction.NONE;
            }
            if (direction == Direction.DOWN)
            {
                posY[0]--;
                //direction = Direction.NONE;
            }
            if (direction == Direction.RIGHT)
            {
                posX[0]++;
                //direction = Direction.NONE;
            }
            if (direction == Direction.LEFT)
            {
                posX[0]--;
                //direction = Direction.NONE;
            }
            for (int i = 0; i < snakeLength; i++)
            {
                if (i == 0)
                {
                    GL.Color3(System.Drawing.Color.Green);
                }
                else
                {
                    GL.Color3(System.Drawing.Color.Blue);
                }
                GL.Rect(posX[i], posY[i], posX[i] + 1, posY[i] + 1);
            }


            if (posX[0] == 0 || posX[0] == gridX - 1 || posY[0] == 0 || posY[0] == gridY - 1)
            {
                gameOver = true;
            }       
            if (posX[0] == foodX && posY[0] == foodY)
            {
                score++;
                snakeLength++;
                if (snakeLength > maxLenght)
                {
                    snakeLength = maxLenght;
                }
                food = true;
            }
        }
    }
}
