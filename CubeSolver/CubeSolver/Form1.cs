using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using DarrenLee.Media;
using System.Drawing.Imaging;

namespace CubeSolver
{
    public partial class Form1 : Form
    {
        Camera myCam;
        Image img;
        bool ok = false;
        String solution = "";
        String[] solutionMoves;
        int solutionIndex = 0;

        // storing cube values
        int[,] white = new int[3, 3] {
            { 1, 1, 1 },
            { 1, 1, 1 },
            { 1, 1, 1 }
        };

        int[,] red = new int[3, 3] {
            { 2, 2, 2 },
            { 2, 2, 2 },
            { 2, 2, 2 }
        };

        int[,] green = new int[3, 3] {
            { 3, 3, 3 },
            { 3, 3, 3 },
            { 3, 3, 3 }
        };

        int[,] blue = new int[3, 3] {
            { 4, 4, 4 },
            { 4, 4, 4 },
            { 4, 4, 4 }
        };

        int[,] orange = new int[3, 3] {
            { 5, 5, 5 },
            { 5, 5, 5 },
            { 5, 5, 5 }
        };

        int[,] yellow = new int[3, 3] {
            { 6, 6, 6 },
            { 6, 6, 6 },
            { 6, 6, 6 }
        };

        // cube between motors
        //int[] recX = { 53, 120, 199, 61, 220, 70, 144, 232 };
        //int[] recY = { 170, 180, 200, 99, 125, 33, 35, 39 };

        // cube on the top right edge
        int[] recX = { 230, 140, 43, 227, 42, 218, 130, 40 };
        int[] recY = { 20, 20, 20, 101, 101, 190, 190, 190 };

        int[,] colorsRGB = new int[6, 3] {
            { 155, 201, 249 }, //white
            { 114, 0, 0 }, //red
            { 38, 181, 115 }, //green
            { 0, 89, 249 }, //blue
            { 233, 132, 124 }, //orange
            { 131, 211, 110 } //yellow
        };

        // algos
        // second layer
        String[] secondLayer1 = { "'yellow", "'green", "yellow", "green", "yellow", "red", "'yellow", "'red" };
        String[] secondLayer2 = { "yellow", "blue", "'yellow", "'blue", "'yellow", "'red", "yellow", "red" };
        String[] secondLayer3 = { "'yellow", "'red", "yellow", "red", "yellow", "blue", "'yellow", "'blue" };
        String[] secondLayer4 = { "yellow", "orange", "'yellow", "'orange", "'yellow", "'blue", "yellow", "blue" };
        String[] secondLayer5 = { "'yellow", "'blue", "yellow", "blue", "yellow", "orange", "'yellow", "'orange" };
        String[] secondLayer6 = { "yellow", "green", "'yellow", "'green", "'yellow", "'orange", "yellow", "orange" };
        String[] secondLayer7 = { "'yellow", "'orange", "yellow", "orange", "yellow", "green", "'yellow", "'green" };
        String[] secondLayer8 = { "yellow", "red", "'yellow", "'red", "'yellow", "'green", "yellow", "green" };
        // yellow cross
        String[] yellowCross1 = { "'red", "'yellow", "'green", "yellow", "green", "red" };
        String[] yellowCross2 = { "'red", "'green", "'yellow", "green", "yellow", "red" };
        // yellow side
        String[] yellowSide1 = { "'green", "'yellow", "green", "'yellow", "'green", "'yellow", "'yellow", "green" };
        String[] yellowSide2 = { "'green", "'yellow", "'Yellow", "green", "yellow", "'green", "yellow", "green" };
        String[] yellowSide3 = { "'red", "'green", "orange", "green", "red", "'green", "'orange", "green" };
        String[] yellowSide4 = { "'green", "'yellow", "green", "'yellow", "'green", "'yellow", "'yellow", "green" };
        String[] yellowSide5 = { "'green", "'yellow", "green", "'yellow", "'green", "'yellow", "'yellow", "green" };
        String[] yellowSide6 = { "'green", "'yellow", "green", "'yellow", "'green", "'yellow", "'yellow", "green" };
        // final layer corners
        String[] fnCorners = { "green", "'red", "green", "'orange", "'orange", "'green", "red", "green", "'orange", "'orange", "'green", "'green", "yellow" };
        // final layer edged
        String[] fnEdges = { "'red", "'red", "'yellow", "'blue", "green", "'red", "'red", "blue", "'green", "'yellow", "'red", "'red" };
        String[] fnEdges2 = { "'red", "'red", "yellow", "'blue", "green", "'red", "'red", "blue", "'green", "yellow", "'red", "'red" };

        public Form1()
        {
            InitializeComponent();
            try
            {
                myCam = new Camera();
                getPorts();
                getInfo();
                myCam.OnFrameArrived += myCamOnFrameArrived;
                drawCubeSides(white, whiteBox);
                drawCubeSides(red, redBox);
                drawCubeSides(green, greenBox);
                drawCubeSides(blue, blueBox);
                drawCubeSides(orange, orangeBox);
                drawCubeSides(yellow, yellowBox);
                ok = true;
            } catch (Exception)
            {
                console("Connect the CubeSolver and a camera!");
            }
        }

        private void sendMove(int index)
        {
            solutionIndex++;
            if (solutionMoves.Length > index)
            {
                if (solutionMoves[index].Equals("red") || solutionMoves[index].Equals("green") || solutionMoves[index].Equals("blue") || solutionMoves[index].Equals("orange") || solutionMoves[index].Equals("yellow") ||
                    solutionMoves[index].Equals("'red") || solutionMoves[index].Equals("'green") || solutionMoves[index].Equals("'blue") || solutionMoves[index].Equals("'orange") || solutionMoves[index].Equals("'yellow"))
                {
                    serialPort1.WriteLine(solutionMoves[index]);
                } else
                {
                    sendMove(solutionIndex);
                }
            } else
            {
                solutionIndex = 0;
            }
        }

