using ColorMine.ColorSpaces;
using OpenTK.Graphics;
using ScottPlot;
using ScottPlot.Drawing.Colormaps;
using ScottPlot.Renderable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AerotorViewLog_3._0
{
    public partial class Form1 : Form
    {
        private const int NUM_OF_LINES_TO_ALLOC = 10000;
        private int currNumOfLines = NUM_OF_LINES_TO_ALLOC;
        private string[] dataLinesLst = new string[NUM_OF_LINES_TO_ALLOC];
        private string[] valNamesLst = new string[] { };

        // a dic containig all value names plotted already, and their corresponding plot
        private Dictionary<string, ScottPlot.Plottable.SignalPlot> valNamePlotDic =
            new Dictionary<string, ScottPlot.Plottable.SignalPlot>();

        // a dic containig all value names plotted already, and a bool represnting on which Y axis they been plotted on
        // bool[0] == left Y axis, bool[1] == right Y axis, bool[2] == costum Y axis
        private Dictionary<string, bool[]> isPlottedDic = new Dictionary<string, bool[]>();

        // a dic conating all the value names plotted on custom Y axis, and their corresponding costum Y axis
        private Dictionary<string, Axis> valNameCustomAxisDic = new Dictionary<string, Axis>();
        
        // a list of all colors of current plotted value names
        private List<int[]> colorLst = new List<int[]>();

        private string prevCustomAxisValName = "";
        private string currCustomAxisValName = "";
        
        // the tree containg all value names, 
        private TreeNode prevCustomAxisNode = new TreeNode();
        private TreeNode mainNode = new TreeNode("All Data Values");

        private ScottPlot.Plottable.MarkerPlot marker = null;
        Random rand = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Allowed files|*.txt;*.csv|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                tBoxGetFile.Text = openFileDialog1.FileName;
                browseFile();
            }
        }

        private void browseFile()
        {
            // reset initial data structures
            currNumOfLines = NUM_OF_LINES_TO_ALLOC;
            dataLinesLst = new string[NUM_OF_LINES_TO_ALLOC];
            valNamesLst = new string[] { };
            valNamePlotDic = new Dictionary<string, ScottPlot.Plottable.SignalPlot>();
            isPlottedDic = new Dictionary<string, bool[]>();
            valNameCustomAxisDic = new Dictionary<string, Axis>();
            colorLst = new List<int[]>();
            prevCustomAxisValName = "";
            treeViewDataVals.Nodes.Clear();
            mainNode = new TreeNode("All Data Values");
            mainNode.Name = "mainNode";
            currCustomAxisValName = "";
            treeViewDataVals.Nodes.Add(mainNode);
            marker = new ScottPlot.Plottable.MarkerPlot();
            dataGraph.Reset();


            if (new string[] { ".csv", ".txt" }.Contains(Path.GetExtension(tBoxGetFile.Text).ToLower()))
            {
                readData(tBoxGetFile.Text);
            }
        }

        private void readData(string fileName)
        {
            int i = 0;
            var reader = new StreamReader(fileName);
            var line = reader.ReadLine();
            valNamesLst = line.Split(';');

            // collect data from file
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                dataLinesLst[i++] = line;
                if (i >= currNumOfLines)
                {
                    // increase the number of lines
                    currNumOfLines += NUM_OF_LINES_TO_ALLOC;
                    Array.Resize<String>(ref dataLinesLst, currNumOfLines);
                }
            }

            // create list of value names and corresponding tree nodes to each value name
            valNamesLst = valNamesLst[0].Split(',');
            foreach (string name in valNamesLst)
            {
                TreeNode treeNode = genValNameTreeNode(name);
                mainNode.Nodes.Add(treeNode);
                isPlottedDic.Add(name, new bool[3] { false, false, false });   
            }

        }

        private TreeNode genValNameTreeNode(string name)
        {
            TreeNode treeNode = new TreeNode();
            treeNode.Name = name;
            treeNode.Text = name;
            TreeNode leftAxis = new TreeNode("Plot On Left Axis");
            TreeNode rightAxis = new TreeNode("Plot On Right Axis");
            TreeNode reScaleAxis = new TreeNode("Plot On Custom Axis");
            treeNode.Nodes.Add(leftAxis);
            treeNode.Nodes.Add(rightAxis);
            treeNode.Nodes.Add(reScaleAxis);
            return treeNode;
        }

        private void dataGraph_LeftClicked(object sender, EventArgs e)
        {
            // get mouse position on the screen
            Point mouseLoc = new Point(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y);

            // modify it to be mouse position on the ScottPlot
            mouseLoc.X -= this.PointToScreen(dataGraph.Location).X;
            mouseLoc.Y -= this.PointToScreen(dataGraph.Location).Y;

            double x = dataGraph.Plot.GetCoordinateX(mouseLoc.X);
            double y = dataGraph.Plot.GetCoordinateY(mouseLoc.Y);

            if (marker != null)
            {
                marker.X = Math.Round(x, 0);
                marker.Y = Math.Round(y, 1);
                marker.Text = marker.X.ToString("F0") + "," + marker.Y.ToString("F1");
            }
        }

        private void vScrollBarOffset_Scroll(object sender, ScrollEventArgs e)
        {
            if(currCustomAxisValName != "")
            {
                Axis reScaledAxis = valNameCustomAxisDic[currCustomAxisValName];
                float changeInValues = e.OldValue - e.NewValue;
                float precentOfReScale = changeInValues / 30;
                dataGraph.Plot.AxisZoom(1, 1 + precentOfReScale, 1, 1, 1, reScaledAxis.AxisIndex);
                dataGraph.Refresh();
            }  
        }

        private void plotGraph(int index, System.Drawing.Color myColor, int sign)
        {
            double[] y = new double[dataLinesLst.Length];
            Array.Clear(y, 0, y.Length);
            for (int i = 0; i < dataLinesLst.Length; i++)
            {
                double d = 0;
                if (dataLinesLst[i] == null)
                    break;
                string[] list = dataLinesLst[i].Split(',');
                if (index >= list.Count())
                {
                    continue;
                }
                if (double.TryParse(list[index], out d))
                {
                    // add value of d
                    y[i] = d;
                }
                else
                {
                    // add 0?
                    y[i] = 0;
                }
            }

            string valName = valNamesLst[index];
            var sig = dataGraph.Plot.AddSignal(y, color: myColor, label: valName);
            dataGraph.Plot.AddMarker(0, y[0], MarkerShape.filledDiamond, 6, System.Drawing.Color.Black);
            sig.YAxisIndex = sign;
            ScottPlot.Plottable.SignalPlot plt = sig;
            valNamePlotDic.Add(valName, plt);

            // plot on right Y axis
            if (sign == 1)
            {
                dataGraph.Plot.YAxis2.Ticks(true);
            }

            // plot on a unique Y axis for this plot
            else if (sign == 2)
            {
                // if value name had been plotted on a custom Y axis before
                if (valNameCustomAxisDic.Keys.Contains(valName))
                {
                    Axis plotLabelAxis = valNameCustomAxisDic[valName];
                    sig.YAxisIndex = plotLabelAxis.AxisIndex;
                    plotLabelAxis.IsVisible = true;   
                }

                // if value name is plotted on a custom Y axis for the first time
                else
                {
                    Axis newYAxis = dataGraph.Plot.AddAxis(Edge.Left);
                    int newAxisIndex = newYAxis.AxisIndex;
                    sig.YAxisIndex = newAxisIndex;
                    valNameCustomAxisDic.Add(valName, newYAxis);
                    newYAxis.Label(valName + " Axis");
                }

                // incase this is the first value name to be plotted on a custom Y axis (i.e. dic is empty)
                if (valNameCustomAxisDic.Keys.Contains(prevCustomAxisValName))
                {
                    Axis prevReScaledAxis = valNameCustomAxisDic[prevCustomAxisValName];
                    prevReScaledAxis.IsVisible = false;
                }

                currCustomAxisValName = valName;
            }

            dataGraph.Plot.Legend();
            dataGraph.Refresh();
        }

        /*generstes a random color, until it finds a color that is different enough from 
         the previous colors, and is not too bright or too dark*/
        private System.Drawing.Color genColor()
        {
            while (true)
            {
                // signifies if color is different from previuos colors and is not too bright or too dark
                bool colorIsVslid = true; 
                int[] randColorARGB = new int[] { rand.Next(0, 256), rand.Next(0, 256), rand.Next(0, 256) };
                foreach (int[] colorARGB in colorLst)
                {
                    if (!checkColorValidity(randColorARGB, colorARGB) || colorTooBrightTooDark(randColorARGB))
                    {
                        colorIsVslid = false;
                        break;
                    }
                }

                if (colorIsVslid)
                {
                    colorLst.Add(randColorARGB);
                    return System.Drawing.Color.FromArgb(255, randColorARGB[0], randColorARGB[1], randColorARGB[2]);
                }

                else
                {
                    continue;
                }
            }
        }

        private bool colorTooBrightTooDark(int[] color)
        {
            float res = (int)Math.Sqrt(
                        color[0] * color[0] * .241 +
                        color[1] * color[1] * .691 +
                        color[2] * color[2] * .068);
            return res > 200 && res < 50;
        }

        /*checks if 2 colors are too close bt cnverting their ARGB values to LAB values,
         and then calculating the metric distance between them in the LAB-values metric space*/
        private bool checkColorValidity(int[] color1, int[] color2)
        {
            double[] color1LAB = RGB2LAB(color1);
            double[] color2LAB = RGB2LAB(color2);
            double a1 = color1LAB[0];
            double a2 = color2LAB[0];
            double b1 = color1LAB[1];
            double b2 = color2LAB[1];
            double c1 = color1LAB[2];
            double c2 = color2LAB[2];
            float disatance =
               (float)Math.Sqrt(Math.Pow((a2 - a1), 2) + Math.Pow((b2 - b1), 2) + Math.Pow((c2 - c1), 2));
            if (disatance < 25) { return false; }
            else { return true; }
        }

        private double[] RGB2LAB(int[] color)
        {
            Rgb rgb = new Rgb() { R = color[0], G = color[1], B = color[2] };
            Lab lab = rgb.To<Lab>();
            double[] res = new double[] { lab.L, lab.A, lab.B };
            return res;
        }

        private void treeViewDataVals_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode currNode = e.Node;
            TreeNode parent = currNode.Parent;
            string valName = "";
            
            // if parent == null, parent.Name will not compile
            if(parent != null)
            {
                valName = parent.Name;
            }

            // makes sure something happens ONLY when a plot node is double-clicked 
            if (!valNamesLst.Contains(currNode.Name) && currNode.Name != "mainNode")   
            {
                foreach (TreeNode node in parent.Nodes)
                {
                    node.ForeColor = Color.Black;
                }
                parent.ForeColor = Color.Black;

                // if value name had been plotted already, on any axis -
                // remove it so it can be plotted on another Y axis (or to un-plot it)
                if (valNamePlotDic.Keys.Contains(valName))
                {
                    dataGraph.Plot.Remove(valNamePlotDic[valName]);
                    valNamePlotDic.Remove(valName);
                }

                Color color = genColor();
                int index = valNamesLst.ToList().IndexOf(valName);

                // plot on left Y axis
                if (currNode.Text == "Plot On Left Axis" && !isPlottedDic[valName][0])
                {
                    plotGraph(index, color, 0);
                    isPlottedDic[valName][0] = true;
                    currNode.ForeColor = color;
                    parent.ForeColor = color;
                    parent.Text = valName + "       LEFT AXIS";
                }

                // plot on right Y axis
                else if (currNode.Text == "Plot On Right Axis" && !isPlottedDic[valName][1])
                {
                    plotGraph(index, color, 1);
                    isPlottedDic[valName][1] = true;
                    currNode.ForeColor = color;
                    parent.ForeColor = color;
                    parent.Text = valName + "       Right AXIS";
                }

                // plot on costum Y axis
                else if (currNode.Text == "Plot On Custom Axis" && !isPlottedDic[valName][2])
                {
                    vScrollBarOffset.Value = 45;
                    plotGraph(index, color, 2);
                    isPlottedDic[valName][2] = true;
                    currNode.ForeColor = color;
                    parent.ForeColor = color;

                    // change label of prev re-scaled node
                    if(isPlottedDic.ContainsKey(prevCustomAxisValName)) 
                    {
                        if (isPlottedDic[prevCustomAxisValName][2])
                        {
                            prevCustomAxisNode.Text = prevCustomAxisValName + "       CUSTOM AXIS (HIDDEN)";
                        }
                    }
                                       
                    parent.Text = valName + "       CUSTOM AXIS";
                    prevCustomAxisValName = valName;
                    prevCustomAxisNode = parent;
                }

                // delete value names' plot from the graph
                else
                {
                    isPlottedDic[valName] = new bool[3] { false, false, false };
                    parent.Text = valName;
                }

                dataGraph.Refresh();
            }

        }

    }

}
