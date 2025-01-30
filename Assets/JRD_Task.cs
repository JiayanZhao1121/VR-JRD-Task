using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class JRD_Task : MonoBehaviour
{
   
    public GameObject Instruction;
    public Transform cameraTransform;
    public Transform rightHandTransform;
    private GameObject StandingLandmarkGameobject;
    private GameObject FacingLandmarkGameobject;
    private GameObject TargetLandmarkGameobject;
    public GameObject LandmarkListGameobject;
    private float PointingLatency = 0f;
    public GameObject ground;
    public int roundIntervalTime = 30;
    public int waitForEndingTaskTime = 30;

    private float startTime = 0f;

    // Instruction UI
    public CanvasGroup PointingInstructionCanvasGroup;
    public CanvasGroup TrialInstructionCanvasGroup;

    public Text PointingInstructionText;
    public Text TrialInstructionText;



    private int stage = 0;

    // organize JRD trials
    
    private List<int[]> JRDTrialList = new List<int[]>();

    private int TrialNumberPerRound = 0;
    private int RoundNumber = 0;

    private int TrialNumberPerRoundCount = 0;
    private int RoundNumberCount = 1;

    private bool isAllRoundsCompleted = false;

    private List<int> HiddenJRDSingleRoundTrialList = new List<int>();
    private List<int> RandomList = new List<int>();

    //a new class that contains all elements needed for data output
    public class PointingDataCombo
    {
        //data outputs
        public string participant;
        public string standingLandmark;
        public string facingLandmark;
        public string targetLandmark;
        public string latency;
        public string roundNumber;
        public string trialNumber;
        public string horizontalTargetAngle;
        public string horizontalJRDFacingAngle;
        public string horizontalJRDCorrectAngle;
        public string horizontalPointingAngle;
        public string horizontalPointingError;
        public string horizontalAbsPointingError;
        public string verticalTargetAngle;
        public string verticalPointingAngle;
        public string verticalPointingError;
        public string verticalAbsPointingError;


        public PointingDataCombo(string parti, string standingL, string facingL, string targetL,
            string lat, string roundNum,string trialNum,
            string horizTargetAng, string horizJRDFacingAng, string horizJRDCorrectAng, 
            string horizPointingAng, string horizPointingErr, string horizAbsPointingErr, 
            string vertTargetAng, string vertPointingAng, string vertPointingErr, 
            string vertAbsPointingErr)
        {
            participant = parti;
            standingLandmark = standingL;
            facingLandmark = facingL;
            targetLandmark = targetL;
            latency = lat;
            roundNumber = roundNum;
            trialNumber = trialNum;
            horizontalTargetAngle = horizTargetAng;
            horizontalJRDFacingAngle = horizJRDFacingAng;
            horizontalJRDCorrectAngle = horizJRDCorrectAng;
            horizontalPointingAngle = horizPointingAng;
            horizontalPointingError = horizPointingErr;
            horizontalAbsPointingError = horizAbsPointingErr;
            verticalTargetAngle = vertTargetAng;
            verticalPointingAngle = vertPointingAng;
            verticalPointingError = vertPointingErr;
            verticalAbsPointingError= vertAbsPointingErr;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
       
        // organize landmarks for the JRD task here
        int[] round1_JRDTrials = { 0,1,3,3,4,2,1,0,4};
        int[] round2_JRDTrials = {8,9,12,8,9,13,5,6,13,10,11,15,5,6,12,8,9,16,8,9,7,14,15,13,14,15,16,5,6,7,5,6,8,5,6,9,10,11,5,14,15,7,14,15,8,14,15,9,10,11,8,10,11,9,5,7,10,7,6,14,6,7,15,7,5,13,7,5,12,15,16,13,5,7,8,7,5,9,16,14,13,7,6,5,14,16,15,15,16,6,14,16,10,16,14,11,16,15,7,15,16,8,16,15,9};

        JRDTrialList.Add(round1_JRDTrials);
        JRDTrialList.Add(round2_JRDTrials);

        //TrialNumberPerRound = round1_JRDTrials.Length / 3;
        RoundNumber = JRDTrialList.Count;

        

        

      
    }

    // Update is called once per frame
    void Update()
    {
       
       
         if (stage == 0)
        {
            TrialNumberPerRound = JRDTrialList[RoundNumberCount - 1].Length / 3;
            //Randomize trials
            for (int i = 0; i < TrialNumberPerRound; i++)
            {
                RandomList.Add(i * 3);
            }
            for (int i = 0; i < RandomList.Count; i++)
            {
                int temp = RandomList[i];
                int randomIndex = Random.Range(i, RandomList.Count);
                RandomList[i] = RandomList[randomIndex];
                RandomList[randomIndex] = temp;
            }
            for (int i = 0; i < RandomList.Count; i++)
            {
                HiddenJRDSingleRoundTrialList.Add(
                    JRDTrialList[RoundNumberCount - 1][RandomList[i]]);
                HiddenJRDSingleRoundTrialList.Add(
                    JRDTrialList[RoundNumberCount - 1][RandomList[i] + 1]);
                HiddenJRDSingleRoundTrialList.Add(
                    JRDTrialList[RoundNumberCount - 1][RandomList[i] + 2]);
            }
            JRDTrialList[RoundNumberCount - 1] = HiddenJRDSingleRoundTrialList.ToArray();
            //

            startTime = Time.time;
            stage++;
        }
        else if (stage == 1)
        {
            if (Time.time - startTime > 3f)
            {
                stage++;
            }
        } 

        Debug.Log("stage = " + stage);
        Debug.Log("TrialNumberPerRoundCount = " + TrialNumberPerRoundCount);
        if (stage == 2 & !isAllRoundsCompleted)
        {
            TrialNumberPerRoundCount++;
            if (TrialNumberPerRoundCount > TrialNumberPerRound)
            {
                TrialInstructionText.text = "Trials completed!\r\n\r\n" +
                       "Please take off your headset and inform the experimenter.";
                if (RoundNumberCount < RoundNumber)
                {
                    RoundNumberCount++;
                    TrialNumberPerRoundCount = 1;
                    //wait for starting the next round
                    stage = 30;

                }
                else
                {
                    //end the task
                    isAllRoundsCompleted = true;
                    stage = 40;
                }

            }
            else
            {
                // go to the next pointing trail
                stage++;
            }



        }
        //go to the next pointing trial
        else if (stage == 3)
        {
            if (RoundNumberCount <= RoundNumber
                && TrialNumberPerRoundCount <= TrialNumberPerRound)
            {
                StandingLandmarkGameobject = LandmarkListGameobject.transform.
                    GetChild(JRDTrialList[RoundNumberCount - 1][(TrialNumberPerRoundCount - 1) * 3]).gameObject;
                FacingLandmarkGameobject = LandmarkListGameobject.transform.
                    GetChild(JRDTrialList[RoundNumberCount - 1][(TrialNumberPerRoundCount - 1) * 3 + 1]).gameObject;
                TargetLandmarkGameobject = LandmarkListGameobject.transform.
                    GetChild(JRDTrialList[RoundNumberCount - 1][(TrialNumberPerRoundCount - 1) * 3 + 2]).gameObject;
                stage++;
            }
        }
        //show instruction
        else if (stage == 4)
        {
            PointingInstructionText.text =
                "Imagine you are at the " + StandingLandmarkGameobject.name +
                " and facing the " + FacingLandmarkGameobject.name + ".\r\n\r\n" +
                "Point to the " + TargetLandmarkGameobject.name + ", then press the trigger button.";

            Instruction.transform.position = cameraTransform.position;
            ground.transform.position = new Vector3(cameraTransform.position.x,
               ground.transform.position.y, cameraTransform.position.z);
            Instruction.transform.eulerAngles = new Vector3(0f, cameraTransform.localEulerAngles.y, 0f);
            ground.transform.eulerAngles = new Vector3(0f, cameraTransform.localEulerAngles.y, 0f);



            Instruction.transform.Find("Canvas").Find("PointingInstruction").gameObject.SetActive(true);
            stage++;
        }
        else if (stage == 5)
        {
            if (PointingInstructionCanvasGroup.alpha < 1)
            {
                PointingInstructionCanvasGroup.alpha += Time.deltaTime;
                if (PointingInstructionCanvasGroup.alpha >= 1)
                {
                    startTime = Time.time;
                    rightHandTransform.
                        GetComponent<UnityEngine.XR.Interaction.Toolkit.XRInteractorLineVisual>()
                        .enabled = true;
                    stage++;
                }
            }

        }
        //perform pointing
        else if (stage == 6 && PlayerPrefs.GetString("Trigger states") == "Trigger pressed")
        {
            PlayerPrefs.SetString("Trigger states", "Null");
            PointingLatency = Time.time - startTime;
            Instruction.transform.Find("Canvas").Find("PointingInstruction").gameObject.SetActive(false);
            PointingInstructionCanvasGroup.alpha = 0f;

            PointFromTo(StandingLandmarkGameobject.transform, FacingLandmarkGameobject.transform,
              TargetLandmarkGameobject.transform);
            rightHandTransform.
                       GetComponent<UnityEngine.XR.Interaction.Toolkit.XRInteractorLineVisual>()
                       .enabled = false;
            stage = 2;
        }

        //wait for the next round
        else if (stage == 30)
        {
            TrialNumberPerRound = JRDTrialList[RoundNumberCount - 1].Length / 3;

            //Randomize trials
            RandomList = new List<int>();
            HiddenJRDSingleRoundTrialList = new List<int>();
            for (int i = 0; i < TrialNumberPerRound; i++)
            {
                RandomList.Add(i * 3);
            }
            for (int i = 0; i < RandomList.Count; i++)
            {
                int temp = RandomList[i];
                int randomIndex = Random.Range(i, RandomList.Count);
                RandomList[i] = RandomList[randomIndex];
                RandomList[randomIndex] = temp;
            }
            for (int i = 0; i < RandomList.Count; i++)
            {
                HiddenJRDSingleRoundTrialList.Add(
                    JRDTrialList[RoundNumberCount - 1][RandomList[i]]);
                HiddenJRDSingleRoundTrialList.Add(
                    JRDTrialList[RoundNumberCount - 1][RandomList[i] + 1]);
                HiddenJRDSingleRoundTrialList.Add(
                    JRDTrialList[RoundNumberCount - 1][RandomList[i] + 2]);
            }
            JRDTrialList[RoundNumberCount - 1] = HiddenJRDSingleRoundTrialList.ToArray();
            //

            Instruction.transform.position = cameraTransform.position;

            Instruction.transform.eulerAngles = new Vector3(0f, cameraTransform.localEulerAngles.y, 0f);

            Instruction.transform.Find("Canvas").Find("TrialInstruction").gameObject.SetActive(true);
            stage++;
        }
        else if (stage == 31)
        {
            if (TrialInstructionCanvasGroup.alpha < 1)
            {
                TrialInstructionCanvasGroup.alpha += Time.deltaTime;
                if (TrialInstructionCanvasGroup.alpha >= 1)
                {
                    startTime = Time.time;
                    stage++;
                }
            }
        }
        else if (stage == 32)
        {
            if (Time.time - startTime > roundIntervalTime)
            {
                TrialInstructionText.alignment = TextAnchor.MiddleCenter;
                TrialInstructionText.text = "Hold down the trigger button\r\n" +
                    "for 3 seconds to start.";
                stage++;
            }
        }
        else if (stage == 33)
        {
            Instruction.transform.position = cameraTransform.position;

            Instruction.transform.eulerAngles = new Vector3(0f, cameraTransform.localEulerAngles.y, 0f);

            Instruction.transform.Find("Canvas").Find("TrialInstruction").gameObject.SetActive(true);

            if (PlayerPrefs.GetString("Trigger states") == "Trigger pressed")
            {
                startTime = Time.time;
                stage++;
            }
        }
        else if (stage == 34)
        {
            if (PlayerPrefs.GetString("Trigger states") == "Trigger released")
            {
                stage--;
            }
            else if (PlayerPrefs.GetString("Trigger states") == "Trigger pressed"
                 && Time.time - startTime > 3f)
            {
                Instruction.transform.Find("Canvas").Find("TrialInstruction").gameObject.SetActive(false);
                TrialInstructionCanvasGroup.alpha = 0f;
                stage = 3;
            }
        }
        //wait for ending the task
        else if (stage == 40)
        {
            Instruction.transform.position = cameraTransform.position;

            Instruction.transform.eulerAngles = new Vector3(0f, cameraTransform.localEulerAngles.y, 0f);

            Instruction.transform.Find("Canvas").Find("TrialInstruction").gameObject.SetActive(true);          
            stage++;
        }
        else if (stage == 41)
        {
            if (TrialInstructionCanvasGroup.alpha < 1)
            {
                TrialInstructionCanvasGroup.alpha += Time.deltaTime;
                if (TrialInstructionCanvasGroup.alpha >= 1)
                {
                    startTime = Time.time;
                    stage++;
                }
            }
        }
        else if (stage == 42)
        {
            if (Time.time - startTime > waitForEndingTaskTime)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                 Application.Quit(); 
#endif
            }
        }
    }
    private void PointFromTo(Transform standingTransform, Transform facingTransform, Transform targetTransform)
    {
        standingTransform = StandingLandmarkGameobject.transform;
        facingTransform = FacingLandmarkGameobject.transform;
        targetTransform = TargetLandmarkGameobject.transform;
       Transform handStartTransform = rightHandTransform.Find("handStartPivot");
        Transform handEndTransform = rightHandTransform.Find("handEndPivot");


        //horizontal facing angle (starting from X-positive axis, incresing angle with counter clock-wise (0-360))
        Vector3 standToFace = facingTransform.position - standingTransform.position;
        float horizontalAngle_facing = CalculateHorizonalSignedAngle(standToFace);

        //horizontal target angle
        Vector3 standToTarget = targetTransform.position - standingTransform.position;
        float horizontalAngle_target = CalculateHorizonalSignedAngle(standToTarget);
        

        //horizontal JRD facing angle
        Vector3 standToFace_JRD = Instruction.transform.Find("Canvas").position - cameraTransform.position;
        float horizontalAngle_JRD_facing = CalculateHorizonalSignedAngle(standToFace_JRD);

        //horizontal JRD correct angle
        float horizontalDirection_correct = ConvertAngleDifferenceToSignedAngle(horizontalAngle_target
            - horizontalAngle_facing);
        float horizontalDirection_JRD_target = horizontalAngle_JRD_facing + horizontalDirection_correct;
        if (horizontalDirection_JRD_target > 360)
        {
            horizontalDirection_JRD_target = horizontalDirection_JRD_target - 360f;
        }

        //horizontal pointing angle
        Vector3 handPointing = handEndTransform.position - handStartTransform.position;
        float horizontalAngle_pointing = CalculateHorizonalSignedAngle(handPointing);        

        //horizontal pointing error
        float horizontalDirection_error = ConvertAngleDifferenceToSignedAngle
            (horizontalAngle_pointing - horizontalDirection_JRD_target);

        //horizontal absolute pointing error
        float horizontalDirection_abs_error = ConvertAngleDifferenceToAbsoluteAngle
            (horizontalAngle_pointing - horizontalDirection_JRD_target);


        //vertical target angle (downward is negative angle (0--90); upward is positive angle (0-90))
        float verticalAngle_target = CalculateVerticalSignedAngle(standToTarget);

        //vertical pointing angle
        float verticalAngle_pointing = CalculateVerticalSignedAngle(handPointing);

        //vertical pointing error
        float verticalDirection_error = verticalAngle_pointing - verticalAngle_target;

        //vertical absolute pointing error
        float verticalDirection_abs_error = Mathf.Abs(verticalDirection_error);

        // Debug.Log("verticalAngle_pointing = " + verticalAngle_pointing + " horizontalAngle_pointing = " + horizontalAngle_pointing);
        PointingDataCombo SavingDataCombo = new PointingDataCombo(PlayerPrefs.GetString("participantID"),
            StandingLandmarkGameobject.name, FacingLandmarkGameobject.name, TargetLandmarkGameobject.name,
            PointingLatency.ToString(), RoundNumberCount.ToString(), 
            TrialNumberPerRoundCount.ToString(), horizontalAngle_target.ToString(),
            horizontalAngle_JRD_facing.ToString(), horizontalDirection_JRD_target.ToString(),
            horizontalAngle_pointing.ToString(), horizontalDirection_error.ToString(),
            horizontalDirection_abs_error.ToString(), verticalAngle_target.ToString(),
            verticalAngle_pointing.ToString(), verticalDirection_error.ToString(),
            verticalDirection_abs_error.ToString());
        
        //Export data from Unity to CSV file in Asset/Resource folder
#if UNITY_EDITOR
        string filePath = @"Assets/Resource/Pointing_saved_data.csv";
#else
        string filePath = Application.persistentDataPath + "/Pointing_saved_data.csv";
#endif
        File.AppendAllText(filePath, SavingDataCombo.participant + "," + SavingDataCombo.standingLandmark 
            + "," + SavingDataCombo.facingLandmark + "," + SavingDataCombo.targetLandmark
            + "," + SavingDataCombo.latency + "," + SavingDataCombo.roundNumber
            + "," + SavingDataCombo.trialNumber
            + "," + SavingDataCombo.horizontalTargetAngle + "," + SavingDataCombo.horizontalJRDFacingAngle
            + "," + SavingDataCombo.horizontalJRDCorrectAngle + "," + SavingDataCombo.horizontalPointingAngle
            + "," + SavingDataCombo.horizontalPointingError + "," + SavingDataCombo.horizontalAbsPointingError
            + "," + SavingDataCombo.verticalTargetAngle + "," + SavingDataCombo.verticalPointingAngle
            + "," + SavingDataCombo.verticalPointingError + "," + SavingDataCombo.verticalAbsPointingError
             + "\n");
    }
    private float CalculateVerticalSignedAngle(Vector3 Direction)
    {
        float verticalAngle = Vector3.Angle(Direction, Vector3.up);
        if (Direction.y < 0)
        {
            verticalAngle = -verticalAngle;
        }
        if (verticalAngle <= 0)
       {
            verticalAngle = verticalAngle + 90f;
        }
        else
        {
            verticalAngle = 90f - verticalAngle;
        }

        return verticalAngle;
    }
    private float CalculateHorizonalSignedAngle(Vector3 Direction)
    {
        Direction = new Vector3(Direction.x, 0, Direction.z);
        float horizontalAngle = Quaternion.FromToRotation(Direction, Vector3.right).eulerAngles.y;

        return horizontalAngle;
    }

    private float ConvertAngleDifferenceToSignedAngle(float AngleDifference)
    {
        float signedAngle = AngleDifference;
        if (AngleDifference < 0)
        {
            signedAngle = 360f + AngleDifference;
        }
      
        return signedAngle;
    }
    private float ConvertAngleDifferenceToAbsoluteAngle(float AngleDifference)
    {
        float absError = AngleDifference;
        if (AngleDifference > 180f)
        {
            absError = 360f - AngleDifference;
        }
        else if (AngleDifference < -180f)
        {
            absError = 360f + AngleDifference;
        }
        return Mathf.Abs(absError);
    }
    public void showUI()
    {
        
    }
}