        private String solve (int[,] whiteIn, int[,] redIn, int[,] greenIn, int[,] blueIn, int[,] orangeIn, int[,] yellowIn)
        {
            // white cross
            String result = "WHITE CROSS: ";
            int exit = whiteCrossProgress(white[2, 1], white[1, 0], white[1, 2], white[0, 1], red[0, 1], green[0, 1], blue[0, 1], orange[0, 1]);
            while (exit < 4)
            {
                String crossResult = solveWhiteCross(white, red, green, blue, orange, yellow);
                if (crossResult.Equals("No solutions."))
                {
                    console(crossResult);
                    break;
                }
                String[] moves = crossResult.Split(' ');
                result += crossResult;
                foreach (String move in moves)
                {
                    var res = simulateTurn(white, red, green, blue, orange, yellow, getMoveInt(move));
                    white = res.Item1;
                    red = res.Item2;
                    green = res.Item3;
                    blue = res.Item4;
                    orange = res.Item5;
                    yellow = res.Item6;
                }
                exit = whiteCrossProgress(white[2, 1], white[1, 0], white[1, 2], white[0, 1], red[0, 1], green[0, 1], blue[0, 1], orange[0, 1]);
            }

            // white layer
            result += "WHITE LAYER: ";
            int exitRow = whiteRowProgress1(white, red, green, blue, orange);
            while (exitRow < 4)
            {
                String rowResult = solveWhiteRow1(white, red, green, blue, orange, yellow);
                if (rowResult.Equals("No solutions."))
                {
                    rowResult = "";
                    String rowResult2 = solveWhiteRow2(white, red, green, blue, orange, yellow);
                    /*if (rowResult2.Equals("No solutions."))
                    {
                        console(rowResult2);
                        break;
                    }*/
                    String[] moves2 = rowResult2.Split(' ');
                    result += rowResult2;
                    foreach (String move in moves2)
                    {
                        var res = simulateTurn(white, red, green, blue, orange, yellow, getMoveInt(move));
                        white = res.Item1;
                        red = res.Item2;
                        green = res.Item3;
                        blue = res.Item4;
                        orange = res.Item5;
                        yellow = res.Item6;
                    }
                }
                String[] moves = rowResult.Split(' ');
                result += rowResult;
                foreach (String move in moves)
                {
                    var res = simulateTurn(white, red, green, blue, orange, yellow, getMoveInt(move));
                    white = res.Item1;
                    red = res.Item2;
                    green = res.Item3;
                    blue = res.Item4;
                    orange = res.Item5;
                    yellow = res.Item6;
                }
                exitRow = whiteRowProgress1(white, red, green, blue, orange);
            }
            // second row
            result += "SECOND ROW: ";
            int exitSRow = secondRowProgress(white, red, green, blue, orange);
            while (exitSRow < 4)
            {
                String rowResult = solveSecondRow2(white, red, green, blue, orange, yellow);

                String[] moves = rowResult.Split(' ');
                result += rowResult;
                foreach (String move in moves)
                {
                    var res = simulateTurn(white, red, green, blue, orange, yellow, getMoveInt(move));
                    white = res.Item1;
                    red = res.Item2;
                    green = res.Item3;
                    blue = res.Item4;
                    orange = res.Item5;
                    yellow = res.Item6;
                }

                String rowResult2 = solveSecondRow(white, red, green, blue, orange, yellow);

                String[] moves2 = rowResult2.Split(' ');
                result += rowResult2;
                foreach (String move in moves2)
                {
                    var res = simulateTurn(white, red, green, blue, orange, yellow, getMoveInt(move));
                    white = res.Item1;
                    red = res.Item2;
                    green = res.Item3;
                    blue = res.Item4;
                    orange = res.Item5;
                    yellow = res.Item6;
                }
                exitSRow = secondRowProgress(white, red, green, blue, orange);
            }
            // yellow cross
            result += "YELLOW CROSS: ";
            int exitY = yellowCrossProgress(white, red, green, blue, orange, yellow);
            while (exitY < 4)
            {
                String crossResult = solveYellowCross(white, red, green, blue, orange, yellow);
                if (crossResult.Equals("No solutions."))
                {
                    console(crossResult);
                    break;
                }
                String[] moves = crossResult.Split(' ');
                result += crossResult;
                foreach (String move in moves)
                {
                    var res = simulateTurn(white, red, green, blue, orange, yellow, getMoveInt(move));
                    white = res.Item1;
                    red = res.Item2;
                    green = res.Item3;
                    blue = res.Item4;
                    orange = res.Item5;
                    yellow = res.Item6;
                }
                exitY = yellowCrossProgress(white, red, green, blue, orange, yellow);
            }
            // yellow layer
            result += "YELLOW LAYER: ";
            int exitYL = yellowLayerProgress(white, red, green, blue, orange, yellow);
            while (exitYL < 8)
            {
                String layerResult = solveYellowLayer(white, red, green, blue, orange, yellow);
                if (layerResult.Equals("No solutions."))
                {
                    console(layerResult);
                    break;
                }
                String[] moves = layerResult.Split(' ');
                result += layerResult;
                foreach (String move in moves)
                {
                    var res = simulateTurn(white, red, green, blue, orange, yellow, getMoveInt(move));
                    white = res.Item1;
                    red = res.Item2;
                    green = res.Item3;
                    blue = res.Item4;
                    orange = res.Item5;
                    yellow = res.Item6;
                }
                exitYL = yellowLayerProgress(white, red, green, blue, orange, yellow);
            }
            // final layer corners
            result += "CORNERS: ";
            int exitC = FinalLayerCornersProgress(white, red, green, blue, orange);
            while (exitC < 4)
            {
                String layerResult = solveFinalLayerCorners(white, red, green, blue, orange, yellow);
                if (layerResult.Equals("No solutions."))
                {
                    console(layerResult);
                    break;
                }
                String[] moves = layerResult.Split(' ');
                result += layerResult;
                foreach (String move in moves)
                {
                    var res = simulateTurn(white, red, green, blue, orange, yellow, getMoveInt(move));
                    white = res.Item1;
                    red = res.Item2;
                    green = res.Item3;
                    blue = res.Item4;
                    orange = res.Item5;
                    yellow = res.Item6;
                }
                exitC = FinalLayerCornersProgress(white, red, green, blue, orange);
            }
            // final layer edges
            result += "EDGES: ";
            int exitE = FinalLayerEdgesProgress(white, red, green, blue, orange);
            while (exitE < 4)
            {
                String layerResult = solveFinalLayerEdges(white, red, green, blue, orange, yellow);
                if (layerResult.Equals("No solutions."))
                {
                    console(layerResult);
                    break;
                }
                String[] moves = layerResult.Split(' ');
                result += layerResult;
                foreach (String move in moves)
                {
                    var res = simulateTurn(white, red, green, blue, orange, yellow, getMoveInt(move));
                    white = res.Item1;
                    red = res.Item2;
                    green = res.Item3;
                    blue = res.Item4;
                    orange = res.Item5;
                    yellow = res.Item6;
                }

                exitE = FinalLayerEdgesProgress(white, red, green, blue, orange);
            }

            drawCubeSides(white, whiteBox);
            drawCubeSides(red, redBox);
            drawCubeSides(green, greenBox);
            drawCubeSides(blue, blueBox);
            drawCubeSides(orange, orangeBox);
            drawCubeSides(yellow, yellowBox);

            solution = result;
            return result;
        }

