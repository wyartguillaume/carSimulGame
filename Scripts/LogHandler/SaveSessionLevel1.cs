using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class SaveSessionLevel1 : MonoBehaviour {



    private VehicleControllerMSACC VehicleController;
    private TiltInput TiltCalculations;
    private AxisTouchButton AxisCalculations;
    private ButtonHandler ButtonFrein;
    private NextScene Level;
    private ReactionTime reactionTime;
    private ObstacleManager obstacles;
    private ObstacleBubbleManager obstacleBubble;
    private CheckObstacleinfo checkbub;
    private DateTime now;
    private RoueQuiiteLaroute NbrSortie;
    private CalculNbrTropProche NbrTouche;
    private string dateSession;
    public SavedPlayerInfo savedPlayerInfo;
    private RecupIdPatient recupId;

    private void Start()
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
        recupId = GameObject.FindObjectOfType<RecupIdPatient>();

        dateSession = now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    private void Awake()
    {
        savedPlayerInfo = GameObject.FindObjectOfType<SavedPlayerInfo>();
    }

    public void CreateSession()
    {

        StartCoroutine(InscrptionSession());
    }

    IEnumerator InscrptionSession()
    {
        WWWForm form = new WWWForm();
        form.AddField("vitesseMoyenne", string.Format("{0}", VehicleController.returnAverageSpeed()));
        form.AddField("nbrAcceleration", string.Format("{0}", AxisCalculations.ReturnPressCountACC()));
        form.AddField("nbrBrake", string.Format("{0}", ButtonFrein.ReturnButtonCounterBrake()));
        form.AddField("dateSession", dateSession);
        form.AddField("RencontreRouteGauche", string.Format("{0}", NbrSortie.RencontreRouteGauche())); // Average Angle
        form.AddField("RencontreRouteDroite", string.Format("{0}", NbrSortie.RencontreRouteDroite())); // Average Angle
        form.AddField("Level", savedPlayerInfo.level()); // Level
        form.AddField("ChoixJourNuit", savedPlayerInfo.jourNuit()); // Jour/Nuit
        form.AddField("TempsSortieGauche", NbrSortie.TimerLeft());
        form.AddField("TempsSortieDroite", NbrSortie.TimerRight());


        using (UnityWebRequest www = UnityWebRequest.Post("https://cognitivedrive.be/gameCreateSessionLevel1/" + recupId.recup(), form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                Debug.Log("Ajout session");
            }
        }
    }

}
