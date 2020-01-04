using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
namespace Snake_GL
{
    public partial class Form1 : Form
    {
        private const int COLUMNS = 40;
        private const int ROWS = 40;
        private const int FPS = 10;
        Game game;
        Timer refreshFunc;
        int index = 0;
        public Form1()
        {
            InitializeComponent();
            game = new Game();
            refreshFunc = new Timer();
            refreshFunc.Enabled = true;
            refreshFunc.Interval = 1000/FPS;
            refreshFunc.Tick += RefreshFunc_Tick;
            refreshFunc.Start();
        }

        private void RefreshFunc_Tick(object sender, EventArgs e)
        {
            glControl1.Invalidate();
        }

        /// <summary>
        /// Painting GlControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            glControl1.MakeCurrent();//Making GL Control as current for loading
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);//Clearing Buffers of Color
            game.drawGrid();
            game.drawSnake();
            game.drawFood();
            GL.Enable(EnableCap.Texture2D);
            TextRenderer.DrawText(e.Graphics, "Score:" + game.score, new Font(FontFamily.GenericSansSerif, 8), new Point(100, 100), Color.Yellow);
            GL.Disable(EnableCap.Texture2D);
            glControl1.SwapBuffers();// Changing Buffer
            if (game.gameOver)
            {
                refreshFunc.Stop();
                MessageBox.Show("You Scored: " + game.score,"Game Over", MessageBoxButtons.OK, MessageBoxIcon.None);
                Application.Exit();
            }
            
        }
        /// <summary>
        /// Initializing GLControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glControl1_Load(object sender, EventArgs e)
        {
            glControl1.MakeCurrent(); //Making GL Control as current for loading
            SetupViewPort();
            GL.ClearColor(Color.Black);//Clearing the GL Control and fill it with the specified color
            game.initGrid(COLUMNS, ROWS);
        }
        public void SetupViewPort()
        {
            GL.Viewport(0,0,500,500);//Setting ViewPort as GlControl
            GL.MatrixMode(MatrixMode.Projection);//Setting Matrix Mode to Projection Mode
            GL.LoadIdentity();//Setting Defaults
            GL.Ortho(0.0, COLUMNS, 0.0, ROWS, -1.0, 1.0);//Setting OrthoGraphic Projection
            GL.MatrixMode(MatrixMode.Modelview);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up)
            {
                game.direction = Direction.UP;
            }
            if (keyData == Keys.Down)
            {
                game.direction = Direction.DOWN;
            }
            if (keyData == Keys.Left)
            {
                game.direction = Direction.LEFT;

            }
            if (keyData == Keys.Right)
            {
                game.direction = Direction.RIGHT;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