        private String solveFinalLayerEdges(int[,] whiteIn, int[,] redIn, int[,] greenIn, int[,] blueIn, int[,] orangeIn, int[,] yellowIn)
        {
            int[,] StartWhite = whiteIn.Clone() as int[,];
            int[,] StartRed = redIn.Clone() as int[,];
            int[,] StartGreen = greenIn.Clone() as int[,];
            int[,] StartBlue = blueIn.Clone() as int[,];
            int[,] StartOrange = orangeIn.Clone() as int[,];
            int[,] StartYellow = yellowIn.Clone() as int[,];
            String result = "No solutions.";
            String algo = "";
            int exit = FinalLayerEdgesProgress(StartWhite, StartRed, StartGreen, StartBlue, StartOrange);

            Random rnd = new Random();
            while (exit < 4)
            {
                int countMain = 0;
                var simResult = simulateTurn(StartWhite, StartRed, StartGreen, StartBlue, StartOrange, StartYellow, -1);
                algo = "";
                int count = 0;
                for (int j = 0; j < rnd.Next(0, 4); j++)
                {
                    count++;
                    for (int i = 0; i < rnd.Next(0, 4); i++)
                    {
                        simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, 4);
                        algo += "yellow ";
                    }
                    foreach (String move in fnEdges)
                    {
                        simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, getMoveInt(move));
                        algo += move + " ";
                    }
                    for (int i = 0; i < rnd.Next(0, 4); i++)
                    {
                        simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, 4);
                        algo += "yellow ";
                    }
                    exit = FinalLayerEdgesProgress(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5);
                    if (exit >= 4)
                    {
                        result = algo;
                        return result;
                    }
                    if (count > 3)
                    {
                        break;
                    }
                }
                countMain++;
                if (countMain > 10)
                {
                    break;
                }
            }
            while (exit < 4)
            {
                var simResult = simulateTurn(StartWhite, StartRed, StartGreen, StartBlue, StartOrange, StartYellow, -1);
                algo = "";
                int count = 0;
                for (int j = 0; j < rnd.Next(0, 4); j++)
                {
                    count++;
                    for (int i = 0; i < rnd.Next(0, 4); i++)
                    {
                        simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, 4);
                        algo += "yellow ";
                    }
                    foreach (String move in fnEdges2)
                    {
                        simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, getMoveInt(move));
                        algo += move + " ";
                    }
                    for (int i = 0; i < rnd.Next(0, 4); i++)
                    {
                        simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, 4);
                        algo += "yellow ";
                    }
                    exit = FinalLayerEdgesProgress(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5);
                    if (exit >= 4)
                    {
                        result = algo;
                        return result;
                    }
                    if (count > 3)
                    {
                        break;
                    }
                }
            }

            return result;
        }

        private String solveFinalLayerCorners(int[,] whiteIn, int[,] redIn, int[,] greenIn, int[,] blueIn, int[,] orangeIn, int[,] yellowIn)
        {
            int[,] StartWhite = whiteIn.Clone() as int[,];
            int[,] StartRed = redIn.Clone() as int[,];
            int[,] StartGreen = greenIn.Clone() as int[,];
            int[,] StartBlue = blueIn.Clone() as int[,];
            int[,] StartOrange = orangeIn.Clone() as int[,];
            int[,] StartYellow = yellowIn.Clone() as int[,];
            String result = "No solutions.";
            String algo = "";
            int exit = FinalLayerCornersProgress(StartWhite, StartRed, StartGreen, StartBlue, StartOrange);

            Random rnd = new Random();
            while (exit < 4)
            {
                var simResult = simulateTurn(StartWhite, StartRed, StartGreen, StartBlue, StartOrange, StartYellow, -1);
                algo = "";
                int count = 0;
                for (int j = 0; j < rnd.Next(0, 4); j++)
                {
                    count++;
                    for (int i = 0; i < rnd.Next(0, 4); i++)
                    {
                        simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, 4);
                        algo += "yellow ";
                    }
                    foreach (String move in fnCorners)
                    {
                        simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, getMoveInt(move));
                        algo += move + " ";
                    }
                    for (int i = 0; i < rnd.Next(0, 4); i++)
                    {
                        simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, 4);
                        algo += "yellow ";
                    }
                    exit = FinalLayerCornersProgress(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5);
                    if (exit >= 4)
                    {
                        result = algo;
                        return result;
                    }
                    if (count > 3)
                    {
                        break;
                    }
                }
            }

            return result;
        }

        private String solveYellowLayer(int[,] whiteIn, int[,] redIn, int[,] greenIn, int[,] blueIn, int[,] orangeIn, int[,] yellowIn)
        {
            int[,] StartWhite = whiteIn.Clone() as int[,];
            int[,] StartRed = redIn.Clone() as int[,];
            int[,] StartGreen = greenIn.Clone() as int[,];
            int[,] StartBlue = blueIn.Clone() as int[,];
            int[,] StartOrange = orangeIn.Clone() as int[,];
            int[,] StartYellow = yellowIn.Clone() as int[,];
            String result = "No solutions.";
            String algo = "";
            int exit = yellowLayerProgress(StartWhite, StartRed, StartGreen, StartBlue, StartOrange, StartYellow);
            int progress;

            Random rnd = new Random();
            while (exit < 8) {
                var simResult = simulateTurn(StartWhite, StartRed, StartGreen, StartBlue, StartOrange, StartYellow, -1);
                algo = "";
                int count = 0;
                for (int j = 0; j < rnd.Next(0, 4); j++)
                {
                    count++;
                    for (int i = 0; i < rnd.Next(0, 4); i++)
                    {
                        simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, 4);
                        algo += "yellow ";
                    }
                    foreach (String move in yellowSide1)
                    {
                        simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, getMoveInt(move));
                        algo += move + " ";
                    }
                    progress = yellowLayerProgress(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6);
                    exit = progress;
                    if (progress >= 8)
                    {
                        result = algo;
                        return result;
                    }
                    if (count > 5)
                    {
                        break;
                    }
                }
            }

            return result;
        }

        private String solveYellowCross(int[,] whiteIn, int[,] redIn, int[,] greenIn, int[,] blueIn, int[,] orangeIn, int[,] yellowIn)
        {
            int[,] StartWhite = whiteIn.Clone() as int[,];
            int[,] StartRed = redIn.Clone() as int[,];
            int[,] StartGreen = greenIn.Clone() as int[,];
            int[,] StartBlue = blueIn.Clone() as int[,];
            int[,] StartOrange = orangeIn.Clone() as int[,];
            int[,] StartYellow = yellowIn.Clone() as int[,];
            String result = "No solutions.";
            int progress;
            int exit = yellowCrossProgress(StartWhite, StartRed, StartGreen, StartBlue, StartOrange, StartYellow);

            for (int i = -1; i < 10; i++)
            {
                for (int j = -1; j < 10; j++)
                {
                    for (int k = -1; k < 10; k++)
                    {
                        for (int l = -1; l < 10; l++)
                        {
                            for (int m = -1; m < 10; m++)
                            {
                                for (int n = -1; n < 10; n++)
                                {
                                    var simResult = simulateTurn(StartWhite, StartRed, StartGreen, StartBlue, StartOrange, StartYellow, i);
                                    simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, j);
                                    simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, k);
                                    simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, l);
                                    simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, m);
                                    simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, n);
                                    progress = yellowCrossProgress(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6);
                                    if (progress > exit)
                                    {
                                        result = getMoveString(i) + getMoveString(j) + getMoveString(k) + getMoveString(l) + getMoveString(m) + getMoveString(n);
                                        return result;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        private String solveSecondRow(int[,] whiteIn, int[,] redIn, int[,] greenIn, int[,] blueIn, int[,] orangeIn, int[,] yellowIn)
        {
            int[,] StartWhite = whiteIn.Clone() as int[,];
            int[,] StartRed = redIn.Clone() as int[,];
            int[,] StartGreen = greenIn.Clone() as int[,];
            int[,] StartBlue = blueIn.Clone() as int[,];
            int[,] StartOrange = orangeIn.Clone() as int[,];
            int[,] StartYellow = yellowIn.Clone() as int[,];
            String result = "";
            int exit = secondRowProgress2(StartWhite, StartRed, StartGreen, StartBlue, StartOrange, StartYellow);
            String[] algorithm = null;
            
            if (StartGreen[2, 1] == 3 && StartYellow[1, 0] == 2)
            {
                algorithm = secondLayer8.Clone() as String[];
            }
            else if (StartGreen[2, 1] == 3 && StartYellow[1, 0] == 5)
            {
                algorithm = secondLayer7.Clone() as String[];
            }
            else if (StartRed[2, 1] == 2 && StartYellow[0, 2] == 4)
            {
                algorithm = secondLayer2.Clone() as String[];
            }
            else if (StartRed[2, 1] == 2 && StartYellow[0, 2] == 3)
            {
                algorithm = secondLayer1.Clone() as String[];
            }
            else if (StartBlue[2, 1] == 4 && StartYellow[1, 2] == 2)
            {
                algorithm = secondLayer3.Clone() as String[];
            }
            else if (StartBlue[2, 1] == 4 && StartYellow[1, 2] == 5)
            {
                algorithm = secondLayer4.Clone() as String[];
            }
            else if (StartOrange[2, 1] == 5 && StartYellow[2, 1] == 3)
            {
                algorithm = secondLayer6.Clone() as String[];
            }
            else if (StartOrange[2, 1] == 5 && StartYellow[2, 1] == 4)
            {
                algorithm = secondLayer5.Clone() as String[];
            }
            else if(StartRed[1, 2] != 2)
            {
                algorithm = secondLayer2.Clone() as String[];
            }
            else if (StartGreen[1, 2] != 3)
            {
                algorithm = secondLayer8.Clone() as String[];
            }
            else if (StartBlue[1, 2] != 4)
            {
                algorithm = secondLayer4.Clone() as String[];
            }
            else if (StartOrange[1, 2] != 5)
            {
                algorithm = secondLayer6.Clone() as String[];
            }
            else if (StartRed[1, 0] != 2)
            {
                algorithm = secondLayer1.Clone() as String[];
            }
            else if (StartGreen[1, 0] != 3)
            {
                algorithm = secondLayer7.Clone() as String[];
            }
            else if (StartBlue[1, 0] != 4)
            {
                algorithm = secondLayer3.Clone() as String[];
            }
            else if (StartOrange[1, 0] != 5)
            {
                algorithm = secondLayer5.Clone() as String[];
            }

            foreach (String move in algorithm) {
                result += move + " ";
            }

            return result;
        }

        private String solveSecondRow2(int[,] whiteIn, int[,] redIn, int[,] greenIn, int[,] blueIn, int[,] orangeIn, int[,] yellowIn)
        {
            int[,] StartWhite = whiteIn.Clone() as int[,];
            int[,] StartRed = redIn.Clone() as int[,];
            int[,] StartGreen = greenIn.Clone() as int[,];
            int[,] StartBlue = blueIn.Clone() as int[,];
            int[,] StartOrange = orangeIn.Clone() as int[,];
            int[,] StartYellow = yellowIn.Clone() as int[,];
            String result = "No solutions.";
            int progress;
            int exit = secondRowProgress2(StartWhite, StartRed, StartGreen, StartBlue, StartOrange, StartYellow);

            for (int i = -1; i < 5; i+=5)
            {
                for (int j = -1; j < 5; j+=5)
                {
                    for (int k = -1; k < 5; k+=5)
                    {
                        var simResult = simulateTurn(StartWhite, StartRed, StartGreen, StartBlue, StartOrange, StartYellow, i);
                        simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, j);
                        simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, k);
                        progress = secondRowProgress2(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6);
                        if (progress > 0)
                        {
                            result = getMoveString(i) + getMoveString(j) + getMoveString(k);
                            return result;
                        }
                    }
                }
            }
            return result;
        }

        private String solveWhiteRow1(int[,] whiteIn, int[,] redIn, int[,] greenIn, int[,] blueIn, int[,] orangeIn, int[,] yellowIn)
        {
            int[,] StartWhite = whiteIn.Clone() as int[,];
            int[,] StartRed = redIn.Clone() as int[,];
            int[,] StartGreen = greenIn.Clone() as int[,];
            int[,] StartBlue = blueIn.Clone() as int[,];
            int[,] StartOrange = orangeIn.Clone() as int[,];
            int[,] StartYellow = yellowIn.Clone() as int[,];
            String result = "No solutions.";
            int progress;
            int exit = whiteRowProgress1(StartWhite, StartRed, StartGreen, StartBlue, StartOrange);

            for (int i = -1; i < 10; i++)
            {
                for (int j = -1; j < 10; j++)
                {
                    for (int k = -1; k < 10; k++)
                    {
                        for (int l = -1; l < 10; l++)
                        {
                            for (int m = -1; m < 10; m++)
                            {
                                var simResult = simulateTurn(StartWhite, StartRed, StartGreen, StartBlue, StartOrange, StartYellow, i);
                                simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, j);
                                simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, k);
                                simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, l);
                                simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, m);
                                progress = whiteRowProgress1(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5);
                                if (progress > exit)
                                {
                                    result = getMoveString(i) + getMoveString(j) + getMoveString(k) + getMoveString(l) + getMoveString(m);
                                    return result;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        private String solveWhiteRow2(int[,] whiteIn, int[,] redIn, int[,] greenIn, int[,] blueIn, int[,] orangeIn, int[,] yellowIn)
        {
            int[,] StartWhite = whiteIn.Clone() as int[,];
            int[,] StartRed = redIn.Clone() as int[,];
            int[,] StartGreen = greenIn.Clone() as int[,];
            int[,] StartBlue = blueIn.Clone() as int[,];
            int[,] StartOrange = orangeIn.Clone() as int[,];
            int[,] StartYellow = yellowIn.Clone() as int[,];
            String result = "No solutions.";
            int progress;
            int exit = whiteRowProgress2(whiteIn, redIn, greenIn, blueIn, orangeIn);

            for (int i = -1; i < 10; i++)
            {
                for (int j = -1; j < 10; j++)
                {
                    for (int k = -1; k < 10; k++)
                    {
                        for (int l = -1; l < 10; l++)
                        {
                            for (int m = -1; m < 10; m++)
                            {
                                var simResult = simulateTurn(StartWhite, StartRed, StartGreen, StartBlue, StartOrange, StartYellow, i);
                                simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, j);
                                simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, k);
                                simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, l);
                                simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, m);
                                progress = whiteRowProgress2(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5);
                                if (progress > exit)
                                {
                                    result = getMoveString(i) + getMoveString(j) + getMoveString(k) + getMoveString(l) + getMoveString(m);
                                    return result;
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        private String solveWhiteCross (int[,] whiteIn, int[,] redIn, int[,] greenIn, int[,] blueIn, int[,] orangeIn, int[,] yellowIn)
        {
            int[,] StartWhite = whiteIn.Clone() as int[,];
            int[,] StartRed = redIn.Clone() as int[,];
            int[,] StartGreen = greenIn.Clone() as int[,];
            int[,] StartBlue = blueIn.Clone() as int[,];
            int[,] StartOrange = orangeIn.Clone() as int[,];
            int[,] StartYellow = yellowIn.Clone() as int[,];
            String result = "No solutions.";
            int progress;
            int exit = whiteCrossProgress(whiteIn[2, 1], whiteIn[1, 0], whiteIn[1, 2], whiteIn[0, 1], redIn[0, 1], greenIn[0, 1], blueIn[0, 1], orangeIn[0, 1]);

            for (int i = -1; i < 10; i++)
            {
                for (int j = -1; j < 10; j++)
                {
                    for (int k = -1; k < 10; k++)
                    {
                        for (int l = -1; l < 10; l++)
                        {
                            for (int m = -1; m < 10; m++)
                            {
                                for (int n = -1; n < 10; n++)
                                {
                                    var simResult = simulateTurn(StartWhite, StartRed, StartGreen, StartBlue, StartOrange, StartYellow, i);
                                    simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, j);
                                    simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, k);
                                    simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, l);
                                    simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, m);
                                    simResult = simulateTurn(simResult.Item1, simResult.Item2, simResult.Item3, simResult.Item4, simResult.Item5, simResult.Item6, n);
                                    progress = whiteCrossProgress(simResult.Item1[2, 1], simResult.Item1[1, 0], simResult.Item1[1, 2], simResult.Item1[0, 1], simResult.Item2[0, 1], simResult.Item3[0, 1], simResult.Item4[0, 1], simResult.Item5[0, 1]);
                                    if (progress > exit)
                                    {
                                        //console(i.ToString() + j.ToString() + k.ToString() + l.ToString() + m.ToString() + n.ToString());
                                        result = getMoveString(i) + getMoveString(j) + getMoveString(k) + getMoveString(l) + getMoveString(m) + getMoveString(n);
                                        return result;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        private int FinalLayerEdgesProgress(int[,] whiteIn, int[,] redIn, int[,] greenIn, int[,] blueIn, int[,] orangeIn)
        {
            int result = 0;

            if (redIn[2, 1] == 2)
            {
                result++;
            }
            if (greenIn[2, 1] == 3)
            {
                result++;
            }
            if (blueIn[2, 1] == 4)
            {
                result++;
            }
            if (orangeIn[2, 1] == 5)
            {
                result++;
            }
            if (FinalLayerCornersProgress(whiteIn, redIn, greenIn, blueIn, orangeIn) != 4)
            {
                result = 0;
            }
            return result;
        }

        private int FinalLayerCornersProgress(int[,] whiteIn, int[,] redIn, int[,] greenIn, int[,] blueIn, int[,] orangeIn)
        {
            int result = 0;

            if (redIn[2, 0] == 2 && greenIn[2, 2] == 3)
            {
                result++;
            }
            if (redIn[2, 2] == 2 && blueIn[2, 0] == 4)
            {
                result++;
            }
            if (orangeIn[2, 0] == 5 && blueIn[2, 2] == 4)
            {
                result++;
            }
            if (orangeIn[2, 2] == 5 && greenIn[2, 0] == 3)
            {
                result++;
            }
            return result;
        }

        private int yellowLayerProgress(int[,] whiteIn, int[,] redIn, int[,] greenIn, int[,] blueIn, int[,] orangeIn, int[,] yellowIn)
        {
            int result = 0;

            if (yellowIn[0, 0] == 6)
            {
                result++;
            }
            if (yellowIn[0, 1] == 6)
            {
                result++;
            }
            if (yellowIn[0, 2] == 6)
            {
                result++;
            }
            if (yellowIn[1, 0] == 6)
            {
                result++;
            }
            if (yellowIn[1, 2] == 6)
            {
                result++;
            }
            if (yellowIn[2, 0] == 6)
            {
                result++;
            }
            if (yellowIn[2, 1] == 6)
            {
                result++;
            }
            if (yellowIn[2, 2] == 6)
            {
                result++;
            }
            if (whiteCrossProgress(whiteIn[2, 1], whiteIn[1, 0], whiteIn[1, 2], whiteIn[0, 1], redIn[0, 1], greenIn[0, 1], blueIn[0, 1], orangeIn[0, 1]) != 4)
            {
                result = 0;
            }
            return result;
        }

        private int yellowCrossProgress(int[,] whiteIn, int[,] redIn, int[,] greenIn, int[,] blueIn, int[,] orangeIn, int[,] yellowIn)
        {
            int result = 0;

            if (yellowIn[0, 1] == 6)
            {
                result++;
            }
            if (yellowIn[1, 0] == 6)
            {
                result++;
            }
            if (yellowIn[1, 2] == 6)
            {
                result++;
            }
            if (yellowIn[2, 1] == 6)
            {
                result++;
            }
            if (whiteCrossProgress(whiteIn[2, 1], whiteIn[1, 0], whiteIn[1, 2], whiteIn[0, 1], redIn[0, 1], greenIn[0, 1], blueIn[0, 1], orangeIn[0, 1]) != 4)
            {
                result = 0;
            }
            return result;
        }

        private int secondRowProgress(int[,] whiteIn, int[,] redIn, int[,] greenIn, int[,] blueIn, int[,] orangeIn)
        {
            int result = 0;

            if (redIn[1, 0] == 2 && greenIn[1, 2] == 3)
            {
                result++;
            }
            if (redIn[1, 2] == 2 && blueIn[1, 0] == 4)
            {
                result++;
            }
            if (orangeIn[1, 0] == 5 && blueIn[1, 2] == 4)
            {
                result++;
            }
            if (orangeIn[1, 2] == 5 && greenIn[1, 0] == 3)
            {
                result++;
            }
            return result;
        }

        private int secondRowProgress2(int[,] whiteIn, int[,] redIn, int[,] greenIn, int[,] blueIn, int[,] orangeIn, int[,] yellowIn)
        {
            int result = 0;

            if (greenIn[2, 1] == 3 && yellowIn[1, 0] == 2)
            {
                result++;
            }
            else if (greenIn[2, 1] == 3 && yellowIn[1, 0] == 5)
            {
                result++;
            }
            else if (redIn[2, 1] == 2 && yellowIn[0, 2] == 4)
            {
                result++;
            }
            else if (redIn[2, 1] == 2 && yellowIn[0, 2] == 3)
            {
                result++;
            }
            else if (blueIn[2, 1] == 4 && yellowIn[1, 2] == 2)
            {
                result++;
            }
            else if (blueIn[2, 1] == 4 && yellowIn[1, 2] == 5)
            {
                result++;
            }
            else if (orangeIn[2, 1] == 5 && yellowIn[2, 1] == 3)
            {
                result++;
            }
            else if (orangeIn[2, 1] == 5 && yellowIn[2, 1] == 4)
            {
                result++;
            }
            return result;
        }

        private int whiteRowProgress1(int[,] whiteIn, int[,] redIn, int[,] greenIn, int[,] blueIn, int[,] orangeIn)
        {
            int result = 0;

            if (whiteIn[2, 0] == 1 && redIn[0, 0] == 2 && greenIn[0, 2] == 3)
            {
                result++;
            }
            if (whiteIn[2, 2] == 1 && redIn[0, 2] == 2 && blueIn[0, 0] == 4)
            {
                result++;
            }
            if (whiteIn[0, 0] == 1 && greenIn[0, 0] == 3 && orangeIn[0, 2] == 5)
            {
                result++;
            }
            if (whiteIn[0, 2] == 1 && blueIn[0, 2] == 4 && orangeIn[0, 0] == 5)
            {
                result++;
            }
            if(whiteCrossProgress(whiteIn[2, 1], whiteIn[1, 0], whiteIn[1, 2], whiteIn[0, 1], redIn[0, 1], greenIn[0, 1], blueIn[0, 1], orangeIn[0, 1]) != 4)
            {
                result = 0;
            }
            return result;
        }

        private int whiteRowProgress2(int[,] whiteIn, int[,] redIn, int[,] greenIn, int[,] blueIn, int[,] orangeIn)
        {
            int result = 0;
            int solvedAlready = whiteRowProgress1(white, red, green, blue, orange);

            if (redIn[2, 0] == 1)
            {
                result++;
            }
            if (redIn[2, 2] == 1)
            {
                result++;
            }
            if (greenIn[2, 0] == 1)
            {
                result++;
            }
            if (greenIn[2, 2] == 1)
            {
                result++;
            }
            if (blueIn[2, 0] == 1)
            {
                result++;
            }
            if (blueIn[2, 2] == 1)
            {
                result++;
            }
            if (orangeIn[2, 0] == 1)
            {
                result++;
            }
            if (orangeIn[2, 2] == 1)
            {
                result++;
            }
            if (whiteCrossProgress(whiteIn[2, 1], whiteIn[1, 0], whiteIn[1, 2], whiteIn[0, 1], redIn[0, 1], greenIn[0, 1], blueIn[0, 1], orangeIn[0, 1]) < 4)
            {
                result = 0;
            }
            if (whiteRowProgress1(whiteIn, redIn, greenIn, blueIn, orangeIn) < solvedAlready)
            {
                result = 0;
            }

            return result;
        }

        private int whiteCrossProgress (int wr, int wg, int wb, int wo, int rw, int gw, int bw, int ow)
        {
            int result = 0;

            if (wr == 1 && rw == 2)
            {
                result++;
            }
            if (wg == 1 && gw == 3)
            {
                result++;
            }
            if (wb == 1 && bw == 4)
            {
                result++;
            }
            if (wo == 1 && ow == 5)
            {
                result++;
            }

            return result;
        }

        private String getMoveString (int move)
        {
            switch (move)
            {
                case 0:
                    return "red ";
                case 1:
                    return "green ";
                case 2:
                    return "blue ";
                case 3:
                    return "orange ";
                case 4:
                    return "yellow ";
                case 5:
                    return "'red ";
                case 6:
                    return "'green ";
                case 7:
                    return "'blue ";
                case 8:
                    return "'orange ";
                case 9:
                    return "'yellow ";
                default:
                    return "";
            }
        }

        private int getMoveInt(String move)
        {
            switch (move)
            {
                case "red":
                    return 0;
                case "green":
                    return 1;
                case "blue":
                    return 2;
                case "orange":
                    return 3;
                case "yellow":
                    return 4;
                case "'red":
                    return 5;
                case "'green":
                    return 6;
                case "'blue":
                    return 7;
                case "'orange":
                    return 8;
                case "'yellow":
                    return 9;
                default:
                    return -1;
            }
        }

        private int calcMoveToAnotherSide (int side, int algo)
        {
            if (side == 1)
            {
                if (algo == 0)
                {
                    return 2; 
                }
                else if (algo == 1)
                {
                    return 0;
                }
                else if (algo == 2)
                {
                    return 3;
                }
                else if (algo == 3)
                {
                    return 1;
                }
                else if (algo == 5)
                {
                    return 7;
                }
                else if (algo == 6)
                {
                    return 5;
                }
                else if (algo == 7)
                {
                    return 8;
                }
                else if (algo == 8)
                {
                    return 6;
                }
            }
            else if (side == 2)
            {
                if (algo == 0)
                {
                    return 3;
                }
                else if (algo == 1)
                {
                    return 2;
                }
                else if (algo == 2)
                {
                    return 1;
                }
                else if (algo == 3)
                {
                    return 0;
                }
                else if (algo == 5)
                {
                    return 8;
                }
                else if (algo == 6)
                {
                    return 7;
                }
                else if (algo == 7)
                {
                    return 6;
                }
                else if (algo == 8)
                {
                    return 5;
                }
            }
            else if (side == 3)
            {
                if (algo == 0)
                {
                    return 1;
                }
                else if (algo == 1)
                {
                    return 3;
                }
                else if (algo == 2)
                {
                    return 0;
                }
                else if (algo == 3)
                {
                    return 2;
                }
                else if (algo == 5)
                {
                    return 6;
                }
                else if (algo == 6)
                {
                    return 8;
                }
                else if (algo == 7)
                {
                    return 5;
                }
                else if (algo == 8)
                {
                    return 7;
                }
            }

            return algo;
        }

        private Tuple<int[,], int[,], int[,], int[,], int[,], int[,]> simulateTurn (int[,] whiteIn, int[,] redIn, int[,] greenIn, int[,] blueIn, int[,] orangeIn, int[,] yellowIn, int turn)
        {
            int[,] newWhite = whiteIn.Clone() as int[,];
            int[,] newRed = redIn.Clone() as int[,];
            int[,] newGreen = greenIn.Clone() as int[,];
            int[,] newBlue = blueIn.Clone() as int[,];
            int[,] newOrange = orangeIn.Clone() as int[,];
            int[,] newYellow = yellowIn.Clone() as int[,];

            if (turn == -1)
            {
                return new Tuple<int[,], int[,], int[,], int[,], int[,], int[,]>(white, red, green, blue, orange, yellow);
            }

            if (turn == 0) //red
            {
                newRed[2, 0] = redIn[0, 0];
                newRed[1, 0] = redIn[0, 1];
                newRed[0, 0] = redIn[0, 2];
                newRed[2, 1] = redIn[1, 0];
                newRed[0, 1] = redIn[1, 2];
                newRed[2, 2] = redIn[2, 0];
                newRed[1, 2] = redIn[2, 1];
                newRed[0, 2] = redIn[2, 2];
                newWhite[2, 2] = blueIn[2, 0];
                newWhite[2, 1] = blueIn[1, 0];
                newWhite[2, 0] = blueIn[0, 0];
                newGreen[0, 2] = whiteIn[2, 2];
                newGreen[1, 2] = whiteIn[2, 1];
                newGreen[2, 2] = whiteIn[2, 0];
                newYellow[0, 0] = greenIn[0, 2];
                newYellow[0, 1] = greenIn[1, 2];
                newYellow[0, 2] = greenIn[2, 2];
                newBlue[0, 0] = yellowIn[0, 2];
                newBlue[1, 0] = yellowIn[0, 1];
                newBlue[2, 0] = yellowIn[0, 0];
            }
            else if (turn == 1) //green
            {
                newGreen[2, 0] = greenIn[0, 0];
                newGreen[1, 0] = greenIn[0, 1];
                newGreen[0, 0] = greenIn[0, 2];
                newGreen[2, 1] = greenIn[1, 0];
                newGreen[0, 1] = greenIn[1, 2];
                newGreen[2, 2] = greenIn[2, 0];
                newGreen[1, 2] = greenIn[2, 1];
                newGreen[0, 2] = greenIn[2, 2];
                newWhite[2, 0] = redIn[2, 0];
                newWhite[1, 0] = redIn[1, 0];
                newWhite[0, 0] = redIn[0, 0];
                newOrange[0, 2] = whiteIn[2, 0];
                newOrange[1, 2] = whiteIn[1, 0];
                newOrange[2, 2] = whiteIn[0, 0];
                newYellow[0, 0] = orangeIn[2, 2];
                newYellow[1, 0] = orangeIn[1, 2];
                newYellow[2, 0] = orangeIn[0, 2];
                newRed[0, 0] = yellowIn[0, 0];
                newRed[1, 0] = yellowIn[1, 0];
                newRed[2, 0] = yellowIn[2, 0];
            }
            else if (turn == 2) //blue
            {
                newBlue[2, 0] = blueIn[0, 0];
                newBlue[1, 0] = blueIn[0, 1];
                newBlue[0, 0] = blueIn[0, 2];
                newBlue[2, 1] = blueIn[1, 0];
                newBlue[0, 1] = blueIn[1, 2];
                newBlue[2, 2] = blueIn[2, 0];
                newBlue[1, 2] = blueIn[2, 1];
                newBlue[0, 2] = blueIn[2, 2];
                newWhite[2, 2] = orangeIn[0, 0];
                newWhite[1, 2] = orangeIn[1, 0];
                newWhite[0, 2] = orangeIn[2, 0];
                newRed[0, 2] = whiteIn[0, 2];
                newRed[1, 2] = whiteIn[1, 2];
                newRed[2, 2] = whiteIn[2, 2];
                newYellow[0, 2] = redIn[0, 2];
                newYellow[1, 2] = redIn[1, 2];
                newYellow[2, 2] = redIn[2, 2];
                newOrange[0, 0] = yellowIn[2, 2];
                newOrange[1, 0] = yellowIn[1, 2];
                newOrange[2, 0] = yellowIn[0, 2];
            }
            else if (turn == 3) //orange
            {
                newOrange[2, 0] = orangeIn[0, 0];
                newOrange[1, 0] = orangeIn[0, 1];
                newOrange[0, 0] = orangeIn[0, 2];
                newOrange[2, 1] = orangeIn[1, 0];
                newOrange[0, 1] = orangeIn[1, 2];
                newOrange[2, 2] = orangeIn[2, 0];
                newOrange[1, 2] = orangeIn[2, 1];
                newOrange[0, 2] = orangeIn[2, 2];
                newWhite[0, 2] = greenIn[0, 0];
                newWhite[0, 1] = greenIn[1, 0];
                newWhite[0, 0] = greenIn[2, 0];
                newBlue[0, 2] = whiteIn[0, 0];
                newBlue[1, 2] = whiteIn[0, 1];
                newBlue[2, 2] = whiteIn[0, 2];
                newYellow[2, 0] = blueIn[2, 2];
                newYellow[2, 1] = blueIn[1, 2];
                newYellow[2, 2] = blueIn[0, 2];
                newGreen[0, 0] = yellowIn[2, 0];
                newGreen[1, 0] = yellowIn[2, 1];
                newGreen[2, 0] = yellowIn[2, 2];
            }
            else if (turn == 4) //yellow
            {
                newYellow[2, 0] = yellowIn[0, 0];
                newYellow[1, 0] = yellowIn[0, 1];
                newYellow[0, 0] = yellowIn[0, 2];
                newYellow[2, 1] = yellowIn[1, 0];
                newYellow[0, 1] = yellowIn[1, 2];
                newYellow[2, 2] = yellowIn[2, 0];
                newYellow[1, 2] = yellowIn[2, 1];
                newYellow[0, 2] = yellowIn[2, 2];
                newRed[2, 2] = blueIn[2, 2];
                newRed[2, 1] = blueIn[2, 1];
                newRed[2, 0] = blueIn[2, 0];
                newGreen[2, 0] = redIn[2, 0];
                newGreen[2, 1] = redIn[2, 1];
                newGreen[2, 2] = redIn[2, 2];
                newOrange[2, 0] = greenIn[2, 0];
                newOrange[2, 1] = greenIn[2, 1];
                newOrange[2, 2] = greenIn[2, 2];
                newBlue[2, 0] = orangeIn[2, 0];
                newBlue[2, 1] = orangeIn[2, 1];
                newBlue[2, 2] = orangeIn[2, 2];
            }
            else if (turn == 5) //'red
            {
                newRed[0, 0] = redIn[2, 0];
                newRed[0, 1] = redIn[1, 0];
                newRed[0, 2] = redIn[0, 0];
                newRed[1, 0] = redIn[2, 1];
                newRed[1, 2] = redIn[0, 1];
                newRed[2, 0] = redIn[2, 2];
                newRed[2, 1] = redIn[1, 2];
                newRed[2, 2] = redIn[0, 2];
                newWhite[2, 0] = greenIn[2, 2];
                newWhite[2, 1] = greenIn[1, 2];
                newWhite[2, 2] = greenIn[0, 2];
                newGreen[2, 2] = yellowIn[0, 2];
                newGreen[1, 2] = yellowIn[0, 1];
                newGreen[0, 2] = yellowIn[0, 0];
                newYellow[0, 2] = blueIn[0, 0];
                newYellow[0, 1] = blueIn[1, 0];
                newYellow[0, 0] = blueIn[2, 0];
                newBlue[0, 0] = whiteIn[2, 0];
                newBlue[1, 0] = whiteIn[2, 1];
                newBlue[2, 0] = whiteIn[2, 2];
            }
            else if (turn == 6) //'green
            {
                newGreen[0, 0] = greenIn[2, 0];
                newGreen[0, 1] = greenIn[1, 0];
                newGreen[0, 2] = greenIn[0, 0];
                newGreen[1, 0] = greenIn[2, 1];
                newGreen[1, 2] = greenIn[0, 1];
                newGreen[2, 0] = greenIn[2, 2];
                newGreen[2, 1] = greenIn[1, 2];
                newGreen[2, 2] = greenIn[0, 2];
                newWhite[0, 0] = orangeIn[2, 2];
                newWhite[1, 0] = orangeIn[1, 2];
                newWhite[2, 0] = orangeIn[0, 2];
                newOrange[0, 2] = yellowIn[2, 0];
                newOrange[1, 2] = yellowIn[1, 0];
                newOrange[2, 2] = yellowIn[0, 0];
                newYellow[0, 0] = redIn[0, 0];
                newYellow[1, 0] = redIn[1, 0];
                newYellow[2, 0] = redIn[2, 0];
                newRed[0, 0] = whiteIn[0, 0];
                newRed[1, 0] = whiteIn[1, 0];
                newRed[2, 0] = whiteIn[2, 0];
            }
            else if (turn == 7) //'blue
            {
                newBlue[0, 0] = blueIn[2, 0];
                newBlue[0, 1] = blueIn[1, 0];
                newBlue[0, 2] = blueIn[0, 0];
                newBlue[1, 0] = blueIn[2, 1];
                newBlue[1, 2] = blueIn[0, 1];
                newBlue[2, 0] = blueIn[2, 2];
                newBlue[2, 1] = blueIn[1, 2];
                newBlue[2, 2] = blueIn[0, 2];
                newWhite[2, 2] = redIn[2, 2];
                newWhite[1, 2] = redIn[1, 2];
                newWhite[0, 2] = redIn[0, 2];
                newRed[2, 2] = yellowIn[2, 2];
                newRed[1, 2] = yellowIn[1, 2];
                newRed[0, 2] = yellowIn[0, 2];
                newYellow[0, 2] = orangeIn[2, 0];
                newYellow[1, 2] = orangeIn[1, 0];
                newYellow[2, 2] = orangeIn[0, 0];
                newOrange[0, 0] = whiteIn[2, 2];
                newOrange[1, 0] = whiteIn[1, 2];
                newOrange[2, 0] = whiteIn[0, 2];
            }
            else if (turn == 8) //'orange
            {
                newOrange[0, 0] = orangeIn[2, 0];
                newOrange[0, 1] = orangeIn[1, 0];
                newOrange[0, 2] = orangeIn[0, 0];
                newOrange[1, 0] = orangeIn[2, 1];
                newOrange[1, 2] = orangeIn[0, 1];
                newOrange[2, 0] = orangeIn[2, 2];
                newOrange[2, 1] = orangeIn[1, 2];
                newOrange[2, 2] = orangeIn[0, 2];
                newWhite[0, 0] = blueIn[0, 2];
                newWhite[0, 1] = blueIn[1, 2];
                newWhite[0, 2] = blueIn[2, 2];
                newBlue[0, 2] = yellowIn[2, 2];
                newBlue[1, 2] = yellowIn[2, 1];
                newBlue[2, 2] = yellowIn[2, 0];
                newYellow[2, 0] = greenIn[0, 0];
                newYellow[2, 1] = greenIn[1, 0];
                newYellow[2, 2] = greenIn[2, 0];
                newGreen[0, 0] = whiteIn[0, 2];
                newGreen[1, 0] = whiteIn[0, 1];
                newGreen[2, 0] = whiteIn[0, 0];
            }
            else if (turn == 9) //'yellow
            {
                newYellow[0, 0] = yellowIn[2, 0];
                newYellow[0, 1] = yellowIn[1, 0];
                newYellow[0, 2] = yellowIn[0, 0];
                newYellow[1, 0] = yellowIn[2, 1];
                newYellow[1, 2] = yellowIn[0, 1];
                newYellow[2, 0] = yellowIn[2, 2];
                newYellow[2, 1] = yellowIn[1, 2];
                newYellow[2, 2] = yellowIn[0, 2];
                newRed[2, 2] = greenIn[2, 2];
                newRed[2, 1] = greenIn[2, 1];
                newRed[2, 0] = greenIn[2, 0];
                newGreen[2, 0] = orangeIn[2, 0];
                newGreen[2, 1] = orangeIn[2, 1];
                newGreen[2, 2] = orangeIn[2, 2];
                newOrange[2, 0] = blueIn[2, 0];
                newOrange[2, 1] = blueIn[2, 1];
                newOrange[2, 2] = blueIn[2, 2];
                newBlue[2, 0] = redIn[2, 0];
                newBlue[2, 1] = redIn[2, 1];
                newBlue[2, 2] = redIn[2, 2];
            }

            var result = new Tuple<int[,], int[,], int[,], int[,], int[,], int[,]>(newWhite, newRed, newGreen, newBlue, newOrange, newYellow);
            return result;
        }

        private void drawCubeSides (int[,] side, PictureBox pb)
        {
            Bitmap bmp = new Bitmap(42, 42, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(bmp);
            SolidBrush sb = new SolidBrush(Color.White);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    switch (side[j, i])
                    {
                        case 1:
                            sb = new SolidBrush(Color.White);
                            break;
                        case 2:
                            sb = new SolidBrush(Color.Red);
                            break;
                        case 3:
                            sb = new SolidBrush(Color.Lime);
                            break;
                        case 4:
                            sb = new SolidBrush(Color.Blue);
                            break;
                        case 5:
                            sb = new SolidBrush(Color.Orange);
                            break;
                        case 6:
                            sb = new SolidBrush(Color.Yellow);
                            break;
                    }
                    graphics.FillRectangle(sb, i*14, j*14, 14, 14);
                }
            }
            pb.Image = bmp;
        }

        private String scanCube (int[,] sideMatrix, PictureBox picBox, int side) {
            int count = 0;
            String result = "";
            int color;

            // white
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if ((i == 1) && (j == 1))
                    {
                        color = side;
                    } else
                    {
                        color = detectColor(recX[count], recY[count], 20, img);                      
                        count++;
                    }
                    sideMatrix[i, j] = color;
                    result += color.ToString() + " ";
                }
            }
            drawCubeSides(sideMatrix, picBox);

            //red
            /*
            count = 0;
            System.Threading.Thread.Sleep(2000);
            serialPort1.WriteLine("green");
            System.Threading.Thread.Sleep(2000);
            serialPort1.WriteLine("'blue");
            System.Threading.Thread.Sleep(2000);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j+=2)
                {
                    color = detectColor(recX[count], recY[count], 20, img);
                    count++;
                    red[i, j] = color;
                    result += color.ToString() + " ";
                }
            }
            serialPort1.WriteLine("'green");
            System.Threading.Thread.Sleep(2000);
            serialPort1.WriteLine("blue");
            System.Threading.Thread.Sleep(2000);
            count = 0;
            serialPort1.WriteLine("red");
            System.Threading.Thread.Sleep(2000);
            serialPort1.WriteLine("green");
            System.Threading.Thread.Sleep(2000);
            serialPort1.WriteLine("'blue");
            System.Threading.Thread.Sleep(2000);
            for (int i = 0; i < 3; i++)
            {
                if (i == 1)
                {
                    color = 2;
                }
                else
                {
                    color = detectColor(recX[count], recY[count], 20, img);
                    count++;
                }
                red[1, i] = color;
                result += color.ToString() + " ";
            }
            serialPort1.WriteLine("'green");
            System.Threading.Thread.Sleep(2000);
            serialPort1.WriteLine("blue");
            System.Threading.Thread.Sleep(2000);
            serialPort1.WriteLine("'red");
            System.Threading.Thread.Sleep(2000);
            count = 0;
            drawCubeSides(red, redBox);
            */

            return result;
        }

        private int detectColor (int x, int y, int len, Image image)
        {

            //image.Save("e:\\myBitmap.bmp");
            int avgRedVal = 0;
            int avgGreenVal = 0;
            int avgBlueVal = 0;
            int count = 0;
            int bestVal = 765;
            int currentVal = 0;
            int result = 0;

            Bitmap bmp = new Bitmap(image);
            for (int i = x; i < x+len; i++)
            {
                for (int j = y; j < y+len; j++)
                {
                    Color color = bmp.GetPixel(i, j);
                    avgRedVal += color.R;
                    avgGreenVal += color.G;
                    avgBlueVal += color.B;
                    count++;
                }
            }
            avgRedVal = avgRedVal / count;
            avgGreenVal = avgGreenVal / count;
            avgBlueVal = avgBlueVal / count;

            for (int i = 0; i < 6; i++)
            {
                currentVal = Math.Abs(colorsRGB[i, 0] - avgRedVal) + Math.Abs(colorsRGB[i, 1] - avgGreenVal) + Math.Abs(colorsRGB[i, 2] - avgBlueVal);
                if (currentVal < bestVal)
                {
                    bestVal = currentVal;
                    result = i + 1;
                }
            }

            if (result == 6 && avgBlueVal > avgGreenVal)
            {
                result = 1;
            } else if (result == 5 && avgGreenVal > avgRedVal)
            {
                result = 6;
            }

            return result;

            /*
            if ((avgBlueVal + avgGreenVal + avgRedVal > 440) && (avgBlueVal > avgGreenVal) && (avgBlueVal > avgRedVal))
            {
                return 1;
            }
            else if ((avgRedVal > avgGreenVal) && (avgRedVal > avgBlueVal) && (avgBlueVal < 70) && (avgBlueVal + avgGreenVal + avgRedVal < 350))
            {
                return 2;
            }
            else if ((avgGreenVal > avgRedVal) && (avgBlueVal > avgRedVal) && (avgBlueVal - avgGreenVal < 45))
            {
                return 3;
            }
            else if (avgBlueVal > avgGreenVal + avgRedVal)
            {
                return 4;
            }
            else if ((avgBlueVal + avgGreenVal + avgRedVal > 350) && (avgRedVal - avgGreenVal > 45))
            {
                return 5;
            }
            else if ((avgBlueVal + avgGreenVal + avgRedVal > 370) && (avgRedVal - avgGreenVal < 45))
            {
                return 6;
            } else
            {
                return 0;
            }
            */
        }

        /*private int detectColor2(int x, int y, int len, Image image)
        {

            //image.Save("e:\\myBitmap.bmp");
            int avgRedVal = 0;
            int avgGreenVal = 0;
            int avgBlueVal = 0;
            int count = 0;
            int bestVal = 765;
            int currentVal = 0;
            int result = 0;

            Bitmap bmp = new Bitmap(image);
            for (int i = x; i < x + len; i++)
            {
                for (int j = y; j < y + len; j++)
                {
                    Color color = bmp.GetPixel(i, j);
                    avgRedVal += color.R;
                    avgGreenVal += color.G;
                    avgBlueVal += color.B;
                    count++;
                }
            }
            avgRedVal = avgRedVal / count;
            avgGreenVal = avgGreenVal / count;
            avgBlueVal = avgBlueVal / count;


            if ((avgBlueVal + avgGreenVal + avgRedVal > 440) && (avgBlueVal > avgGreenVal) && (avgBlueVal > avgRedVal))
            {
                return 1;
            }
            else if ((avgRedVal > avgGreenVal) && (avgRedVal > avgBlueVal) && (avgBlueVal < 70) && (avgBlueVal + avgGreenVal + avgRedVal < 350))
            {
                return 2;
            }
            else if ((avgGreenVal > avgRedVal) && (avgBlueVal > avgRedVal) && (avgBlueVal - avgGreenVal < 45))
            {
                return 3;
            }
            else if (avgBlueVal > avgGreenVal + avgRedVal)
            {
                return 4;
            }
            else if ((avgBlueVal + avgGreenVal + avgRedVal > 350) && (avgRedVal - avgGreenVal > 45))
            {
                return 5;
            }
            else if ((avgBlueVal + avgGreenVal + avgRedVal > 370) && (avgRedVal - avgGreenVal < 45))
            {
                return 6;
            } else
            {
                return 0;
            }
        }*/

        private void getInfo ()
        {
            var cameraDevices = myCam.GetCameraSources();
            foreach (var device in cameraDevices)
            {
                comboBoxCam.Items.Add(device);
            }
        }

        private void myCamOnFrameArrived (Object source, FrameArrivedEventArgs e)
        {
            img = e.GetFrame();
            camBox.Image = img;
            Graphics g = Graphics.FromImage(img);
            g.DrawRectangle(Pens.Black, recX[0], recY[0], 20, 20);
            g.DrawRectangle(Pens.Black, recX[1], recY[1], 20, 20);
            g.DrawRectangle(Pens.Black, recX[2], recY[2], 20, 20);
            g.DrawRectangle(Pens.Black, recX[3], recY[3], 20, 20);
            g.DrawRectangle(Pens.Black, recX[4], recY[4], 20, 20);
            g.DrawRectangle(Pens.Black, recX[5], recY[5], 20, 20);
            g.DrawRectangle(Pens.Black, recX[6], recY[6], 20, 20);
            g.DrawRectangle(Pens.Black, recX[7], recY[7], 20, 20);
        }

        private void getPorts ()
        {
            String[] ports = SerialPort.GetPortNames();
            comboBoxPort.Items.AddRange(ports);
        }

        private void console (String input)
        {
            try
            {
                BeginInvoke(new EventHandler(delegate
                {
                    textBoxReceive.Text += "-> " + input + "\r\n";
                    textBoxReceive.SelectionStart = textBoxReceive.TextLength;
                    textBoxReceive.ScrollToCaret();
                }));
            } catch (Exception)
            {
                textBoxReceive.Text += "-> " + input + "\r\n";
                textBoxReceive.SelectionStart = textBoxReceive.TextLength;
                textBoxReceive.ScrollToCaret();
            }
            
        }

        async Task UseDelay(int delay)
        {
            await Task.Delay(delay);
        }

        // buttons and stuff
        private void buttonOP_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxPort.Text == "" || comboBoxBR.Text == "")
                {
                    textBoxReceive.Text = "Please select port settings";
                } else
                {
                    serialPort1.PortName = comboBoxPort.Text;
                    serialPort1.BaudRate = Convert.ToInt32(comboBoxBR.Text);
                    serialPort1.Open();
                    ConnStatus.Text = "Status: Connected";
                    textBoxReceive.Enabled = true;
                    textBoxSend.Enabled = true;
                    buttonOP.Enabled = false;
                    buttonCP.Enabled = true;

                }
            } catch (UnauthorizedAccessException)
            {
                console("Unauthorized access!");
            }
        }

        private void buttonCP_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            ConnStatus.Text = "Status: Closed";
            textBoxReceive.Enabled = false;
            textBoxSend.Enabled = false;
            buttonOP.Enabled = true;
            buttonCP.Enabled = false;
        }

        private void textBoxSend_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                String s = textBoxSend.Text.Replace(System.Environment.NewLine, "");
                if (s != "")
                {
                    console(s);
                    if (s.Equals("scan"))
                    {
                        console("Automatic scan is disabled for now.");
                    }
                    else if (s.Substring(0, 1).Equals("c"))
                    {
                        if (s.Substring(0, 6).Equals("cwhite"))
                        {
                            white[Convert.ToInt32(s.Substring(6, 1)), Convert.ToInt32(s.Substring(7, 1))] = Convert.ToInt32(s.Substring(8, 1));
                            drawCubeSides(white, whiteBox);
                        }
                        else if (s.Substring(0, 4).Equals("cred"))
                        {
                            red[Int32.Parse(s.Substring(4, 1)), Int32.Parse(s.Substring(5, 1))] = Int32.Parse(s.Substring(6, 1));
                            drawCubeSides(red, redBox);
                        }
                        else if (s.Substring(0, 6).Equals("cgreen"))
                        {
                            green[Int32.Parse(s.Substring(6, 1)), Int32.Parse(s.Substring(7, 1))] = Int32.Parse(s.Substring(8, 1));
                            drawCubeSides(green, greenBox);
                        }
                        else if (s.Substring(0, 5).Equals("cblue"))
                        {
                            blue[Int32.Parse(s.Substring(5, 1)), Int32.Parse(s.Substring(6, 1))] = Int32.Parse(s.Substring(7, 1));
                            drawCubeSides(blue, blueBox);
                        }
                        else if (s.Substring(0, 7).Equals("corange"))
                        {
                            orange[Int32.Parse(s.Substring(7, 1)), Int32.Parse(s.Substring(8, 1))] = Int32.Parse(s.Substring(9, 1));
                            drawCubeSides(orange, orangeBox);
                        }
                        else if (s.Substring(0, 7).Equals("cyellow"))
                        {
                            yellow[Int32.Parse(s.Substring(7, 1)), Int32.Parse(s.Substring(8, 1))] = Int32.Parse(s.Substring(9, 1));
                            drawCubeSides(yellow, yellowBox);
                        }
                    }
                    else if (s.Equals("solution"))
                    {
                        console(solve(white, red, green, blue, orange, yellow));
                    }
                    else if (s.Equals("solve"))
                    {
                        solutionMoves = solution.Split(' ');
                        sendMove(solutionIndex);
                    }
                    else if (s.Equals("test"))
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 20; i++)
                        {
                            var res = simulateTurn(white, red, green, blue, orange, yellow, rnd.Next(0, 10));
                            white = res.Item1;
                            red = res.Item2;
                            green = res.Item3;
                            blue = res.Item4;
                            orange = res.Item5;
                            yellow = res.Item6;
                        }

                        drawCubeSides(white, whiteBox);
                        drawCubeSides(red, redBox);
                        drawCubeSides(green, greenBox);
                        drawCubeSides(blue, blueBox);
                        drawCubeSides(orange, orangeBox);
                        drawCubeSides(yellow, yellowBox);
                    }
                    else
                    {
                        serialPort1.WriteLine(s);
                    }
                    textBoxSend.Text = "";
                }
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string received = serialPort1.ReadLine();
                received = received.Trim();

                console("Received: " + received);
                if (received.Equals("done", StringComparison.OrdinalIgnoreCase))
                {
                    sendMove(solutionIndex);
                }
            }
            catch (TimeoutException)
            {
                console("Timeout exception!");
            }
        }

        private void comboBoxCam_SelectedIndexChanged(object sender, EventArgs e)
        {
            myCam.ChangeCamera(comboBoxCam.SelectedIndex);
            myCam.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ok)
            {
                myCam.Stop();
            }
        }

        private void whiteBox_Click(object sender, EventArgs e)
        {
            console(scanCube(white, whiteBox, 1));
        }

        private void redBox_Click(object sender, EventArgs e)
        {
            console(scanCube(red, redBox, 2));
        }

        private void greenBox_Click(object sender, EventArgs e)
        {
            console(scanCube(green, greenBox, 3));
        }

        private void blueBox_Click(object sender, EventArgs e)
        {
            console(scanCube(blue, blueBox, 4));
        }

        private void orangeBox_Click(object sender, EventArgs e)
        {
            console(scanCube(orange, orangeBox, 5));
        }

        private void yellowBox_Click(object sender, EventArgs e)
        {
            console(scanCube(yellow, yellowBox, 6));
        }
    }
}
