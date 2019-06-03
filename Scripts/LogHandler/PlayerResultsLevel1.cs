using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerResultsLevel1 : MonoBehaviour {

    private VehicleControllerMSACC VehicleController;
    private TiltInput TiltCalculations;
    private AxisTouchButton AxisCalculations;
    private ButtonHandler ButtonFrein;
    private NextScene Level;
    private ReactionTime reactionTime;
    private ObstacleManager obstacles;
    private ObstacleBubbleManager obstacleBubble;
    public SavedPlayerInfo savedPlayerInfo;
    bool isLoadedFromFile;
    private int id = 1;
    private CheckObstacleinfo checkbub;
    private DateTime now;
    private RoueQuiiteLaroute NbrSortie;
    private CalculNbrTropProche NbrTouche;
    private string dateSession;

    private List<string[]> rowData = new List<string[]>();

    // Use this for initialization

    private void Awake()
    {
        savedPlayerInfo = GameObject.FindObjectOfType<SavedPlayerInfo>();
    }
    void Start()
    {
        VehicleController = GameObject.FindObjectOfType<VehicleControllerMSACC>();
        TiltCalculations = GameObject.FindObjectOfType<TiltInput>();
        AxisCalculations = GameObject.FindObjectOfType<AxisTouchButton>();
        Level = GameObject.FindObjectOfType<NextScene>();
        reactionTime = GameObject.FindObjectOfType<ReactionTime>();
        obstacles = GameObject.FindObjectOfType<ObstacleManager>();
        obstacleBubble = GameObject.FindObjectOfType<ObstacleBubbleManager>();
        checkbub = GameObject.FindObjectOfType<CheckObstacleinfo>();
        NbrSortie = GameObject.FindObjectOfType<RoueQuiiteLaroute>();
        ButtonFrein = GameObject.FindObjectOfType<ButtonHandler>();
        NbrTouche = GameObject.FindObjectOfType<CalculNbrTropProche>();
        now = DateTime.Now;
        dateSession = now.ToString("yyyy-MM-dd HH:mm:ss");
    }


        void Save()
    {
        isLoadedFromFile = false;
        if (File.Exists(getPath()))
        {
            LoadDocument(getPath());
        }

        if (isLoadedFromFile)
        {
            Debug.Log("loaded from file");
            string[] rowDataTemp = new string[rowData[0].Length];
            for (int i = 0; i < 1; i++)
            {
                rowDataTemp[i] = "";
                rowData.Add(rowDataTemp);

            }
            for (int i = 0; i < 1; i++)
            {

                rowDataTemp = new string[25];
                rowDataTemp[0] = savedPlayerInfo.returnPlayerName(); // name
				rowDataTemp[1] = savedPlayerInfo.returnPlayerDOB(); // ID
				rowDataTemp[2] = savedPlayerInfo.returnPlayerGroup(); // Group
				rowDataTemp[3] = savedPlayerInfo.returnPlayerLaterality(); // Laterality
                rowDataTemp[4] = string.Format("{0}", VehicleController.returnAverageSpeed()); // Average Speed
                rowDataTemp[5] = string.Format("{0}", NbrSortie.RencontreRouteGauche()); // Average Angle
                rowDataTemp[6] = string.Format("{0}", NbrSortie.RencontreRouteDroite()); // Average Angle
                rowDataTemp[7] = string.Format("{0}", AxisCalculations.ReturnPressCountACC()); // Acc button presses
                rowDataTemp[8] = string.Format("{0}", ButtonFrein.ReturnButtonCounterBrake()); // Dec button presses
                rowDataTemp[9] = string.Format("{0}", VehicleController.returnAverageSpeedOZ()); // AverageOZ Speed
                rowDataTemp[10] = string.Format("{0}", reactionTime.returnReactionTime()); // Reaction Time
                rowDataTemp[11] = string.Format("{0}", obstacles.returnPedestrianRHit()); // No. of coll. w/h Ped R
                rowDataTemp[12] = string.Format("{0}", obstacles.returnPedestrianLhit()); // No. of coll. w/h Ped L
                rowDataTemp[13] = string.Format("{0}", obstacles.returnAnimalRHit()); // No. of coll. w/h Ani R
                rowDataTemp[14] = string.Format("{0}", obstacles.returnAnimalLHit()); // No. of coll. w/h Ani L
                rowDataTemp[15] = string.Format("{0}", obstacles.TotalObRHit()); // Total coll. w/h Obstacles R
                rowDataTemp[16] = string.Format("{0}", obstacles.TotalObLHit()); // Total coll. w/h Obstacles L
                rowDataTemp[17] = checkbub.ObstacleHitMiss(); // Obstacle tag
                rowDataTemp[18] = checkbub.ObstacleBubbleHitMiss(); // Obstacle bubble
                rowDataTemp[19] = savedPlayerInfo.level(); // Level*/
                rowDataTemp[20] = savedPlayerInfo.jourNuit(); // Jour/Nuit
                rowDataTemp[22] = NbrSortie.TimerLeft();
                rowDataTemp[23] = NbrSortie.TimerRight();
                rowDataTemp[24] = string.Format("{0}", NbrTouche.ReturnNbrFoisTouche());

                rowData.Add(rowDataTemp);

            }

        }
        else
        {

            // Creating First row of titles manually..
            string[] rowDataTemp = new string[25];
            rowDataTemp[0] = "PlayerName";
			rowDataTemp[1] = "DOB";
			rowDataTemp[2] = "Group";
			rowDataTemp[3] = "Laterality";
            rowDataTemp[4] = "Average Speed (Km/h)";
            rowDataTemp[5] = "Nbr out the road left";
            rowDataTemp[6] = "Nbr out the road right";
            rowDataTemp[7] = "Acceleration Button";
            rowDataTemp[8] = "Brake Button";
            rowDataTemp[9] = "Average Speed Inside the OZ (km/h)";
            rowDataTemp[10] = "Average Reaction Time (s)";
            rowDataTemp[11] = "No. of coll. w/h Ped R";
            rowDataTemp[12] = "No. of coll. w/h Ped L";
            rowDataTemp[13] = "No. of coll. w/h Ani R";
            rowDataTemp[14] = "No. of coll. w/h Ani L";
            rowDataTemp[15] = "Total coll. w/h Obstacles R ";
            rowDataTemp[16] = "Total coll. w/h Obstacles L ";
            rowDataTemp[17] = "Obstacle Hit";
            rowDataTemp[18] = "Obstacle Bubble Hit";
            rowDataTemp[19] = "Level";
            rowDataTemp[20] = "Jour/Nuit";
            rowDataTemp[22] = "Temps deviation gauche";
            rowDataTemp[23] = "Temps deviation droite";
            rowDataTemp[24] = "Nbr de fois trop proche de la voiture";

            rowData.Add(rowDataTemp);


            // You can add up the values in as many cells as you want.
            for (int i = 0; i < 1; i++)
            {

                rowDataTemp = new string[25];
                rowDataTemp[0] = savedPlayerInfo.returnPlayerName(); // name
                rowDataTemp[1] = savedPlayerInfo.returnPlayerDOB(); // ID
                rowDataTemp[2] = savedPlayerInfo.returnPlayerGroup(); // Group
                rowDataTemp[3] = savedPlayerInfo.returnPlayerLaterality(); // Laterality
                rowDataTemp[4] = string.Format("{0}", VehicleController.returnAverageSpeed()); // Average Speed
                rowDataTemp[5] = string.Format("{0}", NbrSortie.RencontreRouteGauche()); // Average Angle
                rowDataTemp[6] = string.Format("{0}", NbrSortie.RencontreRouteDroite()); // Average Angle
                rowDataTemp[7] = string.Format("{0}", AxisCalculations.ReturnPressCountACC()); // Acc button presses
                rowDataTemp[8] = string.Format("{0}", ButtonFrein.ReturnButtonCounterBrake()); // Dec button presses
                rowDataTemp[9] = string.Format("{0}", VehicleController.returnAverageSpeedOZ()); // AverageOZ Speed
                rowDataTemp[10] = string.Format("{0}", reactionTime.returnReactionTime()); // Reaction Time
                rowDataTemp[11] = string.Format("{0}", obstacles.returnPedestrianRHit()); // No. of coll. w/h Ped R
                rowDataTemp[12] = string.Format("{0}", obstacles.returnPedestrianLhit()); // No. of coll. w/h Ped L
                rowDataTemp[13] = string.Format("{0}", obstacles.returnAnimalRHit()); // No. of coll. w/h Ani R
                rowDataTemp[14] = string.Format("{0}", obstacles.returnAnimalLHit()); // No. of coll. w/h Ani L
                rowDataTemp[15] = string.Format("{0}", obstacles.TotalObRHit()); // Total coll. w/h Obstacles R
                rowDataTemp[16] = string.Format("{0}", obstacles.TotalObLHit()); // Total coll. w/h Obstacles L
                rowDataTemp[17] = checkbub.ObstacleHitMiss(); // Obstacle tag
                rowDataTemp[18] = checkbub.ObstacleBubbleHitMiss(); // Obstacle bubble
                rowDataTemp[19] = savedPlayerInfo.level(); // Level*/
                rowDataTemp[20] = savedPlayerInfo.jourNuit(); // Jour/Nuit
                rowDataTemp[22] = NbrSortie.TimerLeft();
                rowDataTemp[23] = NbrSortie.TimerRight();
                rowDataTemp[24] = string.Format("{0}", NbrTouche.ReturnNbrFoisTouche());

                rowData.Add(rowDataTemp);
            }

        }
        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        string filePath = getPath();

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

   

   

    private void OnDestroy()
	{
		Save();
	}


    public void LoadDocument(string path)
	{
		using (StreamReader stream = new StreamReader(path, Encoding.UTF8))
		{
			rowData.Clear();

			string line;
			// Read and display lines from the file until the end of
			// the file is reached.
			while ((line = stream.ReadLine()) != null)
			{
				if (line != "")
				{
					string[] split = line.Split(',');
					rowData.Add(split);
				}
			}
			if (rowData.Count > 0)
				isLoadedFromFile = true;
		}
	}

	// Following method is used to retrive the relative path as device platform
	private string getPath()
	{
		#if UNITY_EDITOR
				return Application.dataPath + "/CSV/" + "Data_Level1.csv";
		#elif UNITY_ANDROID
		        return Application.persistentDataPath+"Saved_data.csv";
		#elif UNITY_IPHONE
		      return Application.persistentDataPath+"/"+"Saved_data.csv";
		#else
		       return Application.dataPath +"/"+"Saved_data.csv";
		#endif
		return Application.persistentDataPath + "Data_Level1.csv";
	}

	


}
