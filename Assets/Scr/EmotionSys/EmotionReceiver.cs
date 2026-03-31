using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System;
using System.Threading;

[Serializable]
public class EmotionData
{
    public bool hasFace;    // Face  detected   
    public float valence;   //  -1.0 ~ 1.0
    public float arousal;   // -1.0 ~ 1.0
    public float intensity; // 0.0 ~ 1.0
    public string label;    // Happy
}

public class EmotionReceiver : MonoBehaviour
{
    public static EmotionReceiver Instance;

    [Header("Mode Setting")]
    [Tooltip("Check the box below to debug")]
    public bool UseDebugMode = false;

    [Header("Network Setting")]
    public string host = "127.0.0.1";
    public int port = 65432;

    [Header("Realtime Data/Debug Data")]
    public string currentEmotion = "Waiting...";
    [Range(-1.0f, 1.0f)] public float currentValence;
    [Range(-1.0f, 1.0f)] public float currentArousal;
    [Range(0.0f, 1.0f)] public float currentIntensity;
    public bool isConnected = false;

    private TcpClient client;
    private Thread receiveThread;
    private NetworkStream stream;
    private StreamReader reader;
    private bool isRunning = false;
    
    // data lock prevent multithread problem
    private object dataLock = new object();
    private EmotionData latestData = new EmotionData();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        if (!UseDebugMode)
        {
            ConnectToPython();
        }
    }

    void ConnectToPython()
    {
        try
        {
            client = new TcpClient();
            client.Connect(host, port);
            stream = client.GetStream();
            reader = new StreamReader(stream);

            isRunning = true;
            isConnected = true;
            
            // Enable new thread reciving data
            receiveThread = new Thread(ReceiveDataLoop);
            receiveThread.IsBackground = true;
            receiveThread.Start();
            
            Debug.Log("<color=green>Connection Success</color>");
        }
        catch (Exception e)
        {
            Debug.LogError($"Connection Error。\n Error: {e.Message}");
        }
    }

    //Data Receiver thread
    void ReceiveDataLoop()
    {
        while (isRunning)
        {
            try
            {
                if (stream != null && stream.CanRead)
                {
                    // read single line (json + \n)
                    string jsonStr = reader.ReadLine();
                    
                    if (!string.IsNullOrEmpty(jsonStr))
                    {
                        // analysis JSON
                        EmotionData data = JsonUtility.FromJson<EmotionData>(jsonStr);
                        
                        // save & lock data
                        lock (dataLock)
                        {
                            latestData = data;
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Network disconnect
                isConnected = false;
                break;
            }
        }
    }

    void Update()
    {
        if (UseDebugMode)
        {
            lock (dataLock)
            {
                latestData.hasFace = true;
                latestData.valence = currentValence;
                latestData.arousal = currentArousal;
                latestData.intensity = currentIntensity;
                latestData.label = "Debug";
            }
            currentEmotion = "Debug Mode";
        }
        else
        {
            EmotionData dataToShow = null;
            
            // pick up data from locked data
            lock (dataLock)
            {
                dataToShow = latestData;
            }

            // show data For debug
            if (dataToShow != null)
            {
                if (dataToShow.hasFace)
                {
                    currentEmotion = dataToShow.label;
                    currentValence = dataToShow.valence;
                    currentArousal = dataToShow.arousal;
                    currentIntensity = dataToShow.intensity;
                }
                else
                {
                    currentEmotion = "No Face";
                }
            }
        }
    }

    //API for other object get emotion
    public EmotionData GetEmotion()
    {
        lock (dataLock)
        {
            if (latestData == null) return new EmotionData();
            return latestData;
        }
    }

    // disconnect and free thread while game close 
    void OnApplicationQuit()
    {
        isRunning = false;
        if (receiveThread != null) receiveThread.Abort();
        if (reader != null) reader.Close();
        if (client != null) client.Close();
    }
}
